# OpenNodeDesk

[简体中文](../README.md) · [繁體中文](README.zh-TW.md) · English · [日本語](README.ja.md) · [한국어](README.ko.md) · [Deutsch](README.de.md) · [Français](README.fr.md) · [Español](README.es.md) · [Русский](README.ru.md) · [Português (Brasil)](README.pt-BR.md) · [Italiano](README.it.md) · [Türkçe](README.tr.md) · [Tiếng Việt](README.vi.md)

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](../LICENSE)
[![Release](https://img.shields.io/github/v/release/OpenNodeSaas/OpenNodeDesk)](https://github.com/OpenNodeSaas/OpenNodeDesk/releases)
[![Platform](https://img.shields.io/badge/platform-Windows%2010%20%2F%2011%20x64-lightgrey)](#download-and-run)

OpenNodeDesk is a free, portable, offline-first remote operations client for Windows administrators, built on WPF (.NET 10). It brings RDP, SSH, and VNC connections, the asset inventory, and connection templates together in a single local application: no account required, and your data never leaves the machine.

> Formerly known as RemotePilot, renamed on 2026-11-14. Release packages for v2.3.0 and earlier still use the file name `RemotePilot-*.zip` and run `RemotePilot.exe` after extraction; starting with this version, everything is named OpenNodeDesk.

## Core Features

### Connections

- **Direct RDP**: Dynamically generates a `.rdp` file and connects through the system's built-in `mstsc.exe`, with no third-party proxy involved; supports resolution, color depth, disk / clipboard / audio redirection, and quick left-right, top-bottom split layouts.
- **Built-in SSH terminal**: Based on SSH.NET and VirtualTerminal.WPF, with both password and private key authentication (private key preferred), and support for **SSH jump host** relaying.
- **SFTP file transfer**: Open the transfer window with one click from the terminal toolbar to browse directories, upload, download, refresh, and jump to paths; jump host scenarios reuse the same forwarding tunnel.
- **VNC**: Launches a locally installed VNC viewer (TightVNC / UltraVNC / RealVNC / TigerVNC), with support for custom parameter templates and automatic client path detection.

### Asset Management

- **Asset hub**: Grouping, favorites, recently used, expiry reminders, notes, and custom fields, with both table and card views.
- **Import and export**: Batch import from CSV / Excel (automatic deduplication, conflict confirmation), export by field; you can also pull assets from your own HTTP endpoint (JSON array or CSV text).
- **Connectivity probing**: Probe host reachability for a single host or in bulk, and write the results back to the asset.
- **Connection templates**: Reusable connection parameter templates, with resolution and split-screen presets.

### Session Workspace

- Multiple sessions on one screen, with self-adaptive cells; single-cell full screen, disconnect, and status indicators are clear at a glance.
- The toolbar's "Open" button lets you search by **asset name / address / group** and start a session directly.

### Security and Privacy

- **Offline-first**: All data is stored locally; no account, no telemetry, no network requirement.
- **Passwords are not saved by default**; only when you check "Remember password" are they encrypted to disk with Windows DPAPI (current user).
- **Protection passphrase**: You can set a passphrase to lock on idle timeout or lock immediately by hand; if you forget it, you can reset with the **recovery code**. After 5 consecutive wrong passphrase attempts, only passphrase entry is disabled; only after 5 more consecutive wrong recovery code attempts does "Clear local data" become the remaining option (must be allowed in settings beforehand).

### Interface

- Instant switching between light and dark themes, applied to all windows at once.
- UI language: 13 options (简体中文, 繁體中文, English, 日本語, 한국어, Deutsch, Français, Español, Русский, Português, Italiano, Türkçe, Tiếng Việt); switching applies instantly and is remembered.

## Download and Run

Release page: <https://github.com/OpenNodeSaas/OpenNodeDesk/releases>

| Release package | Description |
| --- | --- |
| `OpenNodeDesk-win-x64.zip` | Directory build, starts a bit faster and makes it easy to replace files yourself |
| `OpenNodeDesk-single-win-x64.zip` | Single-file build, with only one `OpenNodeDesk.exe` after extraction, easy to carry around |

- Requirements: Windows 10 / 11 (x64).
- The release packages are self-contained; no separate .NET runtime installation is needed. After extraction, run `OpenNodeDesk.exe` directly — portable and installation-free.

## Quick Start

1. Open the "Asset hub" and click "New asset", then fill in the name, host, port, and protocol (RDP defaults to 3389, SSH to 22, VNC to 5900).
2. Protocols that need credentials will prompt for a password; it is saved only if you check "Remember password".
3. Double-click the asset or click "Connect"; RDP opens an mstsc window and SSH opens the built-in terminal.
4. Once the SSH connection is established, click "SFTP File" in the terminal toolbar to upload / download files.
5. To view several machines at once, switch to the "Session workspace" and click "Open" to search for assets and add them.
6. Save frequently used parameters as a "Connection template" and apply it directly when creating new assets.

## Build

Requires [.NET SDK 10](https://dotnet.microsoft.com/download).

```powershell
dotnet build src\OpenNodeDesk\OpenNodeDesk.csproj -c Release
dotnet test tests\OpenNodeDesk.Tests
pwsh -File scripts\publish.ps1   # generates the two release packages under artifacts
```

## Data and Security

- Asset data is stored in `%LOCALAPPDATA%\FastRdp\RemoteOps\assets.json`; to back up or migrate, simply copy this file.
- For compatibility with historical versions, the data directory still uses `%LOCALAPPDATA%\FastRdp\RemoteOps\`, independent of the program name.
- Passwords are not saved by default. They are saved only when you check "Remember password", and are encrypted with Windows DPAPI (current user), so they can only be decrypted under the same Windows user.
- Each RDP connection generates a temporary `.rdp` file in `%TEMP%\OpenNodeDesk\{random folder}\`, containing the DPAPI-encrypted password field, which can be safely deleted.
- Before enabling the protection passphrase, it is recommended to back up your assets with "Export selected".

## FAQ

- **RDP redirection is checked but doesn't take effect?** After newer Windows security updates, opening an unsigned `.rdp` first shows a security dialog in which clipboard, drives, printers, and other redirections are all off by default; you have to check them once manually in the dialog. The app already writes keys such as `redirectclipboard:i:1` into the `.rdp` according to your selection, but this step cannot be done for you. To skip it entirely, code-sign the `.rdp`, or have IT trust a designated publisher through Group Policy.
- **The RDP window title shows "name (IP)"?** The mstsc title comes from the `.rdp` file name, and the app names it "asset name (host address).rdp".
- **After VNC connects, the workspace shows a placeholder page?** The remote screen is hosted in the local VNC viewer's own window; the app does not include a built-in VNC client.
- **Wrong SSH password?** The app prompts you to re-enter it and reconnects; it prompts only once per asset to avoid repeated pop-ups.
- **Can't connect to the jump host?** The jump host asset itself must have a usable password or private key; if both are missing, the connection errors out before it even starts.

For more details, see the in-app "Help manual" page.

## Third-Party Dependencies

| Component | License |
| --- | --- |
| [ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader) | MIT |
| [SSH.NET](https://github.com/sshnet/SSH.NET) | MIT |
| [BouncyCastle.Cryptography](https://www.bouncycastle.org/) | MIT |
| Microsoft.Extensions.Logging.Abstractions | MIT |
| [VirtualTerminal.WPF](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |
| [VirtualTerminal.SecureShell](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |

All are permissive open source licenses, with no commercial components, and are compatible with Apache 2.0.

## Code Signing

The release packages are currently unsigned. We plan to apply for free Windows code signing through the SignPath Foundation; see [CODE_SIGNING.md](../CODE_SIGNING.md) for details.

## Sponsorship

The GitHub Sponsors application has not been submitted yet (the maintainer needs to apply at github.com/sponsors and connect Stripe for payouts). The repository is already configured with `.github/FUNDING.yml` (`github: [OpenNodeSaas]`); once approved, a Sponsor button will appear on the repository page.

## Feedback

- Email: remotemate@protonmail.com
- Issue: <https://github.com/OpenNodeSaas/OpenNodeDesk/issues>

## License

This project is open sourced under the [Apache License 2.0](../LICENSE).
