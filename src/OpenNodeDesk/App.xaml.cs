using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using OpenNodeDesk.Services;

namespace OpenNodeDesk;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    // 锁名沿用改名前的 RemotePilot：与旧版本共用同一把锁，避免新旧版本同时运行出现双实例
    private const string MutexName = @"Global\RemotePilot.SingleInstance";
    private const string ActivateEventName = @"Global\RemotePilot.Activate";

    // 命名互斥体/事件，进程存活期间必须持有引用，否则会被 GC 回收导致单实例失效
    private Mutex? _singleInstanceMutex;
    private EventWaitHandle? _activateEvent;
    private RegisteredWaitHandle? _activateWait;

    /// <summary>退出后是否需要重新启动自身（用于「清空数据并进入」）。</summary>
    public static bool RestartOnExit { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        _singleInstanceMutex = new Mutex(true, MutexName, out var isFirstInstance);
        if (!isFirstInstance)
        {
            // 已有实例在跑：通知它把窗口放到最前，然后本进程直接退出
            SignalExistingInstance();
            _singleInstanceMutex.Dispose();
            _singleInstanceMutex = null;
            Shutdown();
            return;
        }

        base.OnStartup(e);
        ThemeManager.Instance.Initialize();
        // 界面语言沿用上次选择；配置文件缺失或标记未知时回落到简体中文
        LocalizationManager.Instance.Initialize(LocalizationManager.FromTag(new AssetStore().Load().Language));

        // 首个实例监听"再次启动"信号，收到后激活自己的主窗口
        _activateEvent = new EventWaitHandle(false, EventResetMode.AutoReset, ActivateEventName);
        _activateWait = ThreadPool.RegisterWaitForSingleObject(
            _activateEvent,
            (_, _) => ActivateMainWindow(),
            null,
            Timeout.Infinite,
            executeOnlyOnce: false);
    }

    private static void SignalExistingInstance()
    {
        // 首个实例创建事件句柄前可能有极短的窗口期，重试几次即可
        for (int i = 0; i < 5; i++)
        {
            try
            {
                using var handle = EventWaitHandle.OpenExisting(ActivateEventName);
                handle.Set();
                return;
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                Thread.Sleep(100);
            }
        }
    }

    private void ActivateMainWindow()
    {
        Dispatcher.BeginInvoke(() =>
        {
            if (MainWindow is not { } window)
            {
                return;
            }

            if (window.WindowState == WindowState.Minimized)
            {
                window.WindowState = WindowState.Normal;
            }

            window.Show();
            window.Activate();
            // Windows 前台窗口限制会让 Activate 失效，用 TOPMOST 瞬时切换强制提到最前
            window.Topmost = true;
            window.Topmost = false;
            window.Focus();
        });
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _activateWait?.Unregister(null);
        _activateEvent?.Dispose();
        _singleInstanceMutex?.ReleaseMutex();
        _singleInstanceMutex?.Dispose();
        base.OnExit(e);

        if (!RestartOnExit) return;
        try
        {
            if (Environment.ProcessPath is { } exePath)
            {
                Process.Start(new ProcessStartInfo { FileName = exePath, UseShellExecute = true });
                RestartOnExit = false;
            }
        }
        catch
        {
            // 重启失败时不再抛异常，避免影响退出流程
        }
    }
}
