# OpenNodeDesk

[简体中文](../README.md) · [繁體中文](README.zh-TW.md) · [English](README.en.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [Deutsch](README.de.md) · [Français](README.fr.md) · [Español](README.es.md) · [Русский](README.ru.md) · [Português (Brasil)](README.pt-BR.md) · Italiano · [Türkçe](README.tr.md) · [Tiếng Việt](README.vi.md)

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](../LICENSE)
[![Release](https://img.shields.io/github/v/release/OpenNodeSaas/OpenNodeDesk)](https://github.com/OpenNodeSaas/OpenNodeDesk/releases)
[![Platform](https://img.shields.io/badge/platform-Windows%2010%20%2F%2011%20x64-lightgrey)](#download-ed-esecuzione)

OpenNodeDesk è un client di amministrazione remota gratuito, portabile e offline-first per operatori Windows, basato su WPF (.NET 10). Riunisce in un'unica applicazione locale tre tipi di connessione — RDP, SSH e VNC — insieme all'inventario degli asset e ai modelli di connessione: nessun account richiesto, i dati non lasciano mai il computer.

> Precedentemente noto come RemotePilot, rinominato il 2026-11-14. I pacchetti di release v2.3.0 e precedenti mantengono ancora il nome file `RemotePilot-*.zip` e si avviano eseguendo `RemotePilot.exe` dopo l'estrazione; da questa versione il nome è unificato in OpenNodeDesk.

## Caratteristiche principali

### Connessioni

- **RDP diretto**: genera dinamicamente un file `.rdp` e si connette tramite `mstsc.exe`, integrato nel sistema, senza alcun proxy di terze parti; supporta risoluzione, profondità di colore, reindirizzamento di disco / appunti / audio, oltre a preset rapidi di affiancamento o sovrapposizione.
- **Terminale SSH integrato**: basato su SSH.NET e VirtualTerminal.WPF, con doppia autenticazione tramite password e chiave privata (chiave privata prioritaria) e supporto per l'inoltro attraverso un **jump host SSH**.
- **Trasferimento file SFTP**: un pulsante nella barra degli strumenti del terminale apre la finestra di trasferimento, dove puoi sfogliare le directory, caricare, scaricare, aggiornare e cambiare percorso; negli scenari con jump host viene riutilizzato lo stesso tunnel di inoltro.
- **VNC**: richiama il VNC viewer installato localmente (TightVNC / UltraVNC / RealVNC / TigerVNC), con modelli di parametri personalizzabili e rilevamento automatico del percorso del client.

### Gestione degli asset

- **Hub degli asset**: gruppi, preferiti, usati di recente, promemoria di scadenza, note e campi personalizzati, con visualizzazione sia a tabella che a schede.
- **Importazione/esportazione**: importazione in blocco da CSV / Excel (deduplicazione automatica, conferma dei conflitti) ed esportazione per campo; puoi anche recuperare gli asset dalla tua interfaccia HTTP (array JSON o testo CSV).
- **Test di connettività**: verifica la raggiungibilità di un singolo host o di più host in blocco e riscrive i risultati nell'asset.
- **Modelli di connessione**: modelli riutilizzabili di parametri di connessione, con preset di risoluzione e suddivisione dello schermo.

### Area di lavoro delle sessioni

- Più sessioni sullo stesso schermo con celle adattive; ingrandimento a schermo intero di una cella, disconnessione e indicatori di stato a colpo d'occhio.
- Il pulsante «Apri» nella barra degli strumenti consente di cercare per **nome dell'asset / indirizzo / gruppo** e aprire direttamente una sessione.

### Sicurezza e privacy

- **Offline-first**: tutti i dati sono memorizzati localmente; nessun account, nessuna telemetria, nessun requisito di connessione.
- **Le password non vengono salvate per impostazione predefinita**; se selezioni «Ricorda password», vengono cifrate su disco con Windows DPAPI (utente corrente).
- **Passphrase di protezione**: puoi impostare una passphrase per bloccare l'app dopo un timeout di inattività o manualmente all'istante; se la dimentichi, puoi reimpostarla con un **codice di recupero**. Dopo 5 passphrase errate consecutive viene disabilitato solo l'inserimento della passphrase; solo dopo altre 5 inserzioni errate consecutive del codice di recupero resta soltanto «Cancella dati locali» (da abilitare in anticipo nelle impostazioni).

### Interfaccia

- Passaggio istantaneo tra tema chiaro e scuro, applicato a tutte le finestre in modo sincronizzato.
- Lingua dell'interfaccia: 13 opzioni (简体中文, 繁體中文, English, 日本語, 한국어, Deutsch, Français, Español, Русский, Português, Italiano, Türkçe, Tiếng Việt); il cambio è immediato e viene ricordato.

## Download ed esecuzione

Pagina delle release: <https://github.com/OpenNodeSaas/OpenNodeDesk/releases>

| Pacchetto di release | Descrizione |
| --- | --- |
| `OpenNodeDesk-win-x64.zip` | Versione a cartella, avvio leggermente più rapido, comoda per sostituire i file manualmente |
| `OpenNodeDesk-single-win-x64.zip` | Versione a file singolo: dopo l'estrazione c'è solo `OpenNodeDesk.exe`, comoda da portare con sé |

- Requisiti di esecuzione: Windows 10 / 11 (x64).
- I pacchetti di release sono self-contained e non richiedono l'installazione separata del runtime .NET; dopo l'estrazione esegui direttamente `OpenNodeDesk.exe`, portabile e senza installazione.

## Guida rapida

1. Apri «Hub degli asset», fai clic su «Nuovo asset» e inserisci nome, host, porta e protocollo (RDP predefinito 3389, SSH predefinito 22, VNC predefinito 5900).
2. I protocolli che richiedono credenziali chiedono di inserire la password; questa viene salvata solo se selezioni «Ricorda password».
3. Fai doppio clic sull'asset o fai clic su «Connetti»; RDP apre una finestra mstsc, SSH apre il terminale integrato.
4. Dopo aver stabilito la connessione SSH, fai clic su «File SFTP» nella barra degli strumenti del terminale per caricare / scaricare file.
5. Per vedere più macchine contemporaneamente, passa all'«Area di lavoro delle sessioni», fai clic su «Apri» per cercare gli asset e aggiungerli.
6. Salva i parametri usati di frequente come «Modello di connessione» e applicali direttamente quando crei un nuovo asset.

## Compilazione

Richiede [.NET SDK 10](https://dotnet.microsoft.com/download).

```powershell
dotnet build src\OpenNodeDesk\OpenNodeDesk.csproj -c Release
dotnet test tests\OpenNodeDesk.Tests
pwsh -File scripts\publish.ps1   # genera i due pacchetti di release nella cartella artifacts
```

## Dati e sicurezza

- I dati degli asset sono memorizzati in `%LOCALAPPDATA%\FastRdp\RemoteOps\assets.json`; per eseguire un backup o una migrazione è sufficiente copiare questo file.
- Per compatibilità con le versioni precedenti, la directory dei dati resta `%LOCALAPPDATA%\FastRdp\RemoteOps\`, indipendentemente dal nome del programma.
- Le password non vengono salvate per impostazione predefinita. Vengono salvate solo se selezioni «Ricorda password» e sono cifrate con Windows DPAPI (utente corrente), quindi possono essere decifrate solo dallo stesso utente Windows.
- A ogni connessione RDP viene generato un file `.rdp` temporaneo in `%TEMP%\OpenNodeDesk\{cartella casuale}\`, contenente il campo della password cifrato con DPAPI; può essere eliminato in tutta sicurezza.
- Prima di abilitare la passphrase di protezione, ti consigliamo di eseguire il backup degli asset con «Esporta selezione».

## Domande frequenti

- **Hai selezionato il reindirizzamento RDP ma non funziona?** Dopo i più recenti aggiornamenti di sicurezza di Windows, all'apertura di un `.rdp` non firmato compare prima una finestra di dialogo di sicurezza in cui il reindirizzamento di appunti, unità, stampanti e così via è disattivato per impostazione predefinita e deve essere selezionato manualmente una volta nella finestra. L'applicazione ha già scritto nel `.rdp` chiavi come `redirectclipboard:i:1` in base alle tue selezioni, ma questo passaggio non può essere automatizzato. Per evitare del tutto la selezione manuale occorre firmare con un certificato il file `.rdp`, oppure far sì che l'IT reputi attendibile l'editore specificato tramite Criteri di gruppo.
- **Il titolo della finestra RDP mostra «nome (IP)»?** Il titolo di mstsc deriva dal nome del file `.rdp`; l'applicazione assegna il nome «nome asset (indirizzo host).rdp».
- **Dopo la connessione VNC l'area di lavoro mostra una pagina segnaposto?** L'immagine remota è ospitata nella finestra del VNC viewer locale; l'applicazione non include un client VNC.
- **La password SSH è errata?** L'applicazione invita a inserirla di nuovo e a riconnetterti; per lo stesso asset l'avviso compare una sola volta, evitando finestre ripetute.
- **Il jump host non si connette?** L'asset del jump host deve disporre di una password o di una chiave privata valida; se mancano entrambe, la connessione restituisce un errore prima ancora di iniziare.

Per una spiegazione più completa consulta la pagina «Manuale di aiuto» all'interno dell'applicazione.

## Dipendenze di terze parti

| Componente | Licenza |
| --- | --- |
| [ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader) | MIT |
| [SSH.NET](https://github.com/sshnet/SSH.NET) | MIT |
| [BouncyCastle.Cryptography](https://www.bouncycastle.org/) | MIT |
| Microsoft.Extensions.Logging.Abstractions | MIT |
| [VirtualTerminal.WPF](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |
| [VirtualTerminal.SecureShell](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |

Sono tutte licenze open source permissive, senza componenti commerciali, compatibili con Apache 2.0.

## Firma del codice

Attualmente i pacchetti di release non sono firmati. È previsto di richiedere la firma del codice Windows gratuita tramite SignPath Foundation; per i dettagli vedi [CODE_SIGNING.md](../CODE_SIGNING.md).

## Sponsorizzazioni

La richiesta di GitHub Sponsors non è ancora stata inviata (il manutentore deve candidarsi su github.com/sponsors e collegare l'incasso tramite Stripe). Il repository ha già configurato `.github/FUNDING.yml` (`github: [OpenNodeSaas]`); una volta approvato, nella pagina del repository comparirà il pulsante Sponsor.

## Feedback

- Email: remotemate@protonmail.com
- Issue: <https://github.com/OpenNodeSaas/OpenNodeDesk/issues>

## Licenza

Questo progetto è distribuito come open source con [Licenza Apache 2.0](../LICENSE).
