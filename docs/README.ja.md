# OpenNodeDesk

[简体中文](../README.md) · [繁體中文](README.zh-TW.md) · [English](README.en.md) · 日本語 · [한국어](README.ko.md) · [Deutsch](README.de.md) · [Français](README.fr.md) · [Español](README.es.md) · [Русский](README.ru.md) · [Português (Brasil)](README.pt-BR.md) · [Italiano](README.it.md) · [Türkçe](README.tr.md) · [Tiếng Việt](README.vi.md)

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](../LICENSE)
[![Release](https://img.shields.io/github/v/release/OpenNodeSaas/OpenNodeDesk)](https://github.com/OpenNodeSaas/OpenNodeDesk/releases)
[![Platform](https://img.shields.io/badge/platform-Windows%2010%20%2F%2011%20x64-lightgrey)](#ダウンロードと実行)

OpenNodeDesk は、Windows 運用担当者向けの無料・ポータブル・オフライン優先のリモート運用クライアントで、WPF（.NET 10）で構築されています。RDP・SSH・VNC の 3 種類の接続、資産インベントリ、接続テンプレートを 1 つのローカルアプリにまとめました。アカウント不要で、データは端末の外に出ません。

> 前身は RemotePilot で、2026-11-14 に改名しました。v2.3.0 以前のリリースパッケージのファイル名は今も `RemotePilot-*.zip` で、解凍後は `RemotePilot.exe` を実行します。本バージョンより OpenNodeDesk に統一しました。

## 主な機能

### 接続

- **RDP 直接接続**：`.rdp` ファイルを動的に生成し、OS 標準の `mstsc.exe` で接続します。サードパーティのプロキシは経由しません。解像度、色深度、ディスク / クリップボード / サウンドのリダイレクト、および左右・上下の 1/2 分割のショートカット設定に対応します。
- **内蔵 SSH ターミナル**：SSH.NET と VirtualTerminal.WPF をベースに、パスワードと秘密鍵の二重認証（秘密鍵を優先）に対応し、**SSH 踏み台ホスト**経由の中継をサポートします。
- **SFTP ファイル転送**：ターミナルのツールバーからワンクリックで転送ウィンドウを開き、ディレクトリ閲覧、アップロード、ダウンロード、更新、パスの移動ができます。踏み台ホストの構成では同じ転送トンネルを再利用します。
- **VNC**：端末にインストール済みの VNC ビューア（TightVNC / UltraVNC / RealVNC / TigerVNC）を呼び出します。カスタムのパラメータテンプレートとクライアントパスの自動検出に対応します。

### 資産管理

- **資産ハブ**：グループ、お気に入り、最近使用、期限リマインダー、メモとカスタムフィールド、テーブル / カードの 2 表示。
- **インポート・エクスポート**：CSV / Excel の一括インポート（自動重複排除、競合の確認）、フィールド単位のエクスポート。独自の HTTP インターフェース（JSON 配列または CSV テキスト）から資産を取り込むこともできます。
- **到達性チェック**：単体または一括でホストの到達性をチェックし、結果を資産に書き戻します。
- **接続テンプレート**：再利用可能な接続パラメータのテンプレートで、解像度と画面分割のプリセットに対応します。

### セッションワークスペース

- 複数セッションを同一画面に表示し、セルは自動調整。単一セルの全画面表示、切断、状態表示がひと目でわかります。
- ツールバーの「開く」ボタンでは **資産名 / アドレス / グループ** で検索し、そのままセッションを開けます。

### セキュリティとプライバシー

- **オフライン優先**：すべてのデータは端末内に保存され、アカウント不要、テレメトリなし、ネット接続も不要です。
- **パスワードは既定で保存しません**。「パスワードを記憶する」をオンにすると、Windows DPAPI（現在のユーザー）で暗号化してディスクに保存します。
- **保護パスフレーズ**：パスフレーズを設定でき、アイドルタイムアウトまたは手動で即座にロックします。忘れた場合は**リカバリーコード**でリセットできます。パスフレーズを 5 回連続で間違えるとパスフレーズ入力のみが無効になり、リカバリーコードをさらに 5 回連続で間違えたときだけ「ローカルデータの消去」（事前に設定で許可が必要）が残ります。

### インターフェース

- ライト / ダークテーマを即時に切り替え、すべてのウィンドウに同期して反映されます。
- UI 言語：13 種類（简体中文、繁體中文、English、日本語、한국어、Deutsch、Français、Español、Русский、Português、Italiano、Türkçe、Tiếng Việt）。切り替えると即座に反映され、記憶されます。

## ダウンロードと実行

リリースページ：<https://github.com/OpenNodeSaas/OpenNodeDesk/releases>

| リリースパッケージ | 説明 |
| --- | --- |
| `OpenNodeDesk-win-x64.zip` | ディレクトリ版。起動がやや速く、ファイルの差し替えが容易 |
| `OpenNodeDesk-single-win-x64.zip` | 単一ファイル版。解凍すると `OpenNodeDesk.exe` が 1 つだけで、持ち運びに便利 |

- 動作要件：Windows 10 / 11（x64）。
- リリースパッケージは self-contained で、.NET ランタイムの別途インストールは不要です。解凍後はそのまま `OpenNodeDesk.exe` を実行でき、ポータブルでインストール不要です。

## クイックスタート

1. 「資産ハブ」を開き、「資産を追加」をクリックして、名前、ホスト、ポート、プロトコルを入力します（RDP は既定 3389、SSH は既定 22、VNC は既定 5900）。
2. 資格情報が必要なプロトコルではパスワードの入力を求められます。保存されるのは「パスワードを記憶する」をオンにした場合だけです。
3. 資産をダブルクリックするか「接続」をクリックします。RDP は mstsc ウィンドウを起動し、SSH は内蔵ターミナルを開きます。
4. SSH 接続の確立後、ターミナルツールバーの「SFTP ファイル」からアップロード / ダウンロードできます。
5. 複数のマシンを同時に見たいときは「セッションワークスペース」に切り替え、「開く」で資産を検索して追加します。
6. よく使うパラメータは「接続テンプレート」として保存し、資産の新規作成時にそのまま適用できます。

## ビルド

[.NET SDK 10](https://dotnet.microsoft.com/download) が必要です。

```powershell
dotnet build src\OpenNodeDesk\OpenNodeDesk.csproj -c Release
dotnet test tests\OpenNodeDesk.Tests
pwsh -File scripts\publish.ps1   # artifacts 配下に 2 つのリリースパッケージを生成
```

## データとセキュリティ

- 資産データは `%LOCALAPPDATA%\FastRdp\RemoteOps\assets.json` に保存されます。バックアップや移行はこのファイルをコピーするだけです。
- 過去バージョンとの互換のため、データディレクトリは今も `%LOCALAPPDATA%\FastRdp\RemoteOps\` を使用しており、プログラム名とは無関係です。
- 既定ではパスワードを保存しません。「パスワードを記憶する」をオンにしたときだけ保存し、Windows DPAPI（現在のユーザー）で暗号化されるため、同じ Windows ユーザーでしか復号できません。
- RDP は接続のたびに `%TEMP%\OpenNodeDesk\{ランダムディレクトリ}\` に一時的な `.rdp` ファイルを生成します。これには DPAPI で暗号化されたパスワードフィールドが含まれ、安全に削除できます。
- 保護パスフレーズを有効にする前に、「選択項目をエクスポート」で資産をバックアップすることをおすすめします。

## よくある質問

- **RDP のリダイレクトにチェックしたのに効かない？** 比較的新しい Windows のセキュリティ更新以降、署名されていない `.rdp` を開くと先にセキュリティダイアログが表示され、クリップボード、ドライブ、プリンターなどのリダイレクトは既定ですべてオフになっているため、ダイアログ内で手動で 1 回チェックする必要があります。アプリはあなたのチェックに従って `redirectclipboard:i:1` などのキーを `.rdp` に書き込んでいますが、この手動チェックだけは代行できません。完全にチェックを不要にするには `.rdp` にコード署名を行うか、IT がグループポリシーで特定の発行元を信頼する必要があります。
- **RDP のウィンドウタイトルが「名前 (IP)」と表示される？** mstsc のタイトルは `.rdp` のファイル名から取られ、アプリは「資産名 (ホストアドレス).rdp」で命名します。
- **VNC に接続後、ワークスペースにはプレースホルダーのページが表示される？** リモート画面は端末の VNC ビューア自身のウィンドウが表示するもので、アプリは VNC クライアントを内蔵していません。
- **SSH のパスワードが間違っている？** アプリは再入力を促して再接続します。同じ資産につき 1 回だけの通知で、ダイアログの繰り返しを防ぎます。
- **踏み台ホストに接続できない？** 踏み台ホストの資産自体に有効なパスワードまたは秘密鍵が必要で、どちらも欠けている場合は接続前にエラーになります。

より詳しい説明はアプリ内の「ヘルプマニュアル」ページをご覧ください。

## サードパーティ依存関係

| コンポーネント | ライセンス |
| --- | --- |
| [ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader) | MIT |
| [SSH.NET](https://github.com/sshnet/SSH.NET) | MIT |
| [BouncyCastle.Cryptography](https://www.bouncycastle.org/) | MIT |
| Microsoft.Extensions.Logging.Abstractions | MIT |
| [VirtualTerminal.WPF](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |
| [VirtualTerminal.SecureShell](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |

すべて寛容なオープンソースライセンスで、商業コンポーネントはなく、Apache 2.0 と互換性があります。

## コード署名

現在、リリースパッケージは署名されていません。SignPath Foundation を通じて無償の Windows コード署名を申請する予定です。詳細は [CODE_SIGNING.md](../CODE_SIGNING.md) をご覧ください。

## スポンサー

GitHub Sponsors の申請はまだ提出していません（メンテナーが github.com/sponsors で申請し、Stripe の受取を接続する必要があります）。リポジトリには `.github/FUNDING.yml`（`github: [OpenNodeSaas]`）を設定済みで、承認後はリポジトリページに Sponsor ボタンが表示されます。

## フィードバック

- メール：remotemate@protonmail.com
- Issue：<https://github.com/OpenNodeSaas/OpenNodeDesk/issues>

## ライセンス

本プロジェクトは [Apache License 2.0](../LICENSE) でオープンソース化されています。
