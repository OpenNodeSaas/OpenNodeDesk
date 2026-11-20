using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OpenNodeDesk.Services;

namespace OpenNodeDesk;

/// <summary>打开资产对话框：按名称/地址搜索或直接在列表中选择。</summary>
public partial class AssetPickerWindow : Window
{
    public const string ResTitle = "Picker_Title";
    public const string ResSearchHint = "Picker_SearchHint";
    public const string ResNoMatch = "Picker_NoMatch";
    public const string ResOpen = "Picker_Open";
    public const string ResCancel = "Picker_Cancel";

    private static LocalizationManager L10n => LocalizationManager.Instance;

    private readonly IReadOnlyList<ServerAssetViewModel> _assets;

    public AssetPickerWindow(IReadOnlyList<ServerAssetViewModel> assets)
    {
        InitializeComponent();
        _assets = assets;
        Filter(string.Empty);
        Loaded += (s, e) => TxtSearch.Focus();
    }

    /// <summary>用户确认要打开的资产；取消时为 null。</summary>
    public ServerAssetViewModel? SelectedAsset { get; private set; }

    private void Filter(string keyword)
    {
        string key = keyword.Trim();
        var matched = string.IsNullOrEmpty(key)
            ? _assets
            : _assets.Where(a =>
                    a.Name.Contains(key, StringComparison.OrdinalIgnoreCase) ||
                    a.Host.Contains(key, StringComparison.OrdinalIgnoreCase) ||
                    a.IpPortDisplay.Contains(key, StringComparison.OrdinalIgnoreCase) ||
                    a.Group.Contains(key, StringComparison.OrdinalIgnoreCase))
                .ToList();

        LstAssets.ItemsSource = matched;
        if (matched.Count > 0) LstAssets.SelectedIndex = 0;

        TxtEmpty.Visibility = matched.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        TxtSearchHint.Visibility = string.IsNullOrEmpty(key) ? Visibility.Visible : Visibility.Collapsed;
    }

    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e) => Filter(TxtSearch.Text);

    private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        e.Handled = true;
        Confirm();
    }

    private void LstAssets_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Confirm();

    private void BtnOpen_Click(object sender, RoutedEventArgs e) => Confirm();

    private void Confirm()
    {
        if (LstAssets.SelectedItem is not ServerAssetViewModel asset) return;
        SelectedAsset = asset;
        DialogResult = true;
    }
}
