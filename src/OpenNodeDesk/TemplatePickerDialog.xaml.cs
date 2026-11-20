using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using OpenNodeDesk.Models;
using OpenNodeDesk.Services;

namespace OpenNodeDesk;

/// <summary>批量设置连接模板时使用的模板选择对话框。</summary>
public partial class TemplatePickerDialog : Window
{
    public const string ResDlgTitle = "Btn_BatchTemplate";
    public const string ResFldTemplate = "Fld_Template";
    public const string ResBtnOk = "Btn_Save";
    public const string ResBtnCancel = "Btn_Cancel";

    private static LocalizationManager L10n => LocalizationManager.Instance;

    /// <summary>选中的模板 Id；为 null 表示清空绑定（无模板）。</summary>
    public Guid? SelectedTemplateId { get; private set; }

    public TemplatePickerDialog(IEnumerable<ConnectionTemplate> templates)
    {
        InitializeComponent();

        CmbTemplates.Items.Add(new ComboBoxItem { Content = L10n["Fld_TemplateNone"], Tag = null });
        foreach (var t in templates)
        {
            CmbTemplates.Items.Add(new ComboBoxItem { Content = t.GetDisplaySummary(), Tag = t.Id });
        }
        CmbTemplates.SelectedIndex = 0;
    }

    private void BtnOk_Click(object sender, RoutedEventArgs e)
    {
        SelectedTemplateId = (CmbTemplates.SelectedItem as ComboBoxItem)?.Tag as Guid?;
        DialogResult = true;
    }
}
