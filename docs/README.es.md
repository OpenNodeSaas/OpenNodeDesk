# OpenNodeDesk

[简体中文](../README.md) · [繁體中文](README.zh-TW.md) · [English](README.en.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [Deutsch](README.de.md) · [Français](README.fr.md) · Español · [Русский](README.ru.md) · [Português (Brasil)](README.pt-BR.md) · [Italiano](README.it.md) · [Türkçe](README.tr.md) · [Tiếng Việt](README.vi.md)

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](../LICENSE)
[![Release](https://img.shields.io/github/v/release/OpenNodeSaas/OpenNodeDesk)](https://github.com/OpenNodeSaas/OpenNodeDesk/releases)
[![Platform](https://img.shields.io/badge/platform-Windows%2010%20%2F%2011%20x64-lightgrey)](#descarga-y-ejecución)

OpenNodeDesk es un cliente de operaciones remotas gratuito, portable y con enfoque offline para profesionales de TI de Windows, construido con WPF (.NET 10). Reúne los tres tipos de conexión (RDP, SSH y VNC), el inventario de assets y los connection templates en una única aplicación local: sin cuenta y sin que los datos salgan de tu equipo.

> Antes llamado RemotePilot, renombrado el 2026-11-14. Los paquetes de las versiones v2.3.0 y anteriores todavía se llaman `RemotePilot-*.zip`, y tras descomprimirlos se ejecuta `RemotePilot.exe`; a partir de esta versión el nombre unificado es OpenNodeDesk.

## Características principales

### Conexión

- **RDP directo**: genera dinámicamente un archivo `.rdp` y se conecta mediante el `mstsc.exe` del sistema, sin pasar por ningún intermediario de terceros; admite resolución, profundidad de color, redirección de disco / portapapeles / sonido, así como ajustes rápidos de división en dos (izquierda-derecha y arriba-abajo).
- **Terminal SSH integrado**: basado en SSH.NET y VirtualTerminal.WPF, con doble autenticación por contraseña o clave privada (con prioridad para la clave privada), y admite retransmisión a través de un **jump host SSH**.
- **Transferencia de archivos SFTP**: con un clic en la barra de herramientas del terminal se abre la ventana de transferencia, donde puedes explorar directorios, subir, descargar, actualizar y saltar a una ruta; en escenarios con jump host se reutiliza el mismo túnel de reenvío.
- **VNC**: invoca el visor VNC ya instalado en el equipo (TightVNC / UltraVNC / RealVNC / TigerVNC), admite plantillas de parámetros personalizadas y detección automática de la ruta del cliente.

### Gestión de assets

- **Asset hub**: grupos, favoritos, usados recientemente, avisos de vencimiento, notas y campos personalizados, con vista de tabla / tarjetas.
- **Importar y exportar**: importación masiva desde CSV / Excel (deduplicación automática y confirmación de conflictos), exportación por campos; también puedes obtener assets desde tu propia interfaz HTTP (arreglo JSON o texto CSV).
- **Comprobación de conectividad**: sondea la alcanzabilidad de uno o varios hosts, y el resultado se escribe de vuelta en el asset.
- **Plantilla de conexión**: plantillas reutilizables de parámetros de conexión, con presets de resolución y pantalla dividida.

### Espacio de trabajo de sesiones

- Varias sesiones en la misma pantalla, con celdas adaptables; pantalla completa por celda, desconexión e indicadores de estado claros de un vistazo.
- El botón «Abrir» de la barra de herramientas permite buscar por **nombre del asset / dirección / grupo** y abrir la sesión directamente.

### Seguridad y privacidad

- **Enfoque offline**: todos los datos se guardan en el equipo local; sin cuenta, sin telemetría y sin necesidad de conexión a internet.
- **Las contraseñas no se guardan de forma predeterminada**; al marcar «Recordar contraseña» se cifran en disco con Windows DPAPI (usuario actual).
- **Protection passphrase**: puedes definir una passphrase que bloquea la aplicación al superar el tiempo de inactividad o de forma manual e inmediata; si la olvidas, puedes restablecerla con el **recovery code**. Tras 5 intentos fallidos seguidos de la passphrase solo se desactiva la entrada de la passphrase; solo tras otros 5 intentos fallidos seguidos del recovery code queda únicamente «Borrar datos locales» (que debe habilitarse antes en la configuración).

### Interfaz

- Cambio instantáneo entre tema claro / oscuro, sincronizado en todas las ventanas.
- Idioma de la interfaz: 13 opciones (简体中文, 繁體中文, English, 日本語, 한국어, Deutsch, Français, Español, Русский, Português, Italiano, Türkçe, Tiếng Việt); el cambio se aplica al instante y se recuerda.

## Descarga y ejecución

Página de releases: <https://github.com/OpenNodeSaas/OpenNodeDesk/releases>

| Paquete | Descripción |
| --- | --- |
| `OpenNodeDesk-win-x64.zip` | Versión de carpeta, arranca algo más rápido y facilita reemplazar archivos por tu cuenta |
| `OpenNodeDesk-single-win-x64.zip` | Versión de archivo único; tras descomprimir solo queda un `OpenNodeDesk.exe`, cómoda de llevar |

- Requisitos: Windows 10 / 11 (x64).
- Los paquetes son self-contained, no requieren instalar el runtime de .NET por separado; descomprime y ejecuta directamente `OpenNodeDesk.exe`, portable y sin instalación.

## Primeros pasos

1. Abre el «Asset hub», pulsa «Nuevo asset» y completa nombre, host, puerto y protocolo (RDP por defecto 3389, SSH 22, VNC 5900).
2. Los protocolos que requieren credenciales pedirán la contraseña; solo se guardará si marcas «Recordar contraseña».
3. Haz doble clic en el asset o pulsa «Conectar»; RDP abrirá la ventana de mstsc y SSH abrirá el terminal integrado.
4. Una vez establecida la conexión SSH, pulsa «Archivos SFTP» en la barra de herramientas del terminal para subir / descargar archivos.
5. Si quieres ver varias máquinas a la vez, cambia al «Espacio de trabajo de sesiones», pulsa «Abrir», busca assets y agrégalos.
6. Guarda los parámetros habituales como «Plantilla de conexión» y aplícalos directamente al crear un asset.

## Compilación

Requiere el [.NET SDK 10](https://dotnet.microsoft.com/download).

```powershell
dotnet build src\OpenNodeDesk\OpenNodeDesk.csproj -c Release
dotnet test tests\OpenNodeDesk.Tests
pwsh -File scripts\publish.ps1   # genera los dos paquetes en artifacts
```

## Datos y seguridad

- Los datos de assets se guardan en `%LOCALAPPDATA%\FastRdp\RemoteOps\assets.json`; para respaldar o migrar basta con copiar ese archivo.
- Por compatibilidad con versiones anteriores, el directorio de datos sigue usando `%LOCALAPPDATA%\FastRdp\RemoteOps\`, independiente del nombre del programa.
- Por defecto no se guardan contraseñas. Solo se guardan si marcas «Recordar contraseña», cifradas con Windows DPAPI (usuario actual), y solo pueden descifrarse bajo el mismo usuario de Windows.
- En cada conexión RDP se genera un archivo `.rdp` temporal en `%TEMP%\OpenNodeDesk\{carpeta aleatoria}\`, que contiene el campo de contraseña cifrado con DPAPI y puede borrarse con seguridad.
- Se recomienda hacer antes una copia de tus assets con «Exportar seleccionados» antes de activar la protection passphrase.

## Preguntas frecuentes

- **¿Marcaste las redirecciones de RDP pero no surten efecto?** Tras las actualizaciones de seguridad más recientes de Windows, al abrir un `.rdp` sin firmar aparece primero un cuadro de diálogo de seguridad, en el que las redirecciones de portapapeles, unidades, impresoras, etc. están todas desactivadas de forma predeterminada y debes marcarlas manualmente una vez. La aplicación ya escribe en el `.rdp` claves como `redirectclipboard:i:1` según tus marcas, pero este paso no se puede marcar en tu lugar. Para evitarlo por completo hay que firmar digitalmente el `.rdp`, o que TI confíe en el publicador indicado mediante directivas de grupo.
- **¿El título de la ventana RDP muestra «nombre (IP)»?** El título de mstsc se toma del nombre de archivo del `.rdp`, y la aplicación lo nombra como «nombre del asset (dirección del host).rdp».
- **¿Tras conectar por VNC ves una página de marcador en el espacio de trabajo?** La imagen remota la aloja la ventana propia del visor VNC local; la aplicación no incluye un cliente VNC.
- **¿Contraseña SSH incorrecta?** La aplicación pedirá reintroducirla y reconectará; para un mismo asset solo lo pide una vez, para evitar ventanas emergentes repetidas.
- **¿No puedes conectar al jump host?** El propio asset del jump host debe tener una contraseña o clave privada utilizable; si faltan ambas, la conexión falla directamente antes de empezar.

Para más detalles, consulta la página «Manual de ayuda» dentro de la aplicación.

## Dependencias de terceros

| Componente | Licencia |
| --- | --- |
| [ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader) | MIT |
| [SSH.NET](https://github.com/sshnet/SSH.NET) | MIT |
| [BouncyCastle.Cryptography](https://www.bouncycastle.org/) | MIT |
| Microsoft.Extensions.Logging.Abstractions | MIT |
| [VirtualTerminal.WPF](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |
| [VirtualTerminal.SecureShell](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |

Todas son licencias de código abierto permisivas, sin componentes comerciales, compatibles con Apache 2.0.

## Firma de código

Actualmente los paquetes no están firmados. Está previsto solicitar la firma de código gratuita de Windows a través de SignPath Foundation; consulta [CODE_SIGNING.md](../CODE_SIGNING.md).

## Patrocinio

La solicitud de GitHub Sponsors aún no se ha enviado (el mantenedor debe solicitarla en github.com/sponsors y conectar el cobro con Stripe). El repositorio ya tiene configurado `.github/FUNDING.yml` (`github: [OpenNodeSaas]`); una vez aprobado, la página del repositorio mostrará un botón Sponsor.

## Comentarios

- Correo: remotemate@protonmail.com
- Issue: <https://github.com/OpenNodeSaas/OpenNodeDesk/issues>

## Licencia

Este proyecto es de código abierto bajo la [Apache License 2.0](../LICENSE).
