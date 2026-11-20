using System.Threading.Tasks;
using System.Windows;
using OpenNodeDesk.Controls;
using OpenNodeDesk.Models;

namespace OpenNodeDesk;

public partial class SshWindow : Window
{
    /// <summary>资产未指定窗口尺寸时使用的默认宽高。</summary>
    public const int DefaultWidth = 1000;
    public const int DefaultHeight = 640;

    private readonly SshTerminalHost _host;

    public SshWindow(ServerAsset asset, string? password, string? privateKeyPath, string? privateKeyPassphrase,
        ServerAsset? jumpAsset, string? jumpPassword)
    {
        InitializeComponent();

        // 标题格式与 RDP 一致：资产名称 (主机地址)
        Title = $"{asset.Name} ({asset.Host})";

        // 独立窗口尺寸按资产配置（在详情面板的「窗口大小」里设置）
        Width = asset.SshWindowWidth ?? DefaultWidth;
        Height = asset.SshWindowHeight ?? DefaultHeight;

        _host = new SshTerminalHost(asset, password, privateKeyPath, privateKeyPassphrase, jumpAsset, jumpPassword);
        RootGrid.Children.Add(_host);

        // 后台线程释放会话：SSH.NET 的 Disconnect 是同步网络操作，放 UI 线程会卡住主窗口（资产列表短暂无响应）
        Closed += (s, e) => Task.Run(_host.DisposeSession);
    }
}
