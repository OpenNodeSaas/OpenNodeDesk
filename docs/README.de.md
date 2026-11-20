# OpenNodeDesk

[简体中文](../README.md) · [繁體中文](README.zh-TW.md) · [English](README.en.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · Deutsch · [Français](README.fr.md) · [Español](README.es.md) · [Русский](README.ru.md) · [Português (Brasil)](README.pt-BR.md) · [Italiano](README.it.md) · [Türkçe](README.tr.md) · [Tiếng Việt](README.vi.md)

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](../LICENSE)
[![Release](https://img.shields.io/github/v/release/OpenNodeSaas/OpenNodeDesk)](https://github.com/OpenNodeSaas/OpenNodeDesk/releases)
[![Platform](https://img.shields.io/badge/platform-Windows%2010%20%2F%2011%20x64-lightgrey)](#herunterladen-und-ausführen)

OpenNodeDesk ist ein kostenloser, portabler, offline-orientierter Remote-Operations-Client für Windows-Administratoren, entwickelt auf Basis von WPF (.NET 10). Er vereint RDP-, SSH- und VNC-Verbindungen, das Asset-Inventar und Verbindungsvorlagen in einer einzigen lokalen Anwendung: kein Konto nötig, und die Daten verlassen den Rechner nicht.

> Früher als RemotePilot bekannt, umbenannt am 2026-11-14. Die Release-Pakete von v2.3.0 und älter tragen weiterhin den Dateinamen `RemotePilot-*.zip` und starten nach dem Entpacken `RemotePilot.exe`; ab dieser Version wird alles einheitlich OpenNodeDesk genannt.

## Kernfunktionen

### Verbindungen

- **Direktes RDP**: Erzeugt dynamisch eine `.rdp`-Datei und verbindet über das systemeigene `mstsc.exe`, ohne einen Drittanbieter-Proxy; unterstützt Auflösung, Farbtiefe, Disk- / Clipboard- / Audio-Umleitung sowie Schnelleinstellungen für links-rechts und oben-unten geteilte Layouts.
- **Integriertes SSH-Terminal**: Basiert auf SSH.NET und VirtualTerminal.WPF, mit Passwort- und Private-Key-Authentifizierung (Private Key bevorzugt) und Unterstützung für **SSH-Jump-Host**-Relais.
- **SFTP-Dateiübertragung**: Öffnet das Übertragungsfenster mit einem Klick aus der Terminal-Symbolleiste; Verzeichnisse durchsuchen, hochladen, herunterladen, aktualisieren und zu Pfaden springen; Jump-Host-Szenarien nutzen denselben Weiterleitungstunnel.
- **VNC**: Startet einen lokal installierten VNC-Viewer (TightVNC / UltraVNC / RealVNC / TigerVNC); unterstützt benutzerdefinierte Parameter-Vorlagen und die automatische Erkennung des Client-Pfads.

### Asset-Verwaltung

- **Asset hub**: Gruppierung, Favoriten, zuletzt verwendet, Ablauf-Erinnerungen, Notizen und benutzerdefinierte Felder, mit Tabellen- und Kartenansicht.
- **Import und Export**: Massenimport aus CSV / Excel (automatische Deduplizierung, Konfliktbestätigung), Export nach Feldern; Assets können auch von Ihrem eigenen HTTP-Endpunkt (JSON-Array oder CSV-Text) abgerufen werden.
- **Konnektivitätsprüfung**: Erreichbarkeit einzelner oder mehrerer Hosts prüfen und die Ergebnisse in das Asset zurückschreiben.
- **Verbindungsvorlagen**: Wiederverwendbare Vorlagen für Verbindungsparameter, mit Auflösungs- und Bildschirmteilungs-Presets.

### Sitzungs-Arbeitsbereich

- Mehrere Sitzungen auf einem Bildschirm, mit selbstanpassenden Zellen; Vollbild einer einzelnen Zelle, Trennen und Statusanzeigen auf einen Blick.
- Die Schaltfläche „Öffnen" in der Symbolleiste sucht nach **Asset-Name / Adresse / Gruppe** und öffnet eine Sitzung direkt.

### Sicherheit und Datenschutz

- **Offline-orientiert**: Alle Daten werden lokal gespeichert; kein Konto, keine Telemetrie, keine Netzwerkanforderung.
- **Passwörter werden standardmäßig nicht gespeichert**; erst mit „Passwort merken" werden sie mit Windows DPAPI (aktueller Benutzer) verschlüsselt auf der Festplatte abgelegt.
- **Schutz-Passphrase**: Sie können eine Passphrase festlegen, um bei Leerlauf-Timeout oder manuell sofort zu sperren; wenn Sie sie vergessen, können Sie mit dem **Wiederherstellungscode** zurücksetzen. Nach 5 aufeinanderfolgenden falschen Passphrasen wird nur die Passphrase-Eingabe deaktiviert; erst nach 5 weiteren aufeinanderfolgenden Fehlern beim Wiederherstellungscode bleibt nur noch „Lokale Daten löschen" übrig (muss vorab in den Einstellungen erlaubt werden).

### Oberfläche

- Sofortiges Umschalten zwischen hellem und dunklem Design, gleichzeitig auf alle Fenster angewendet.
- Oberflächensprache: 13 Optionen (简体中文, 繁體中文, English, 日本語, 한국어, Deutsch, Français, Español, Русский, Português, Italiano, Türkçe, Tiếng Việt); die Auswahl wirkt sofort und bleibt gespeichert.

## Herunterladen und Ausführen

Release-Seite: <https://github.com/OpenNodeSaas/OpenNodeDesk/releases>

| Release-Paket | Beschreibung |
| --- | --- |
| `OpenNodeDesk-win-x64.zip` | Verzeichnisversion, startet etwas schneller und erleichtert das Ersetzen von Dateien |
| `OpenNodeDesk-single-win-x64.zip` | Einzeldatei-Version, nach dem Entpacken nur eine `OpenNodeDesk.exe`, einfach zum Mitnehmen |

- Voraussetzungen: Windows 10 / 11 (x64).
- Die Release-Pakete sind self-contained; es muss keine separate .NET-Laufzeit installiert werden. Nach dem Entpacken starten Sie direkt `OpenNodeDesk.exe` – portabel und installationsfrei.

## Schnellstart

1. Öffnen Sie den „Asset hub" und klicken Sie auf „Neues Asset"; geben Sie Name, Host, Port und Protokoll an (RDP standardmäßig 3389, SSH 22, VNC 5900).
2. Protokolle, die Anmeldedaten benötigen, fragen nach einem Passwort; gespeichert wird es nur, wenn Sie „Passwort merken" aktivieren.
3. Doppelklicken Sie auf das Asset oder klicken Sie auf „Verbinden"; RDP öffnet ein mstsc-Fenster, SSH öffnet das integrierte Terminal.
4. Sobald die SSH-Verbindung hergestellt ist, klicken Sie in der Terminal-Symbolleiste auf „SFTP-Datei", um Dateien hoch- oder herunterzuladen.
5. Um mehrere Rechner gleichzeitig zu sehen, wechseln Sie zum „Sitzungs-Arbeitsbereich" und klicken Sie auf „Öffnen", um Assets zu suchen und hinzuzufügen.
6. Speichern Sie häufig verwendete Parameter als „Verbindungsvorlage" und wenden Sie sie beim Anlegen neuer Assets direkt an.

## Build

Erfordert [.NET SDK 10](https://dotnet.microsoft.com/download).

```powershell
dotnet build src\OpenNodeDesk\OpenNodeDesk.csproj -c Release
dotnet test tests\OpenNodeDesk.Tests
pwsh -File scripts\publish.ps1   # erzeugt die beiden Release-Pakete unter artifacts
```

## Daten und Sicherheit

- Asset-Daten werden in `%LOCALAPPDATA%\FastRdp\RemoteOps\assets.json` gespeichert; zum Sichern oder Migrieren genügt das Kopieren dieser Datei.
- Aus Kompatibilität mit älteren Versionen verwendet das Datenverzeichnis weiterhin `%LOCALAPPDATA%\FastRdp\RemoteOps\`, unabhängig vom Programmnamen.
- Passwörter werden standardmäßig nicht gespeichert. Sie werden nur gespeichert, wenn Sie „Passwort merken" aktivieren, und mit Windows DPAPI (aktueller Benutzer) verschlüsselt, sodass sie nur unter demselben Windows-Benutzer entschlüsselt werden können.
- Jede RDP-Verbindung erzeugt in `%TEMP%\OpenNodeDesk\{random folder}\` eine temporäre `.rdp`-Datei, die das DPAPI-verschlüsselte Passwortfeld enthält und gefahrlos gelöscht werden kann.
- Bevor Sie die Schutz-Passphrase aktivieren, sollten Sie Ihre Assets mit „Auswahl exportieren" sichern.

## Häufige Fragen

- **RDP-Umleitung ist aktiviert, wirkt aber nicht?** Nach neueren Windows-Sicherheitsupdates wird beim Öffnen einer unsignierten `.rdp` zuerst ein Sicherheitsdialog angezeigt, in dem Clipboard, Laufwerke, Drucker und andere Umleitungen standardmäßig alle deaktiviert sind; Sie müssen sie einmal manuell im Dialog aktivieren. Die Anwendung schreibt bereits Schlüssel wie `redirectclipboard:i:1` gemäß Ihrer Auswahl in die `.rdp`, aber dieser Schritt kann nicht für Sie erledigt werden. Um ihn ganz zu vermeiden, müssen Sie die `.rdp` codesignieren oder die IT einen bestimmten Herausgeber per Gruppenrichtlinie vertrauen lassen.
- **Der RDP-Fenstertitel zeigt „Name (IP)"?** Der mstsc-Titel stammt aus dem Dateinamen der `.rdp`, und die Anwendung benennt sie „Asset-Name (Hostadresse).rdp".
- **Nach dem Verbinden mit VNC zeigt der Arbeitsbereich eine Platzhalterseite?** Das entfernte Bild wird im eigenen Fenster des lokalen VNC-Viewers angezeigt; die Anwendung enthält keinen integrierten VNC-Client.
- **SSH-Passwort falsch?** Die Anwendung fordert zur erneuten Eingabe auf und verbindet neu; bei demselben Asset wird nur einmal nachgefragt, um wiederholte Pop-ups zu vermeiden.
- **Verbindung zum Jump-Host nicht möglich?** Das Jump-Host-Asset selbst muss ein nutzbares Passwort oder einen Private Key haben; fehlen beide, schlägt die Verbindung schon vor dem Start fehl.

Eine ausführlichere Beschreibung finden Sie auf der Seite „Hilfehandbuch" in der Anwendung.

## Drittanbieter-Abhängigkeiten

| Komponente | Lizenz |
| --- | --- |
| [ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader) | MIT |
| [SSH.NET](https://github.com/sshnet/SSH.NET) | MIT |
| [BouncyCastle.Cryptography](https://www.bouncycastle.org/) | MIT |
| Microsoft.Extensions.Logging.Abstractions | MIT |
| [VirtualTerminal.WPF](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |
| [VirtualTerminal.SecureShell](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |

Alle sind freizügige Open-Source-Lizenzen, ohne kommerzielle Komponenten und kompatibel mit Apache 2.0.

## Codesignierung

Die Release-Pakete sind derzeit unsigniert. Geplant ist eine kostenlose Windows-Codesignierung über die SignPath Foundation; Details siehe [CODE_SIGNING.md](../CODE_SIGNING.md).

## Sponsoring

Der GitHub-Sponsors-Antrag wurde noch nicht eingereicht (der Maintainer muss unter github.com/sponsors beantragen und Stripe für Auszahlungen anbinden). Das Repository ist bereits mit `.github/FUNDING.yml` konfiguriert (`github: [OpenNodeSaas]`); nach der Genehmigung erscheint auf der Repository-Seite eine Sponsor-Schaltfläche.

## Feedback

- E-Mail: remotemate@protonmail.com
- Issue: <https://github.com/OpenNodeSaas/OpenNodeDesk/issues>

## Lizenz

Dieses Projekt ist unter der [Apache License 2.0](../LICENSE) quelloffen.
