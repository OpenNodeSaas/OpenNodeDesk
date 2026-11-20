using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using OpenNodeDesk.Services;

namespace OpenNodeDesk.Controls;

/// <summary>
/// 设置页：集中配置外装 VNC 客户端（程序路径 + 启动参数模板）与 SSH 终端外观（字号、配色方案）。
/// </summary>
public partial class SettingsView : UserControl
{
    // Localization Keys
    public const string ResTitle = "Settings_Title";
    public const string ResProtectionSection = "Settings_ProtectionSection";
    public const string ResProtectionEnable = "Settings_ProtectionEnable";
    public const string ResProtectionPassword = "Settings_ProtectionPassword";
    public const string ResProtectionConfirm = "Settings_ProtectionConfirm";
    public const string ResProtectionIdle = "Settings_ProtectionIdle";
    public const string ResProtectionLockNow = "Settings_ProtectionLockNow";
    public const string ResProtectionHint = "Settings_ProtectionHint";
    public const string ResProtectionAllowWipe = "Settings_ProtectionAllowWipe";

    private static LocalizationManager L10n => LocalizationManager.Instance;

    /// <summary>用户在设置页点击保存时触发（VNC 与 SSH 外观一起保存）。</summary>
    public event Action? Saved;

    /// <summary>用户点击「立即锁定」并校验通过后触发，由主窗口执行锁定。</summary>
    public event Action? LockRequested;

    /// <summary>用户在本页切换界面语言时触发，由主窗口应用并落盘。</summary>
    public event Action<AppLanguage>? LanguageSelectionChanged;

    /// <summary>当前 Saved 事件是否由「立即锁定」触发（用于跳过"设置已保存"弹窗）。</summary>
    public bool IsLockPending { get; private set; }

    public string VncViewerPath => TxtViewerPath.Text.Trim();
    public string VncArgumentTemplate => TxtArgs.Text.Trim();
    public double SshFontSize => SliderFontSize.Value;
    public int SshSchemeIndex => CmbScheme.SelectedIndex;
    public bool SshStandaloneWindow => ChkSshStandalone.IsChecked == true;

    public bool ProtectionEnabled => ChkProtectionEnable.IsChecked == true;
    public int ProtectionIdleMinutes => (CmbProtectionIdle.SelectedItem as ComboBoxItem)?.Tag as int? ?? 10;
    public bool ProtectionAllowDataWipe => ChkAllowDataWipe.IsChecked == true;
    public string NewPassword => PwdProtection.Password;
    public string ConfirmPassword => PwdProtectionConfirm.Password;

    /// <summary>本次保存新生成、需要用户抄写的恢复口令；不修改口令时为 null。</summary>
    public string? RecoveryCode { get; private set; }

    private bool _hasExistingPassword;
    private bool _loading;
    private bool _syncingLanguage;

    public SettingsView()
    {
        InitializeComponent();

        LocalizationManager.Instance.LanguageChanged += (s, e) =>
        {
            ApplyStaticTexts();
            RefreshViewerStatus();
        };

        ApplyStaticTexts();
        RefreshViewerStatus();
    }

    /// <summary>由主窗口在加载设置时调用，回填当前值。</summary>
    public void LoadSettings(string viewerPath, string argumentTemplate, double sshFontSize, int sshScheme, bool sshStandaloneWindow,
        bool protectionEnabled = false, int protectionIdleMinutes = 10, bool hasExistingPassword = false, bool protectionAllowDataWipe = true)
    {
        TxtViewerPath.Text = viewerPath ?? "";
        TxtArgs.Text = argumentTemplate ?? "";
        SliderFontSize.Value = Math.Clamp(sshFontSize, SshTerminalView.MinFontSize, SshTerminalView.MaxFontSize);
        CmbScheme.SelectedIndex = Math.Clamp(sshScheme, 0, CmbScheme.Items.Count - 1);
        ChkSshStandalone.IsChecked = sshStandaloneWindow;

        _loading = true;
        ChkProtectionEnable.IsChecked = protectionEnabled;
        SelectIdleMinutes(protectionIdleMinutes);
        ChkAllowDataWipe.IsChecked = protectionAllowDataWipe;
        PwdProtection.Password = "";
        PwdProtectionConfirm.Password = "";
        _hasExistingPassword = hasExistingPassword;
        UpdateProtectionEnabledState();
        _loading = false;

        RefreshViewerStatus();
        UpdatePreview();
    }

    private void ApplyStaticTexts()
    {
        TxtSubtitle.Text = L10n["Settings_Subtitle"];
        TxtLanguageSection.Text = L10n["Settings_Language"];
        TxtLanguageHint.Text = L10n["Settings_LanguageHint"];
        RebuildLanguageItems();
        TxtVncSection.Text = L10n["Settings_VncSection"];
        TxtSshSection.Text = L10n["Settings_SshSection"];
        TxtViewerLabel.Text = L10n["Help_ViewerLabel"];
        TxtArgsLabel.Text = L10n["Help_ArgsLabel"];
        TxtArgsHint.Text = L10n["Help_ArgsHint"];
        BtnBrowse.Content = L10n["Help_BtnBrowse"];
        BtnDetect.Content = L10n["Help_BtnDetect"];
        BtnResetArgs.Content = L10n["Help_BtnResetArgs"];
        TxtSshFontSize.Text = L10n["Settings_SshFontSize"];
        TxtSshScheme.Text = L10n["Settings_SshScheme"];
        ChkSshStandalone.Content = L10n["Settings_SshStandalone"];
        BtnSaveSettings.Content = L10n["Settings_BtnSave"];
        RebuildSchemeItems();
        UpdateFontSizeValue();

        TxtProtectionSection.Text = L10n[ResProtectionSection];
        ChkProtectionEnable.Content = L10n[ResProtectionEnable];
        TxtProtectionPasswordLabel.Text = L10n[ResProtectionPassword];
        TxtProtectionConfirmLabel.Text = L10n[ResProtectionConfirm];
        TxtProtectionIdleLabel.Text = L10n[ResProtectionIdle];
        ChkAllowDataWipe.Content = L10n[ResProtectionAllowWipe];
        TxtProtectionExistingHint.Text = L10n[ResProtectionHint];
        TxtProtectionRisk.Text = L10n["Settings_ProtectionRisk"];
        BtnLockNow.Content = L10n[ResProtectionLockNow];
        RebuildProtectionIdleItems();
    }

    /// <summary>按当前语言重建界面语言下拉项（显示各语言母语名称），并选中当前项。</summary>
    private void RebuildLanguageItems()
    {
        if (CmbLanguage is null) return;

        _syncingLanguage = true;
        CmbLanguage.Items.Clear();
        ComboBoxItem? selected = null;
        foreach (var option in LocalizationManager.SupportedLanguages)
        {
            var item = new ComboBoxItem { Content = option.NativeName, Tag = option.Language };
            CmbLanguage.Items.Add(item);
            if (option.Language == L10n.CurrentLanguage) selected = item;
        }

        CmbLanguage.SelectedItem = selected;
        _syncingLanguage = false;
    }

    private void CmbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // 重建列表时的程序化选中不当作业务选择，避免与主窗口来回触发
        if (_syncingLanguage) return;
        if ((CmbLanguage.SelectedItem as ComboBoxItem)?.Tag is AppLanguage language)
        {
            LanguageSelectionChanged?.Invoke(language);
        }
    }

    /// <summary>按当前语言重建空闲锁定时长下拉项，并尽量保留原选中分钟数。</summary>
    private void RebuildProtectionIdleItems()
    {
        int current = ProtectionIdleMinutes;
        CmbProtectionIdle.Items.Clear();
        foreach (int minutes in new[] { 1, 5, 10, 15, 30, 60 })
        {
            CmbProtectionIdle.Items.Add(new ComboBoxItem
            {
                Content = L10n[$"Settings_ProtectionIdle{minutes}"],
                Tag = minutes
            });
        }
        SelectIdleMinutes(current);
    }

    /// <summary>按分钟数选中下拉项；找不到时回落到 10 分钟（索引 2）。</summary>
    private void SelectIdleMinutes(int minutes)
    {
        if (CmbProtectionIdle is null) return;

        foreach (var item in CmbProtectionIdle.Items)
        {
            if (item is ComboBoxItem cbi && cbi.Tag is int m && m == minutes)
            {
                CmbProtectionIdle.SelectedItem = cbi;
                return;
            }
        }

        if (CmbProtectionIdle.Items.Count > 2)
        {
            CmbProtectionIdle.SelectedIndex = 2;
        }
    }

    private void ChkProtectionEnable_Changed(object sender, RoutedEventArgs e)
    {
        // 加载设置期间由 LoadSettings 统一刷新，避免读到尚未回填的已存口令标记
        if (!_loading) UpdateProtectionEnabledState();
    }

    /// <summary>口令相关控件的可用状态与已存口令提示跟随启用开关。</summary>
    private void UpdateProtectionEnabledState()
    {
        bool enabled = ProtectionEnabled;
        PwdProtection.IsEnabled = enabled;
        PwdProtectionConfirm.IsEnabled = enabled;
        CmbProtectionIdle.IsEnabled = enabled;
        ChkAllowDataWipe.IsEnabled = enabled;
        BtnLockNow.IsEnabled = enabled;
        TxtProtectionExistingHint.Visibility = enabled && _hasExistingPassword ? Visibility.Visible : Visibility.Collapsed;
    }

    /// <summary>按当前语言重建配色方案下拉项，并尽量保留原选中档位。</summary>
    private void RebuildSchemeItems()
    {
        int index = CmbScheme.SelectedIndex;
        CmbScheme.Items.Clear();
        CmbScheme.Items.Add(L10n["Term_SchemeDark"]);
        CmbScheme.Items.Add(L10n["Term_SchemeGreen"]);
        CmbScheme.Items.Add(L10n["Term_SchemeLight"]);
        CmbScheme.SelectedIndex = index >= 0 && index < CmbScheme.Items.Count ? index : 0;
    }

    private void UpdateFontSizeValue()
    {
        if (TxtFontSizeValue is not null)
        {
            TxtFontSizeValue.Text = ((int)SliderFontSize.Value).ToString();
        }
    }

    private void SliderFontSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => UpdateFontSizeValue();

    private void CmbScheme_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdatePreview();

    /// <summary>用当前配色档位的终端背景色实时预览（与 SshTerminalView 的取值保持一致）。</summary>
    private void UpdatePreview()
    {
        if (PreviewSwatch is null) return;
        string hex = CmbScheme.SelectedIndex switch
        {
            1 => "#0B1F0B",
            2 => "#F7F7F7",
            _ => "#1E1E1E"
        };
        PreviewSwatch.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));
    }

    private void RefreshViewerStatus()
    {
        var configured = TxtViewerPath.Text.Trim();

        if (configured.Length > 0 && !File.Exists(configured))
        {
            SetStatus(L10n["Help_ViewerNotExists"], "Danger");
            return;
        }

        var resolved = VncLauncher.ResolveViewer(configured);
        if (resolved == null)
        {
            SetStatus(L10n["Help_ViewerNotDetected"], "MutedForeground");
            return;
        }

        SetStatus(L10n.Format("Help_ViewerDetected", resolved.ExecutablePath, VncLauncher.DescribeFamily(resolved.Family)), "Success");
    }

    private void SetStatus(string text, string brushKey)
    {
        TxtViewerStatus.Text = text;
        TxtViewerStatus.Foreground = (Brush)FindResource(brushKey);
    }

    private void BtnBrowse_Click(object sender, RoutedEventArgs e)
    {
        var dlg = new OpenFileDialog
        {
            Title = L10n["Help_BtnBrowse"],
            Filter = L10n["Help_ViewerFilter"]
        };

        if (dlg.ShowDialog() == true)
        {
            TxtViewerPath.Text = dlg.FileName;
            RefreshViewerStatus();
        }
    }

    private void BtnDetect_Click(object sender, RoutedEventArgs e)
    {
        // 清空自定义路径后再探测，避免探测被旧路径短路
        var resolved = VncLauncher.ResolveViewer(null);
        if (resolved != null)
        {
            TxtViewerPath.Text = resolved.ExecutablePath;
            if (string.IsNullOrWhiteSpace(TxtArgs.Text))
            {
                TxtArgs.Text = VncLauncher.DefaultArgumentTemplate(resolved.Family);
            }
        }

        RefreshViewerStatus();
    }

    private void BtnResetArgs_Click(object sender, RoutedEventArgs e)
    {
        var family = VncLauncher.ResolveViewer(TxtViewerPath.Text)?.Family ?? VncViewerFamily.Unknown;
        TxtArgs.Text = VncLauncher.DefaultArgumentTemplate(family);
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateProtection()) return;

        PrepareRecoveryCode();
        Saved?.Invoke();

        // 口令已落盘：清空输入框并点亮「已设置口令」提示，避免再次保存时重复生成恢复口令
        ClearPasswordInputs();
        RefreshViewerStatus();
    }

    private void BtnLockNow_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateProtection()) return;

        PrepareRecoveryCode();
        IsLockPending = true;
        try
        {
            Saved?.Invoke();      // 先落盘
            LockRequested?.Invoke();
        }
        finally
        {
            IsLockPending = false;
            ClearPasswordInputs();
        }
    }

    /// <summary>启用保护且本次输入了新口令时生成恢复口令并提示用户备份；不修改口令时清空。</summary>
    private void PrepareRecoveryCode()
    {
        if (!ProtectionEnabled || NewPassword.Trim().Length == 0)
        {
            RecoveryCode = null;
            return;
        }

        RecoveryCode = ProtectionHasher.GenerateRecoveryCode();
        try
        {
            Clipboard.SetText(RecoveryCode);
        }
        catch (Exception)
        {
            // 剪贴板可能被其他进程占用；弹窗里仍会展示恢复口令
        }

        MessageBox.Show(Window.GetWindow(this),
            L10n.Format("Settings_ProtectionRecoveryMessage", RecoveryCode),
            L10n["Settings_ProtectionRecoveryTitle"], MessageBoxButton.OK, MessageBoxImage.Information);
    }

    /// <summary>保存成功后清空口令输入，并标记为「已设置口令」。</summary>
    private void ClearPasswordInputs()
    {
        PwdProtection.Clear();
        PwdProtectionConfirm.Clear();
        _hasExistingPassword = ProtectionEnabled;
        UpdateProtectionEnabledState();
    }

    /// <summary>校验保护口令输入；未启用或无需修改口令时直接通过。</summary>
    private bool ValidateProtection()
    {
        if (!ProtectionEnabled) return true;

        var pwd = NewPassword.Trim();
        var pwdConfirm = ConfirmPassword;

        if (pwd.Length == 0)
        {
            if (_hasExistingPassword) return true;
            ShowProtectionWarning(L10n["Settings_ProtectionPasswordRequired"]);
            return false;
        }

        if (pwd.Length < 4)
        {
            ShowProtectionWarning(L10n["Settings_ProtectionPasswordTooShort"]);
            return false;
        }

        if (pwd != pwdConfirm)
        {
            ShowProtectionWarning(L10n["Settings_ProtectionPasswordMismatch"]);
            return false;
        }

        return true;
    }

    private void ShowProtectionWarning(string message) =>
        MessageBox.Show(Window.GetWindow(this), message, L10n["Msg_Tip"], MessageBoxButton.OK, MessageBoxImage.Warning);
}
