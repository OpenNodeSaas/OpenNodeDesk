using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using OpenNodeDesk.Services;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace OpenNodeDesk;

/// <summary>SFTP 文件传输窗口：浏览远程目录，并把文件上传 / 下载到本地。</summary>
public partial class SftpWindow : Window
{
    public const string ResTitle = "Sftp_Title";
    public const string ResUpload = "Sftp_Upload";
    public const string ResDownload = "Sftp_Download";
    public const string ResRefresh = "Sftp_Refresh";
    public const string ResUp = "Sftp_Up";
    public const string ResGo = "Sftp_Go";
    public const string ResName = "Sftp_Name";
    public const string ResSize = "Sftp_Size";
    public const string ResModified = "Sftp_Modified";

    private static LocalizationManager L10n => LocalizationManager.Instance;

    private readonly Func<SftpClient> _clientFactory;
    private readonly string _endpoint;
    private SftpClient? _client;
    private string _path = "/";
    private bool _busy;

    public SftpWindow(string assetName, string endpoint, Func<SftpClient> clientFactory)
    {
        InitializeComponent();
        _clientFactory = clientFactory;
        _endpoint = endpoint;
        Title = $"{L10n[ResTitle]} - {assetName}";

        Loaded += async (s, e) => await ConnectAsync();
        Closed += (s, e) =>
        {
            try { _client?.Dispose(); } catch { }
            _client = null;
        };
    }

    private async Task ConnectAsync()
    {
        SetBusy(true, L10n.Format("Sftp_Connecting", _endpoint));

        SftpClient? client = null;
        try
        {
            client = await Task.Run(_clientFactory);
            await Task.Run(client.Connect);
            _client = client;
            _path = string.IsNullOrWhiteSpace(client.WorkingDirectory) ? "/" : client.WorkingDirectory;
            await LoadDirectoryAsync(_path);
            TxtStatus.Text = L10n.Format("Sftp_Connected", _endpoint);
        }
        catch (Exception ex)
        {
            try { client?.Dispose(); } catch { }
            _client = null;
            TxtStatus.Text = L10n.Format("Sftp_ConnectFailed", ex.Message);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async Task LoadDirectoryAsync(string path)
    {
        if (_client is not SftpClient client) return;

        SetBusy(true, path);
        try
        {
            var entries = await Task.Run(() => client.ListDirectory(path)
                .Where(f => f.Name != "." && f.Name != "..")
                .Select(f => new SftpEntry(f))
                .OrderByDescending(e => e.IsDirectory)
                .ThenBy(e => e.Name, StringComparer.OrdinalIgnoreCase)
                .ToList());

            _path = path;
            TxtPath.Text = path;
            LstFiles.ItemsSource = entries;
            TxtStatus.Text = L10n.Format("Sftp_EntryCount", path, entries.Count);
        }
        catch (Exception ex)
        {
            TxtStatus.Text = L10n.Format("Sftp_ListFailed", ex.Message);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async void BtnUpload_Click(object sender, RoutedEventArgs e)
    {
        if (_busy) return;
        if (_client is not SftpClient client)
        {
            TxtStatus.Text = L10n["Sftp_NotConnected"];
            return;
        }

        var dialog = new OpenFileDialog
        {
            Title = L10n["Sftp_UploadTitle"],
            Filter = L10n["Sftp_AllFiles"],
            Multiselect = true
        };
        if (dialog.ShowDialog(this) != true) return;

        SetBusy(true, L10n["Sftp_UploadTitle"]);
        try
        {
            string targetDir = _path;
            await Task.Run(() =>
            {
                foreach (var localPath in dialog.FileNames)
                {
                    using var stream = File.OpenRead(localPath);
                    client.UploadFile(stream, Combine(targetDir, Path.GetFileName(localPath)), true);
                }
            });
            await LoadDirectoryAsync(_path);
            TxtStatus.Text = L10n.Format("Sftp_UploadDone", dialog.FileNames.Length);
        }
        catch (Exception ex)
        {
            TxtStatus.Text = L10n.Format("Sftp_TransferFailed", ex.Message);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async void BtnDownload_Click(object sender, RoutedEventArgs e)
    {
        if (_busy) return;
        if (_client is not SftpClient client)
        {
            TxtStatus.Text = L10n["Sftp_NotConnected"];
            return;
        }
        if (LstFiles.SelectedItem is not SftpEntry entry)
        {
            TxtStatus.Text = L10n["Sftp_SelectFile"];
            return;
        }
        if (entry.IsDirectory)
        {
            TxtStatus.Text = L10n["Sftp_DirNotFile"];
            return;
        }

        var dialog = new SaveFileDialog
        {
            Title = L10n["Sftp_DownloadTitle"],
            FileName = entry.Name
        };
        if (dialog.ShowDialog(this) != true) return;

        SetBusy(true, entry.Name);
        try
        {
            string remotePath = entry.FullName;
            string localPath = dialog.FileName;
            await Task.Run(() =>
            {
                using var stream = File.Create(localPath);
                client.DownloadFile(remotePath, stream);
            });
            TxtStatus.Text = L10n.Format("Sftp_DownloadDone", localPath);
        }
        catch (Exception ex)
        {
            TxtStatus.Text = L10n.Format("Sftp_TransferFailed", ex.Message);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async void BtnRefresh_Click(object sender, RoutedEventArgs e) => await LoadDirectoryAsync(_path);

    private async void BtnUp_Click(object sender, RoutedEventArgs e) => await LoadDirectoryAsync(ParentOf(_path));

    private async void BtnGo_Click(object sender, RoutedEventArgs e) => await NavigateToTypedPath();

    private async void TxtPath_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        e.Handled = true;
        await NavigateToTypedPath();
    }

    private Task NavigateToTypedPath()
    {
        string path = TxtPath.Text.Trim();
        return LoadDirectoryAsync(string.IsNullOrEmpty(path) ? "/" : path);
    }

    private async void LstFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (LstFiles.SelectedItem is SftpEntry entry && entry.IsDirectory)
        {
            await LoadDirectoryAsync(Combine(_path, entry.Name));
        }
    }

    private void SetBusy(bool busy, string? status = null)
    {
        _busy = busy;
        BtnUpload.IsEnabled = !busy && _client != null;
        BtnDownload.IsEnabled = !busy && _client != null;
        BtnRefresh.IsEnabled = !busy && _client != null;
        BtnUp.IsEnabled = !busy && _client != null;
        TxtPath.IsEnabled = !busy;
        if (status != null) TxtStatus.Text = status;
    }

    private static string Combine(string parent, string name) => parent.TrimEnd('/') + "/" + name;

    private static string ParentOf(string path)
    {
        string trimmed = path.TrimEnd('/');
        int index = trimmed.LastIndexOf('/');
        return index <= 0 ? "/" : trimmed[..index];
    }

    private static string FormatSize(long bytes)
    {
        string[] units = { "B", "KB", "MB", "GB", "TB" };
        double value = bytes;
        int unit = 0;
        while (value >= 1024 && unit < units.Length - 1)
        {
            value /= 1024;
            unit++;
        }
        return unit == 0 ? $"{bytes} B" : $"{value:0.#} {units[unit]}";
    }

    /// <summary>ListView 行数据：目录优先排序，大小与时间做展示格式化。</summary>
    public sealed class SftpEntry
    {
        public SftpEntry(ISftpFile file)
        {
            Name = file.Name;
            IsDirectory = file.IsDirectory;
            FullName = file.FullName;
            SizeText = file.IsDirectory ? "-" : FormatSize(file.Attributes.Size);
            ModifiedText = file.Attributes.LastWriteTime.ToString("yyyy-MM-dd HH:mm");
        }

        public string Name { get; }
        public bool IsDirectory { get; }
        public string FullName { get; }
        public string SizeText { get; }
        public string ModifiedText { get; }
        public string DisplayName => IsDirectory ? Name + "/" : Name;

        private static string FormatSize(long bytes) => SftpWindow.FormatSize(bytes);
    }
}
