# OpenNodeDesk

[简体中文](../README.md) · [繁體中文](README.zh-TW.md) · [English](README.en.md) · [日本語](README.ja.md) · 한국어 · [Deutsch](README.de.md) · [Français](README.fr.md) · [Español](README.es.md) · [Русский](README.ru.md) · [Português (Brasil)](README.pt-BR.md) · [Italiano](README.it.md) · [Türkçe](README.tr.md) · [Tiếng Việt](README.vi.md)

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](../LICENSE)
[![Release](https://img.shields.io/github/v/release/OpenNodeSaas/OpenNodeDesk)](https://github.com/OpenNodeSaas/OpenNodeDesk/releases)
[![Platform](https://img.shields.io/badge/platform-Windows%2010%20%2F%2011%20x64-lightgrey)](#다운로드-및-실행)

OpenNodeDesk는 Windows 운영 담당자를 위한 무료·포터블·오프라인 우선 원격 운영 클라이언트로, WPF(.NET 10) 기반으로 만들어졌습니다. RDP·SSH·VNC 세 가지 연결, 자산 인벤토리, 연결 템플릿을 하나의 로컬 앱에 담았습니다. 계정이 필요 없고, 데이터는 기기 밖으로 나가지 않습니다.

> 이전 이름은 RemotePilot이며 2026-11-14에 개명했습니다. v2.3.0 이하 릴리스 패키지의 파일명은 여전히 `RemotePilot-*.zip`이고, 압축을 풀면 `RemotePilot.exe`를 실행합니다. 이 버전부터 OpenNodeDesk로 통일했습니다.

## 주요 기능

### 연결

- **RDP 직접 연결**: `.rdp` 파일을 동적으로 생성해 OS 기본 `mstsc.exe`로 연결하며, 서드파티 프록시를 거치지 않습니다. 해상도, 색 농도, 디스크 / 클립보드 / 사운드 리디렉션, 그리고 좌우·상하 1/2 분할 단축 설정을 지원합니다.
- **내장 SSH 터미널**: SSH.NET과 VirtualTerminal.WPF 기반으로 비밀번호와 개인 키 이중 인증(개인 키 우선)을 지원하며, **SSH 점프 호스트** 경유 중계를 지원합니다.
- **SFTP 파일 전송**: 터미널 도구 모음에서 원클릭으로 전송 창을 열어 디렉터리 탐색, 업로드, 다운로드, 새로 고침, 경로 이동을 할 수 있습니다. 점프 호스트 환경에서는 동일한 포워딩 터널을 재사용합니다.
- **VNC**: 기기에 설치된 VNC 뷰어(TightVNC / UltraVNC / RealVNC / TigerVNC)를 호출하며, 사용자 지정 매개변수 템플릿과 클라이언트 경로 자동 감지를 지원합니다.

### 자산 관리

- **자산 허브**: 그룹, 즐겨찾기, 최근 사용, 만료 알림, 메모와 사용자 지정 필드, 테이블 / 카드 두 가지 보기.
- **가져오기·내보내기**: CSV / Excel 일괄 가져오기(자동 중복 제거, 충돌 확인), 필드별 내보내기. 자체 HTTP 인터페이스(JSON 배열 또는 CSV 텍스트)에서 자산을 가져올 수도 있습니다.
- **연결 확인**: 단일 또는 일괄로 호스트 도달 가능성을 확인하고 결과를 자산에 기록합니다.
- **연결 템플릿**: 재사용 가능한 연결 매개변수 템플릿으로, 해상도와 화면 분할 프리셋을 지원합니다.

### 세션 워크스페이스

- 여러 세션을 한 화면에 표시하고 셀이 자동으로 조정됩니다. 단일 셀 전체 화면, 연결 끊김, 상태 표시를 한눈에 볼 수 있습니다.
- 도구 모음의 「열기」 버튼으로 **자산 이름 / 주소 / 그룹**을 검색해 바로 세션을 열 수 있습니다.

### 보안 및 개인정보

- **오프라인 우선**: 모든 데이터가 기기에 저장되며, 계정도 텔레메트리도 네트워크 연결도 필요하지 않습니다.
- **비밀번호는 기본적으로 저장하지 않습니다**. 「비밀번호 기억」을 선택하면 Windows DPAPI(현재 사용자)로 암호화해 디스크에 저장합니다.
- **보호 패스프레이즈**: 패스프레이즈를 설정할 수 있으며, 유휴 시간 초과 또는 수동으로 즉시 잠급니다. 분실 시 **복구 코드**로 재설정할 수 있습니다. 패스프레이즈를 연속 5회 틀리면 패스프레이즈 입력만 비활성화되고, 복구 코드를 다시 연속 5회 틀린 뒤에야 「로컬 데이터 지우기」(사전에 설정에서 허용 필요)만 남습니다.

### 인터페이스

- 라이트 / 다크 테마를 즉시 전환하며 모든 창에 동기화되어 적용됩니다.
- UI 언어: 13가지(简体中文, 繁體中文, English, 日本語, 한국어, Deutsch, Français, Español, Русский, Português, Italiano, Türkçe, Tiếng Việt). 선택하면 즉시 적용되고 기억됩니다.

## 다운로드 및 실행

릴리스 페이지: <https://github.com/OpenNodeSaas/OpenNodeDesk/releases>

| 릴리스 패키지 | 설명 |
| --- | --- |
| `OpenNodeDesk-win-x64.zip` | 디렉터리 버전. 실행이 조금 빠르고 파일 교체가 간편 |
| `OpenNodeDesk-single-win-x64.zip` | 단일 파일 버전. 압축을 풀면 `OpenNodeDesk.exe` 하나뿐이라 휴대에 편리 |

- 실행 요구 사항: Windows 10 / 11(x64).
- 릴리스 패키지는 self-contained로 .NET 런타임을 따로 설치할 필요가 없습니다. 압축을 푼 뒤 바로 `OpenNodeDesk.exe`를 실행하면 되며, 설치가 필요 없는 포터블 방식입니다.

## 빠른 시작

1. 「자산 허브」를 열고 「자산 추가」를 클릭한 뒤 이름, 호스트, 포트, 프로토콜을 입력합니다(RDP 기본 3389, SSH 기본 22, VNC 기본 5900).
2. 자격 증명이 필요한 프로토콜은 비밀번호 입력을 요구합니다. 「비밀번호 기억」을 선택한 경우에만 저장됩니다.
3. 자산을 더블 클릭하거나 「연결」을 클릭하면 됩니다. RDP는 mstsc 창을 띄우고, SSH는 내장 터미널을 엽니다.
4. SSH 연결이 수립되면 터미널 도구 모음의 「SFTP 파일」에서 업로드 / 다운로드할 수 있습니다.
5. 여러 대를 동시에 보려면 「세션 워크스페이스」로 전환해 「열기」로 자산을 검색하여 추가합니다.
6. 자주 쓰는 매개변수는 「연결 템플릿」으로 저장해 두고 자산을 새로 만들 때 그대로 적용합니다.

## 빌드

[.NET SDK 10](https://dotnet.microsoft.com/download)이 필요합니다.

```powershell
dotnet build src\OpenNodeDesk\OpenNodeDesk.csproj -c Release
dotnet test tests\OpenNodeDesk.Tests
pwsh -File scripts\publish.ps1   # artifacts 아래에 두 개의 릴리스 패키지 생성
```

## 데이터와 보안

- 자산 데이터는 `%LOCALAPPDATA%\FastRdp\RemoteOps\assets.json`에 저장되며, 백업이나 이관은 이 파일을 복사하기만 하면 됩니다.
- 이전 버전과의 호환을 위해 데이터 디렉터리는 여전히 `%LOCALAPPDATA%\FastRdp\RemoteOps\`를 사용하며, 프로그램 이름과는 무관합니다.
- 기본적으로 비밀번호를 저장하지 않습니다. 「비밀번호 기억」을 선택한 경우에만 저장하며, Windows DPAPI(현재 사용자)로 암호화되어 같은 Windows 사용자에서만 복호화할 수 있습니다.
- RDP는 연결할 때마다 `%TEMP%\OpenNodeDesk\{임의 디렉터리}\`에 임시 `.rdp` 파일을 생성하며, 여기에는 DPAPI로 암호화된 비밀번호 필드가 들어 있어 안전하게 삭제할 수 있습니다.
- 보호 패스프레이즈를 활성화하기 전에 「선택 항목 내보내기」로 자산을 백업하는 것을 권장합니다.

## 자주 묻는 질문

- **RDP 리디렉션을 체크했는데 적용되지 않나요?** 최신 Windows 보안 업데이트 이후 서명되지 않은 `.rdp`를 열면 먼저 보안 대화 상자가 표시되는데, 여기서 클립보드, 드라이브, 프린터 등의 리디렉션이 기본적으로 모두 꺼져 있어 대화 상자에서 한 번 수동으로 체크해야 합니다. 앱은 사용자가 체크한 대로 `redirectclipboard:i:1` 등의 키를 `.rdp`에 기록해 두지만, 이 수동 체크만은 대신할 수 없습니다. 체크를 완전히 없애려면 `.rdp`에 코드 서명을 하거나 IT가 그룹 정책으로 특정 게시자를 신뢰해야 합니다.
- **RDP 창 제목이 「이름 (IP)」로 표시되나요?** mstsc의 제목은 `.rdp` 파일명에서 가져오며, 앱은 「자산 이름 (호스트 주소).rdp」로 이름을 짓습니다.
- **VNC 연결 후 워크스페이스에 자리 표시자 페이지가 보이나요?** 원격 화면은 기기의 VNC 뷰어 자체 창이 담당하며, 앱은 VNC 클라이언트를 내장하지 않습니다.
- **SSH 비밀번호가 틀렸나요?** 앱은 다시 입력하도록 안내하고 재연결합니다. 같은 자산에는 한 번만 알리므로 대화 상자가 반복되지 않습니다.
- **점프 호스트에 연결되지 않나요?** 점프 호스트 자산 자체에 사용 가능한 비밀번호나 개인 키가 있어야 하며, 둘 다 없으면 연결이 시작되기 전에 바로 오류가 납니다.

더 자세한 설명은 앱 내 「도움말 설명서」 페이지를 참고하세요.

## 서드파티 종속성

| 구성 요소 | 라이선스 |
| --- | --- |
| [ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader) | MIT |
| [SSH.NET](https://github.com/sshnet/SSH.NET) | MIT |
| [BouncyCastle.Cryptography](https://www.bouncycastle.org/) | MIT |
| Microsoft.Extensions.Logging.Abstractions | MIT |
| [VirtualTerminal.WPF](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |
| [VirtualTerminal.SecureShell](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |

모두 관대한 오픈 소스 라이선스이며 상용 구성 요소는 없고, Apache 2.0과 호환됩니다.

## 코드 서명

현재 릴리스 패키지는 서명되어 있지 않습니다. SignPath Foundation을 통해 무료 Windows 코드 서명을 신청할 계획이며, 자세한 내용은 [CODE_SIGNING.md](../CODE_SIGNING.md)를 참고하세요.

## 후원

GitHub Sponsors 신청은 아직 제출하지 않았습니다(유지관리자가 github.com/sponsors에서 신청하고 Stripe 수금을 연결해야 합니다). 저장소에는 `.github/FUNDING.yml`(`github: [OpenNodeSaas]`)이 설정되어 있으며, 승인되면 저장소 페이지에 Sponsor 버튼이 표시됩니다.

## 피드백

- 이메일: remotemate@protonmail.com
- Issue: <https://github.com/OpenNodeSaas/OpenNodeDesk/issues>

## 라이선스

이 프로젝트는 [Apache License 2.0](../LICENSE)으로 오픈 소스로 공개됩니다.
