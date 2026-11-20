# OpenNodeDesk

简体中文 · [繁體中文](docs/README.zh-TW.md) · [English](docs/README.en.md) · [日本語](docs/README.ja.md) · [한국어](docs/README.ko.md) · [Deutsch](docs/README.de.md) · [Français](docs/README.fr.md) · [Español](docs/README.es.md) · [Русский](docs/README.ru.md) · [Português (Brasil)](docs/README.pt-BR.md) · [Italiano](docs/README.it.md) · [Türkçe](docs/README.tr.md) · [Tiếng Việt](docs/README.vi.md)

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](LICENSE)
[![Release](https://img.shields.io/github/v/release/OpenNodeSaas/OpenNodeDesk)](https://github.com/OpenNodeSaas/OpenNodeDesk/releases)
[![Platform](https://img.shields.io/badge/platform-Windows%2010%20%2F%2011%20x64-lightgrey)](#下载与运行)

OpenNodeDesk 是一款面向 Windows 运维人员的免费、绿色、离线优先的远程运维客户端，基于 WPF（.NET 10）构建。它把 RDP、SSH、VNC 三类连接、资产清单和连接方案收进同一个本地应用：无需账号，数据不出本机。

> 前身为 RemotePilot，2026-11-14 改名。v2.3.0 及更早的发布包文件名仍是 `RemotePilot-*.zip`，解压后运行 `RemotePilot.exe`；本版本起统一改名为 OpenNodeDesk。

## 核心特性

### 连接

- **RDP 直连**：动态生成 `.rdp` 文件，经系统自带 `mstsc.exe` 连接，不经过任何第三方代理；支持分辨率、颜色深度、磁盘 / 剪贴板 / 声音重定向，以及左右、上下一分二的快捷设置。
- **内置 SSH 终端**：基于 SSH.NET 与 VirtualTerminal.WPF，密码与私钥双认证（私钥优先），支持 **SSH 跳板机**中转。
- **SFTP 文件传输**：终端工具栏一键打开传输窗口，可浏览目录、上传、下载、刷新、跳转路径；跳板机场景复用同一条转发隧道。
- **VNC**：调用本机已安装的 VNC viewer（TightVNC / UltraVNC / RealVNC / TigerVNC），支持自定义参数模板与自动探测客户端路径。

### 资产管理

- **资产中心**：分组、收藏、最近使用、到期提醒、备注与自定义字段，表格 / 卡片双视图。
- **导入导出**：CSV / Excel 批量导入（自动去重、冲突确认）、按字段导出；也可从你自己的 HTTP 接口（JSON 数组或 CSV 文本）拉取资产。
- **连通性探测**：单台或批量探测主机可达性，结果写回资产。
- **连接方案模板**：可复用的连接参数模板，支持分辨率与分屏预设。

### 会话工作台

- 多会话同屏，单元格自适应；单格全屏、断开、状态指示一目了然。
- 工具栏「打开」按钮可按 **资产名 / 地址 / 分组** 搜索，直接开会话。

### 安全与隐私

- **离线优先**：所有数据保存在本机，无账号、无遥测、无联网要求。
- **密码默认不保存**；勾选「记住密码」后以 Windows DPAPI（当前用户）加密落盘。
- **保护口令**：可设置口令，空闲超时或手动立即锁定；忘记口令可用**恢复口令**重置。连续 5 次口令错误只停用口令输入，恢复口令再连续 5 次错误后才只剩「清空本地数据」（需事先在设置中允许）。

### 界面

- 亮色 / 暗色主题即时切换，所有窗口同步生效。
- 界面语言：13 种（简体中文、繁體中文、English、日本語、한국어、Deutsch、Français、Español、Русский、Português、Italiano、Türkçe、Tiếng Việt），切换后立即生效并记住。

## 下载与运行

发布页：<https://github.com/OpenNodeSaas/OpenNodeDesk/releases>

| 发布包 | 说明 |
| --- | --- |
| `OpenNodeDesk-win-x64.zip` | 目录版，启动略快，便于自行替换文件 |
| `OpenNodeDesk-single-win-x64.zip` | 单文件版，解压后只有一个 `OpenNodeDesk.exe`，便于携带 |

- 运行要求：Windows 10 / 11（x64）。
- 发布包为 self-contained，无需另装 .NET 运行时；解压后直接运行 `OpenNodeDesk.exe`，绿色免安装。

## 快速上手

1. 打开「资产中心」，点「新增资产」，填名称、主机、端口与协议（RDP 默认 3389，SSH 默认 22，VNC 默认 5900）。
2. 需要凭据的协议会提示输入密码；只有勾选「记住密码」才会保存。
3. 双击资产或点「连接」即可；RDP 会拉起 mstsc 窗口，SSH 会打开内置终端。
4. SSH 连接建立后，点终端工具栏「SFTP 文件」即可上传 / 下载文件。
5. 想同时看多台机器，切到「会话工作台」，点「打开」搜索资产并加入。
6. 常用参数存成「连接方案模板」，新建资产时直接套用。

## 构建

需要 [.NET SDK 10](https://dotnet.microsoft.com/download)。

```powershell
dotnet build src\OpenNodeDesk\OpenNodeDesk.csproj -c Release
dotnet test tests\OpenNodeDesk.Tests
pwsh -File scripts\publish.ps1   # 生成 artifacts 下的两个发布包
```

## 数据与安全

- 资产数据保存在 `%LOCALAPPDATA%\FastRdp\RemoteOps\assets.json`，备份或迁移只需复制该文件。
- 为兼容历史版本，数据目录仍沿用 `%LOCALAPPDATA%\FastRdp\RemoteOps\`，与程序名无关。
- 默认不保存密码。仅当你勾选「记住密码」时才保存，且使用 Windows DPAPI（当前用户）加密，只能在同一 Windows 用户下解密。
- RDP 每次连接会在 `%TEMP%\OpenNodeDesk\{随机目录}\` 生成一个临时 `.rdp` 文件，其中包含 DPAPI 加密后的密码字段，可以安全删除。
- 建议启用保护口令前，先用「导出所选」备份资产。

## 常见问题

- **RDP 重定向勾了却没生效？** 较新的 Windows 安全更新后，打开未签名 `.rdp` 会先弹出安全对话框，其中剪贴板、驱动器、打印机等重定向默认全部关闭，必须在对话框里手动勾选一次。应用已按你的勾选把 `redirectclipboard:i:1` 等键写入 `.rdp`，但这一步无法代勾。想彻底免勾需给 `.rdp` 做代码签名，或由 IT 通过组策略信任指定发布者。
- **RDP 窗口标题显示「名称 (IP)」？** mstsc 的标题取自 `.rdp` 文件名，应用按「资产名称 (主机地址).rdp」命名。
- **VNC 连上后工作台里是占位页？** 远程画面由本机 VNC viewer 自己的窗口承载，应用不内置 VNC 客户端。
- **SSH 密码错了？** 应用会提示重新输入并重连，同一资产只提示一次，避免反复弹窗。
- **跳板机连不上？** 跳板机资产自身必须有可用的密码或私钥，两者都缺失时连接会在开始前直接报错。

更完整的说明见应用内「帮助手册」页。

## 第三方依赖

| 组件 | 许可证 |
| --- | --- |
| [ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader) | MIT |
| [SSH.NET](https://github.com/sshnet/SSH.NET) | MIT |
| [BouncyCastle.Cryptography](https://www.bouncycastle.org/) | MIT |
| Microsoft.Extensions.Logging.Abstractions | MIT |
| [VirtualTerminal.WPF](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |
| [VirtualTerminal.SecureShell](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |

全部为宽松开源许可证，无商业组件，与 Apache 2.0 兼容。

## 代码签名

目前发布包未签名。计划通过 SignPath Foundation 申请免费 Windows 代码签名，详情见 [CODE_SIGNING.md](CODE_SIGNING.md)。

## 赞助

GitHub Sponsors 申请尚未提交（需维护者在 github.com/sponsors 申请并接通 Stripe 收款）。仓库已配置 `.github/FUNDING.yml`（`github: [OpenNodeSaas]`），通过后仓库页面将显示 Sponsor 按钮。

## 反馈

- 邮箱：remotemate@protonmail.com
- Issue：<https://github.com/OpenNodeSaas/OpenNodeDesk/issues>

## 许可证

本项目以 [Apache License 2.0](LICENSE) 开源。
