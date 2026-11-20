# OpenNodeDesk

[简体中文](../README.md) · [繁體中文](README.zh-TW.md) · [English](README.en.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [Deutsch](README.de.md) · [Français](README.fr.md) · [Español](README.es.md) · [Русский](README.ru.md) · [Português (Brasil)](README.pt-BR.md) · [Italiano](README.it.md) · Türkçe · [Tiếng Việt](README.vi.md)

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](../LICENSE)
[![Release](https://img.shields.io/github/v/release/OpenNodeSaas/OpenNodeDesk)](https://github.com/OpenNodeSaas/OpenNodeDesk/releases)
[![Platform](https://img.shields.io/badge/platform-Windows%2010%20%2F%2011%20x64-lightgrey)](#sürümü-indirme-ve-çalıştırma)

OpenNodeDesk, Windows sistem yöneticileri için WPF (.NET 10) ile geliştirilmiş ücretsiz, taşınabilir ve çevrimdışı öncelikli bir uzaktan yönetim istemcisidir. RDP, SSH ve VNC olmak üzere üç bağlantı türünü, varlık envanterini ve bağlantı modellerini tek bir yerel uygulamada toplar: hesap gerekmez, veriler bilgisayarınızdan çıkmaz.

> Önceden RemotePilot adıyla biliniyordu, 2026-11-14 tarihinde yeniden adlandırıldı. v2.3.0 ve daha eski sürüm paketlerinin dosya adları hâlâ `RemotePilot-*.zip` biçimindedir ve arşivden çıkardıktan sonra `RemotePilot.exe` çalıştırılır; bu sürümden itibaren ad birleşik olarak OpenNodeDesk oldu.

## Temel özellikler

### Bağlantılar

- **Doğrudan RDP**: dinamik olarak `.rdp` dosyası oluşturur ve sistemle gelen `mstsc.exe` üzerinden bağlanır, hiçbir üçüncü taraf proxy kullanılmaz; çözünürlük, renk derinliği, disk / pano / ses yönlendirmesinin yanı sıra yan yana ve üst üste hızlı yerleşim ayarlarını destekler.
- **Yerleşik SSH terminali**: SSH.NET ve VirtualTerminal.WPF tabanlıdır, parola ve özel anahtarla çift kimlik doğrulama (özel anahtar öncelikli) ve **SSH jump host** üzerinden aktarım desteği sunar.
- **SFTP dosya aktarımı**: terminal araç çubuğundaki tek düğme ile aktarım penceresi açılır; dizinlere göz atabilir, yükleyebilir, indirebilir, yenileyebilir ve yol değiştirebilirsiniz; jump host senaryolarında aynı yönlendirme tüneli yeniden kullanılır.
- **VNC**: bilgisayarınızda kurulu VNC görüntüleyicisini (TightVNC / UltraVNC / RealVNC / TigerVNC) çağırır; özel parametre şablonlarını ve istemci yolunun otomatik algılanmasını destekler.

### Varlık yönetimi

- **Varlık merkezi**: gruplama, favoriler, son kullanılanlar, süre bitimi hatırlatıcıları, notlar ve özel alanlar; tablo / kart olmak üzere çift görünüm.
- **İçe/dışa aktarma**: CSV / Excel ile toplu içe aktarma (otomatik yinelenen kaldırma, çakışma onayı) ve alana göre dışa aktarma; varlıkları kendi HTTP arayüzünüzden (JSON dizisi veya CSV metni) de çekebilirsiniz.
- **Bağlantı testi**: tek bir ana bilgisayarın veya toplu hâlde birden fazla ana bilgisayarın erişilebilirliğini test eder ve sonucu varlığa geri yazar.
- **Bağlantı şablonları**: yeniden kullanılabilir bağlantı parametresi şablonları; çözünürlük ve ekran bölme ön ayarlarını destekler.

### Oturum çalışma alanı

- Birden çok oturum aynı ekranda, hücreler uyarlanabilir; tek hücreyi tam ekran yapma, bağlantıyı kesme ve durum göstergeleri bir bakışta görünür.
- Araç çubuğundaki «Aç» düğmesiyle **varlık adı / adres / grup** ile arama yapıp doğrudan oturum açabilirsiniz.

### Güvenlik ve gizlilik

- **Çevrimdışı öncelikli**: tüm veriler yerel olarak saklanır; hesap yok, telemetri yok, ağ bağlantısı zorunluluğu yok.
- **Parolalar varsayılan olarak kaydedilmez**; «Parolayı hatırla» seçildiğinde Windows DPAPI (geçerli kullanıcı) ile şifrelenerek diske yazılır.
- **Koruma parolası**: bir parola belirleyerek boşta kalma zaman aşımı sonrası veya elle anında kilitleyebilirsiniz; parolayı unutursanız **kurtarma kodu** ile sıfırlayabilirsiniz. Üst üste 5 hatalı parola yalnızca parola girişini devre dışı bırakır; kurtarma kodunda da üst üste 5 hata olduktan sonra yalnızca «Yerel verileri temizle» seçeneği kalır (bunun önceden ayarlardan etkinleştirilmesi gerekir).

### Arayüz

- Açık / koyu tema anında değişir ve tüm pencerelerde eşzamanlı olarak geçerli olur.
- Arayüz dili: 13 seçenek (简体中文, 繁體中文, English, 日本語, 한국어, Deutsch, Français, Español, Русский, Português, Italiano, Türkçe, Tiếng Việt); değişiklik anında uygulanır ve hatırlanır.

## Sürümü indirme ve çalıştırma

Sürüm sayfası: <https://github.com/OpenNodeSaas/OpenNodeDesk/releases>

| Sürüm paketi | Açıklama |
| --- | --- |
| `OpenNodeDesk-win-x64.zip` | Klasör sürümü, açılış biraz daha hızlı, dosyaları kendinizinkilerle değiştirmek kolay |
| `OpenNodeDesk-single-win-x64.zip` | Tek dosya sürümü, arşivden çıkarıldığında yalnızca bir `OpenNodeDesk.exe` kalır, taşıması kolay |

- Çalıştırma gereksinimleri: Windows 10 / 11 (x64).
- Sürüm paketleri self-contained olup ayrıca .NET çalışma zamanı kurmayı gerektirmez; arşivden çıkardıktan sonra doğrudan `OpenNodeDesk.exe` çalıştırılır, kurulum gerektirmez.

## Hızlı başlangıç

1. «Varlık merkezi»ni açın, «Yeni varlık»a tıklayın ve ad, ana bilgisayar, port ile protokolü girin (RDP varsayılan 3389, SSH varsayılan 22, VNC varsayılan 5900).
2. Kimlik bilgisi gerektiren protokoller parola girmenizi ister; parola yalnızca «Parolayı hatırla» seçildiğinde kaydedilir.
3. Varlığa çift tıklayın veya «Bağlan»a tıklayın; RDP bir mstsc penceresi açar, SSH ise yerleşik terminali açar.
4. SSH bağlantısı kurulduktan sonra terminal araç çubuğundaki «SFTP dosyası»na tıklayarak dosya yükleyebilir / indirebilirsiniz.
5. Aynı anda birden çok makineyi görmek isterseniz «Oturum çalışma alanı»na geçin, «Aç»a tıklayarak varlıkları arayıp ekleyin.
6. Sık kullandığınız parametreleri «Bağlantı şablonu» olarak kaydedin ve yeni varlık oluştururken doğrudan uygulayın.

## Derleme

Gerekli: [.NET SDK 10](https://dotnet.microsoft.com/download).

```powershell
dotnet build src\OpenNodeDesk\OpenNodeDesk.csproj -c Release
dotnet test tests\OpenNodeDesk.Tests
pwsh -File scripts\publish.ps1   # artifacts altında iki sürüm paketi oluşturur
```

## Veriler ve güvenlik

- Varlık verileri `%LOCALAPPDATA%\FastRdp\RemoteOps\assets.json` içinde saklanır; yedeklemek veya taşımak için yalnızca bu dosyayı kopyalamanız yeterlidir.
- Eski sürümlerle uyumluluk için veri dizini hâlâ `%LOCALAPPDATA%\FastRdp\RemoteOps\` olarak kullanılır ve program adından bağımsızdır.
- Parolalar varsayılan olarak kaydedilmez. Yalnızca «Parolayı hatırla» seçildiğinde kaydedilir ve Windows DPAPI (geçerli kullanıcı) ile şifrelenir, dolayısıyla yalnızca aynı Windows kullanıcısı altında çözülebilir.
- Her RDP bağlantısında `%TEMP%\OpenNodeDesk\{rastgele klasör}\` içinde DPAPI ile şifrelenmiş parola alanını içeren geçici bir `.rdp` dosyası oluşturulur ve güvenle silinebilir.
- Koruma parolasını etkinleştirmeden önce varlıklarınızı «Seçilileri dışa aktar» ile yedeklemeniz önerilir.

## Sık sorulan sorular

- **RDP yönlendirmesini işaretlediniz ama geçerli olmadı mı?** Daha yeni Windows güvenlik güncellemelerinden sonra imzasız bir `.rdp` açıldığında önce bir güvenlik iletişim kutusu görünür; bu kutuda pano, sürücü, yazıcı gibi yönlendirmeler varsayılan olarak kapalıdır ve bir kez elle işaretlenmesi gerekir. Uygulama, sizin seçimlerinize göre `redirectclipboard:i:1` gibi anahtarları `.rdp` dosyasına zaten yazmıştır, ancak bu adım sizin yerinize işaretlenemez. İşaretlemeyi tamamen ortadan kaldırmak için `.rdp` dosyasını kod imzasıyla imzalamanız ya da BT'nin Grup İlkesi aracılığıyla belirtilen yayıncıya güvenmesi gerekir.
- **RDP pencere başlığında «ad (IP)» mi görünüyor?** mstsc başlığını `.rdp` dosya adından alır; uygulama «varlık adı (ana bilgisayar adresi).rdp» biçiminde adlandırır.
- **VNC bağlandıktan sonra çalışma alanında yer tutucu bir sayfa mı var?** Uzak görüntü, yerel VNC görüntüleyicisinin kendi penceresinde gösterilir; uygulama bir VNC istemcisi içermez.
- **SSH parolası yanlış mı?** Uygulama yeniden girmeniz ve yeniden bağlanmanız için uyarır; aynı varlık için yalnızca bir kez uyarır, böylece tekrar tekrar pencere açılmasını önler.
- **Jump host bağlanmıyor mu?** Jump host varlığının kendisinde geçerli bir parola veya özel anahtar olmalıdır; her ikisi de yoksa bağlantı başlamadan önce doğrudan hata verir.

Daha ayrıntılı açıklamalar için uygulama içindeki «Yardım el kitabı» sayfasına bakın.

## Üçüncü taraf bağımlılıkları

| Bileşen | Lisans |
| --- | --- |
| [ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader) | MIT |
| [SSH.NET](https://github.com/sshnet/SSH.NET) | MIT |
| [BouncyCastle.Cryptography](https://www.bouncycastle.org/) | MIT |
| Microsoft.Extensions.Logging.Abstractions | MIT |
| [VirtualTerminal.WPF](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |
| [VirtualTerminal.SecureShell](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |

Tamamı esnek açık kaynak lisanslarıdır, ticari bileşen yoktur ve Apache 2.0 ile uyumludur.

## Kod imzalama

Şu anda sürüm paketleri imzasızdır. SignPath Foundation üzerinden ücretsiz Windows kod imzalama başvurusu yapılması planlanmaktadır; ayrıntılar için bkz. [CODE_SIGNING.md](../CODE_SIGNING.md).

## Sponsorluk

GitHub Sponsors başvurusu henüz gönderilmedi (bakımcının github.com/sponsors üzerinden başvurması ve Stripe ile ödeme altyapısını bağlaması gerekir). Depoya `.github/FUNDING.yml` (`github: [OpenNodeSaas]`) zaten eklendi; onaylandıktan sonra depo sayfasında Sponsor düğmesi görünecektir.

## Geri bildirim

- E-posta: remotemate@protonmail.com
- Issue: <https://github.com/OpenNodeSaas/OpenNodeDesk/issues>

## Lisans

Bu proje [Apache License 2.0](../LICENSE) ile açık kaynak olarak sunulmaktadır.
