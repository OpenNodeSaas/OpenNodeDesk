# OpenNodeDesk

[简体中文](../README.md) · 繁體中文 · [English](README.en.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [Deutsch](README.de.md) · [Français](README.fr.md) · [Español](README.es.md) · [Русский](README.ru.md) · [Português (Brasil)](README.pt-BR.md) · [Italiano](README.it.md) · [Türkçe](README.tr.md) · [Tiếng Việt](README.vi.md)

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](../LICENSE)
[![Release](https://img.shields.io/github/v/release/OpenNodeSaas/OpenNodeDesk)](https://github.com/OpenNodeSaas/OpenNodeDesk/releases)
[![Platform](https://img.shields.io/badge/platform-Windows%2010%20%2F%2011%20x64-lightgrey)](#下載與執行)

OpenNodeDesk 是一款面向 Windows 維運人員的免費、綠色、離線優先的遠端維運用戶端，以 WPF（.NET 10）打造。它把 RDP、SSH、VNC 三類連線、資產清單與連線方案收進同一個本機應用程式：免帳號，資料不出本機。

> 前身為 RemotePilot，於 2026-11-14 改名。v2.3.0 及更早的發佈包檔名仍是 `RemotePilot-*.zip`，解壓縮後執行 `RemotePilot.exe`；本版本起統一改名為 OpenNodeDesk。

## 核心功能

### 連線

- **RDP 直連**：動態產生 `.rdp` 檔，經系統內建的 `mstsc.exe` 連線，不經過任何第三方代理；支援解析度、色彩深度、磁碟 / 剪貼簿 / 音效重新導向，以及左右、上下二分之一的快捷設定。
- **內建 SSH 終端**：以 SSH.NET 與 VirtualTerminal.WPF 為基礎，密碼與私鑰雙重驗證（私鑰優先），支援 **SSH 跳板機**中轉。
- **SFTP 檔案傳輸**：終端工具列一鍵開啟傳輸視窗，可瀏覽目錄、上傳、下載、重新整理、跳轉路徑；跳板機情境會沿用同一條轉送通道。
- **VNC**：呼叫本機已安裝的 VNC viewer（TightVNC / UltraVNC / RealVNC / TigerVNC），支援自訂參數範本與自動偵測用戶端路徑。

### 資產管理

- **資產中心**：分組、收藏、最近使用、到期提醒、備註與自訂欄位，表格 / 卡片雙檢視。
- **匯入匯出**：CSV / Excel 批次匯入（自動去除重複、衝突確認）、依欄位匯出；也可從你自己的 HTTP 介面（JSON 陣列或 CSV 文字）拉取資產。
- **連通性探測**：單台或批次探測主機可達性，結果寫回資產。
- **連線方案範本**：可重複使用的連線參數範本，支援解析度與分割畫面預設。

### 工作階段工作台

- 多個工作階段同螢幕顯示，儲存格自動調整；單格全螢幕、中斷、狀態指示一目瞭然。
- 工具列「開啟」按鈕可依 **資產名稱 / 位址 / 分組** 搜尋，直接開啟工作階段。

### 安全性與隱私

- **離線優先**：所有資料保存在本機，免帳號、無遙測、無須連網。
- **密碼預設不儲存**；勾選「記住密碼」後以 Windows DPAPI（目前使用者）加密寫入磁碟。
- **保護口令**：可設定的口令，閒置逾時或手動立即鎖定；忘記口令可用**恢復口令**重設。連續 5 次口令錯誤只會停用口令輸入，恢復口令再連續 5 次錯誤後才只剩「清除本機資料」（須事先在設定中允許）。

### 介面

- 亮色 / 暗色主題即時切換，所有視窗同步生效。
- 介面語言：13 種（简体中文、繁體中文、English、日本語、한국어、Deutsch、Français、Español、Русский、Português、Italiano、Türkçe、Tiếng Việt），切換後立即生效並記住。

## 下載與執行

發佈頁：<https://github.com/OpenNodeSaas/OpenNodeDesk/releases>

| 發佈包 | 說明 |
| --- | --- |
| `OpenNodeDesk-win-x64.zip` | 目錄版，啟動略快，便於自行替換檔案 |
| `OpenNodeDesk-single-win-x64.zip` | 單檔版，解壓縮後只有一個 `OpenNodeDesk.exe`，便於攜帶 |

- 執行需求：Windows 10 / 11（x64）。
- 發佈包為 self-contained，無須另裝 .NET 執行階段；解壓縮後直接執行 `OpenNodeDesk.exe`，綠色免安裝。

## 快速上手

1. 開啟「資產中心」，點「新增資產」，填寫名稱、主機、連接埠與通訊協定（RDP 預設 3389，SSH 預設 22，VNC 預設 5900）。
2. 需要憑證的通訊協定會提示輸入密碼；只有勾選「記住密碼」才會儲存。
3. 連按兩下資產或點「連線」即可；RDP 會拉起 mstsc 視窗，SSH 會開啟內建終端。
4. SSH 連線建立後，點終端工具列「SFTP 檔案」即可上傳 / 下載檔案。
5. 想同時檢視多台機器，切到「工作階段工作台」，點「開啟」搜尋資產並加入。
6. 常用參數存成「連線方案範本」，新增資產時直接套用。

## 建置

需要 [.NET SDK 10](https://dotnet.microsoft.com/download)。

```powershell
dotnet build src\OpenNodeDesk\OpenNodeDesk.csproj -c Release
dotnet test tests\OpenNodeDesk.Tests
pwsh -File scripts\publish.ps1   # 產生 artifacts 下的兩個發佈包
```

## 資料與安全

- 資產資料保存在 `%LOCALAPPDATA%\FastRdp\RemoteOps\assets.json`，備份或移轉只需複製該檔案。
- 為相容歷史版本，資料目錄仍沿用 `%LOCALAPPDATA%\FastRdp\RemoteOps\`，與程式名稱無關。
- 預設不儲存密碼。僅當你勾選「記住密碼」時才儲存，且使用 Windows DPAPI（目前使用者）加密，只能在同一個 Windows 使用者下解密。
- RDP 每次連線會在 `%TEMP%\OpenNodeDesk\{隨機目錄}\` 產生一個臨時 `.rdp` 檔，其中包含 DPAPI 加密後的密碼欄位，可以安全刪除。
- 建議啟用保護口令前，先用「匯出所選」備份資產。

## 常見問題

- **RDP 重新導向勾了卻沒生效？** 較新的 Windows 安全更新之後，開啟未簽章的 `.rdp` 會先彈出安全對話方塊，其中剪貼簿、磁碟機、印表機等重新導向預設全部關閉，必須在對話方塊裡手動勾選一次。應用程式已依你的勾選把 `redirectclipboard:i:1` 等鍵寫入 `.rdp`，但這一步無法代勾。想徹底免勾需給 `.rdp` 做程式碼簽章，或由 IT 透過群組原則信任指定的發行者。
- **RDP 視窗標題顯示「名稱 (IP)」？** mstsc 的標題取自 `.rdp` 檔名，應用程式依「資產名稱 (主機位址).rdp」命名。
- **VNC 連上後工作台裡是佔位頁？** 遠端畫面由本機 VNC viewer 自己的視窗承載，應用程式不內建 VNC 用戶端。
- **SSH 密碼錯了？** 應用程式會提示重新輸入並重新連線，同一資產只提示一次，避免反覆彈窗。
- **跳板機連不上？** 跳板機資產本身必須有可用的密碼或私鑰，兩者都缺少時連線會在開始前直接報錯。

更完整的說明見應用程式內「說明手冊」頁。

## 第三方相依

| 元件 | 授權條款 |
| --- | --- |
| [ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader) | MIT |
| [SSH.NET](https://github.com/sshnet/SSH.NET) | MIT |
| [BouncyCastle.Cryptography](https://www.bouncycastle.org/) | MIT |
| Microsoft.Extensions.Logging.Abstractions | MIT |
| [VirtualTerminal.WPF](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |
| [VirtualTerminal.SecureShell](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |

全部為寬鬆的開放原始碼授權，無商業元件，與 Apache 2.0 相容。

## 程式碼簽章

目前發佈包未簽章。計畫透過 SignPath Foundation 申請免費 Windows 程式碼簽章，詳情見 [CODE_SIGNING.md](../CODE_SIGNING.md)。

## 贊助

GitHub Sponsors 申請尚未提交（需維護者在 github.com/sponsors 申請並接通 Stripe 收款）。儲存庫已設定 `.github/FUNDING.yml`（`github: [OpenNodeSaas]`），通過後儲存庫頁面將顯示 Sponsor 按鈕。

## 意見回饋

- 電子郵件：remotemate@protonmail.com
- Issue：<https://github.com/OpenNodeSaas/OpenNodeDesk/issues>

## 授權條款

本專案以 [Apache License 2.0](../LICENSE) 開源。
