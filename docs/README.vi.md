# OpenNodeDesk

[简体中文](../README.md) · [繁體中文](README.zh-TW.md) · [English](README.en.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [Deutsch](README.de.md) · [Français](README.fr.md) · [Español](README.es.md) · [Русский](README.ru.md) · [Português (Brasil)](README.pt-BR.md) · [Italiano](README.it.md) · [Türkçe](README.tr.md) · Tiếng Việt

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](../LICENSE)
[![Release](https://img.shields.io/github/v/release/OpenNodeSaas/OpenNodeDesk)](https://github.com/OpenNodeSaas/OpenNodeDesk/releases)
[![Platform](https://img.shields.io/badge/platform-Windows%2010%20%2F%2011%20x64-lightgrey)](#tải-về-và-chạy)

OpenNodeDesk là ứng dụng máy khách quản trị từ xa miễn phí, dạng di động (portable) và ưu tiên ngoại tuyến (offline-first) dành cho nhân viên vận hành Windows, được xây dựng trên WPF (.NET 10). Ứng dụng gom ba loại kết nối RDP, SSH, VNC cùng danh mục tài sản và các mẫu cấu hình kết nối vào một ứng dụng cục bộ duy nhất: không cần tài khoản, dữ liệu không rời khỏi máy.

> Trước đây có tên là RemotePilot, đổi tên vào ngày 2026-11-14. Tên tệp của các gói phát hành v2.3.0 và sớm hơn vẫn là `RemotePilot-*.zip`, sau khi giải nén chạy `RemotePilot.exe`; từ phiên bản này được thống nhất đổi tên thành OpenNodeDesk.

## Tính năng chính

### Kết nối

- **RDP kết nối trực tiếp**: tạo tệp `.rdp` động, kết nối qua `mstsc.exe` có sẵn của hệ thống, không qua bất kỳ proxy bên thứ ba nào; hỗ trợ độ phân giải, độ sâu màu, chuyển hướng ổ đĩa / clipboard / âm thanh, cùng các thiết lập nhanh bố cục chia đôi ngang hoặc dọc.
- **Terminal SSH tích hợp**: dựa trên SSH.NET và VirtualTerminal.WPF, xác thực kép bằng mật khẩu và khóa riêng (ưu tiên khóa riêng), hỗ trợ trung chuyển qua **jump host SSH**.
- **Truyền tệp SFTP**: một nút trên thanh công cụ terminal mở cửa sổ truyền tệp, có thể duyệt thư mục, tải lên, tải xuống, làm mới, chuyển đường dẫn; trong tình huống có jump host sẽ dùng lại cùng một đường hầm chuyển tiếp.
- **VNC**: gọi trình xem VNC đã cài trên máy (TightVNC / UltraVNC / RealVNC / TigerVNC), hỗ trợ mẫu tham số tùy chỉnh và tự động dò tìm đường dẫn trình khách.

### Quản lý tài sản

- **Trung tâm tài sản**: phân nhóm, yêu thích, dùng gần đây, nhắc nhở hết hạn, ghi chú và trường tùy chỉnh, hai chế độ xem dạng bảng / dạng thẻ.
- **Nhập/xuất**: nhập hàng loạt từ CSV / Excel (tự động loại trùng, xác nhận xung đột), xuất theo trường; cũng có thể lấy tài sản từ giao diện HTTP của riêng bạn (mảng JSON hoặc văn bản CSV).
- **Kiểm tra kết nối**: kiểm tra khả năng kết nối tới một máy hoặc hàng loạt máy, ghi kết quả trở lại tài sản.
- **Mẫu cấu hình kết nối**: mẫu tham số kết nối có thể tái sử dụng, hỗ trợ đặt trước độ phân giải và chia màn hình.

### Bàn làm việc phiên

- Nhiều phiên cùng màn hình, ô tự thích ứng; phóng to toàn màn hình một ô, ngắt kết nối, chỉ báo trạng thái rõ ràng trong nháy mắt.
- Nút «Mở» trên thanh công cụ cho phép tìm theo **tên tài sản / địa chỉ / nhóm** để mở phiên trực tiếp.

### Bảo mật và quyền riêng tư

- **Ưu tiên ngoại tuyến**: mọi dữ liệu lưu trên máy, không tài khoản, không telemetry, không yêu cầu kết nối mạng.
- **Mặc định không lưu mật khẩu**; sau khi tích «Ghi nhớ mật khẩu» sẽ được mã hóa xuống đĩa bằng Windows DPAPI (người dùng hiện tại).
- **Mật khẩu bảo vệ**: có thể đặt mật khẩu, tự khóa khi hết thời gian chờ hoặc khóa ngay thủ công; nếu quên mật khẩu có thể đặt lại bằng **mã khôi phục**. Nhập sai mật khẩu 5 lần liên tiếp chỉ vô hiệu hóa việc nhập mật khẩu, mã khôi phục sai thêm 5 lần liên tiếp nữa thì chỉ còn «Xóa dữ liệu cục bộ» (cần cho phép trước trong cài đặt).

### Giao diện

- Chuyển ngay chủ đề sáng / tối, áp dụng đồng bộ cho mọi cửa sổ.
- Ngôn ngữ giao diện: 13 lựa chọn (简体中文, 繁體中文, English, 日本語, 한국어, Deutsch, Français, Español, Русский, Português, Italiano, Türkçe, Tiếng Việt); thay đổi có hiệu lực ngay và được ghi nhớ.

## Tải về và chạy

Trang phát hành: <https://github.com/OpenNodeSaas/OpenNodeDesk/releases>

| Gói phát hành | Mô tả |
| --- | --- |
| `OpenNodeDesk-win-x64.zip` | Bản thư mục, khởi động nhanh hơn một chút, thuận tiện tự thay thế tệp |
| `OpenNodeDesk-single-win-x64.zip` | Bản một tệp, sau khi giải nén chỉ có một `OpenNodeDesk.exe`, thuận tiện mang theo |

- Yêu cầu chạy: Windows 10 / 11 (x64).
- Gói phát hành là self-contained, không cần cài thêm runtime .NET; sau khi giải nén chạy trực tiếp `OpenNodeDesk.exe`, không cần cài đặt.

## Bắt đầu nhanh

1. Mở «Trung tâm tài sản», bấm «Thêm tài sản», điền tên, máy chủ, cổng và giao thức (RDP mặc định 3389, SSH mặc định 22, VNC mặc định 5900).
2. Giao thức cần thông tin xác thực sẽ nhắc nhập mật khẩu; chỉ lưu khi tích «Ghi nhớ mật khẩu».
3. Nháy đúp tài sản hoặc bấm «Kết nối»; RDP sẽ mở cửa sổ mstsc, SSH sẽ mở terminal tích hợp.
4. Sau khi thiết lập kết nối SSH, bấm «Tệp SFTP» trên thanh công cụ terminal để tải lên / tải xuống tệp.
5. Muốn xem nhiều máy cùng lúc, chuyển sang «Bàn làm việc phiên», bấm «Mở» tìm tài sản và thêm vào.
6. Lưu tham số thường dùng thành «Mẫu cấu hình kết nối», áp dụng trực tiếp khi tạo tài sản mới.

## Xây dựng

Cần [.NET SDK 10](https://dotnet.microsoft.com/download).

```powershell
dotnet build src\OpenNodeDesk\OpenNodeDesk.csproj -c Release
dotnet test tests\OpenNodeDesk.Tests
pwsh -File scripts\publish.ps1   # tạo hai gói phát hành trong thư mục artifacts
```

## Dữ liệu và bảo mật

- Dữ liệu tài sản lưu tại `%LOCALAPPDATA%\FastRdp\RemoteOps\assets.json`, sao lưu hoặc di chuyển chỉ cần sao chép tệp này.
- Để tương thích với phiên bản cũ, thư mục dữ liệu vẫn dùng `%LOCALAPPDATA%\FastRdp\RemoteOps\`, không liên quan đến tên chương trình.
- Mặc định không lưu mật khẩu. Chỉ lưu khi bạn tích «Ghi nhớ mật khẩu», và được mã hóa bằng Windows DPAPI (người dùng hiện tại), chỉ giải mã được dưới cùng một người dùng Windows.
- Mỗi lần kết nối RDP sẽ tạo một tệp `.rdp` tạm thời trong `%TEMP%\OpenNodeDesk\{thư mục ngẫu nhiên}\`, trong đó chứa trường mật khẩu đã mã hóa bằng DPAPI, có thể xóa an toàn.
- Nên sao lưu tài sản bằng «Xuất mục đã chọn» trước khi bật mật khẩu bảo vệ.

## Câu hỏi thường gặp

- **Đã tích chuyển hướng RDP nhưng không có hiệu lực?** Sau các bản cập nhật bảo mật Windows gần đây, khi mở tệp `.rdp` chưa ký sẽ hiện hộp thoại bảo mật trước, trong đó chuyển hướng clipboard, ổ đĩa, máy in... mặc định đều tắt, phải tích thủ công một lần trong hộp thoại. Ứng dụng đã ghi các khóa như `redirectclipboard:i:1` vào `.rdp` theo lựa chọn của bạn, nhưng bước này không thể tích thay. Muốn tránh hẳn phải ký mã cho `.rdp`, hoặc để IT tin cậy nhà phát hành chỉ định qua Chính sách nhóm.
- **Tiêu đề cửa sổ RDP hiển thị «tên (IP)»?** Tiêu đề của mstsc lấy từ tên tệp `.rdp`, ứng dụng đặt tên theo «tên tài sản (địa chỉ máy chủ).rdp».
- **Kết nối VNC xong bàn làm việc chỉ là trang giữ chỗ?** Hình ảnh từ xa do cửa sổ riêng của trình xem VNC trên máy hiển thị, ứng dụng không tích hợp trình khách VNC.
- **Mật khẩu SSH sai?** Ứng dụng sẽ nhắc nhập lại và kết nối lại, mỗi tài sản chỉ nhắc một lần, tránh bật cửa sổ liên tục.
- **Jump host không kết nối được?** Bản thân tài sản jump host phải có mật khẩu hoặc khóa riêng hợp lệ, khi cả hai đều thiếu thì kết nối sẽ báo lỗi ngay từ trước khi bắt đầu.

Xem hướng dẫn đầy đủ hơn ở trang «Sổ tay trợ giúp» trong ứng dụng.

## Phụ thuộc bên thứ ba

| Thành phần | Giấy phép |
| --- | --- |
| [ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader) | MIT |
| [SSH.NET](https://github.com/sshnet/SSH.NET) | MIT |
| [BouncyCastle.Cryptography](https://www.bouncycastle.org/) | MIT |
| Microsoft.Extensions.Logging.Abstractions | MIT |
| [VirtualTerminal.WPF](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |
| [VirtualTerminal.SecureShell](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |

Tất cả đều là giấy phép mã nguồn mở lỏng, không có thành phần thương mại, tương thích với Apache 2.0.

## Ký mã

Hiện tại gói phát hành chưa được ký. Dự kiến xin ký mã Windows miễn phí qua SignPath Foundation, chi tiết xem [CODE_SIGNING.md](../CODE_SIGNING.md).

## Tài trợ

Đơn đăng ký GitHub Sponsors chưa được gửi (cần người bảo trì đăng ký tại github.com/sponsors và kết nối nhận tiền qua Stripe). Kho lưu trữ đã cấu hình `.github/FUNDING.yml` (`github: [OpenNodeSaas]`), sau khi được duyệt trang kho sẽ hiển thị nút Sponsor.

## Phản hồi

- Email: remotemate@protonmail.com
- Issue: <https://github.com/OpenNodeSaas/OpenNodeDesk/issues>

## Giấy phép

Dự án này được mã nguồn mở theo [Giấy phép Apache 2.0](../LICENSE).
