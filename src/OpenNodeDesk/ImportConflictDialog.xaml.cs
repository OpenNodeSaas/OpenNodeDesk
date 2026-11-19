using System.Windows;
using OpenNodeDesk.Services;

namespace OpenNodeDesk;

/// <summary>导入时对「相同 IP:端口」重复项的处置方式。</summary>
public enum ImportConflictAction
{
    Skip,
    Overwrite
}

/// <summary>导入判重冲突对话框：让用户选择跳过（保留现有）还是覆盖（使用导入数据），可应用到全部重复项。</summary>
public partial class ImportConflictDialog : Window
{
    public const string ResTitle = "Dlg_ImportConflictTitle";
    public const string ResHint = "Dlg_ImportConflictHint";
    public const string ResApplyAll = "Dlg_ImportConflictApplyAll";
    public const string ResSkip = "Btn_Skip";
    public const string ResOverwrite = "Btn_Overwrite";

    /// <summary>用户选择；直接关闭窗口时默认按「跳过」处理。</summary>
    public ImportConflictAction Action { get; private set; } = ImportConflictAction.Skip;

    /// <summary>是否把本次选择应用到后续全部重复项。</summary>
    public bool ApplyToAll => ChkApplyAll.IsChecked == true;

    public ImportConflictDialog(string ipPort, string existingName, string importedName)
    {
        InitializeComponent();
        TxtHint.Text = LocalizationManager.Instance.Format(ResHint, ipPort, existingName, importedName);
    }

    private void BtnSkip_Click(object sender, RoutedEventArgs e)
    {
        Action = ImportConflictAction.Skip;
        DialogResult = true;
    }

    private void BtnOverwrite_Click(object sender, RoutedEventArgs e)
    {
        Action = ImportConflictAction.Overwrite;
        DialogResult = true;
    }
}
