# OpenNodeDesk

[简体中文](../README.md) · [繁體中文](README.zh-TW.md) · [English](README.en.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [Deutsch](README.de.md) · [Français](README.fr.md) · [Español](README.es.md) · [Русский](README.ru.md) · Português (Brasil) · [Italiano](README.it.md) · [Türkçe](README.tr.md) · [Tiếng Việt](README.vi.md)

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](../LICENSE)
[![Release](https://img.shields.io/github/v/release/OpenNodeSaas/OpenNodeDesk)](https://github.com/OpenNodeSaas/OpenNodeDesk/releases)
[![Platform](https://img.shields.io/badge/platform-Windows%2010%20%2F%2011%20x64-lightgrey)](#download-e-execução)

O OpenNodeDesk é um cliente de operações remotas gratuito, portátil e com foco em uso offline para profissionais de TI do Windows, criado com WPF (.NET 10). Ele reúne os três tipos de conexão (RDP, SSH e VNC), o inventário de assets e os connection templates em um único aplicativo local: sem conta e sem que os dados saiam da sua máquina.

> Antes chamado de RemotePilot, renomeado em 2026-11-14. Os pacotes das versões v2.3.0 e anteriores ainda se chamam `RemotePilot-*.zip`, e após descompactar executa-se `RemotePilot.exe`; a partir desta versão o nome unificado é OpenNodeDesk.

## Principais recursos

### Conexão

- **RDP direto**: gera dinamicamente um arquivo `.rdp` e conecta pelo `mstsc.exe` do sistema, sem intermediários de terceiros; oferece suporte a resolução, profundidade de cor, redirecionamento de disco / área de transferência / som e ajustes rápidos de divisão em dois (lado a lado e acima/abaixo).
- **Terminal SSH integrado**: baseado em SSH.NET e VirtualTerminal.WPF, com autenticação dupla por senha ou chave privada (com prioridade para a chave privada), com suporte a encaminhamento por **jump host SSH**.
- **Transferência de arquivos SFTP**: um clique na barra de ferramentas do terminal abre a janela de transferência, onde é possível navegar por diretórios, enviar, baixar, atualizar e ir para um caminho; em cenário com jump host, reaproveita o mesmo túnel de encaminhamento.
- **VNC**: chama o visualizador VNC já instalado na máquina (TightVNC / UltraVNC / RealVNC / TigerVNC), com modelos de parâmetros personalizados e detecção automática do caminho do cliente.

### Gestão de assets

- **Asset hub**: grupos, favoritos, usados recentemente, alertas de vencimento, observações e campos personalizados, com visualização em tabela / cartões.
- **Importação e exportação**: importação em massa de CSV / Excel (deduplicação automática e confirmação de conflitos), exportação por campos; também é possível buscar assets na sua própria interface HTTP (array JSON ou texto CSV).
- **Verificação de conectividade**: testa o alcance de um host ou em lote, e o resultado é gravado de volta no asset.
- **Modelo de conexão**: modelos reutilizáveis de parâmetros de conexão, com presets de resolução e tela dividida.

### Espaço de trabalho de sessões

- Várias sessões na mesma tela, com células adaptáveis; tela cheia por célula, desconexão e indicadores de status claros à primeira vista.
- O botão «Abrir» da barra de ferramentas permite pesquisar por **nome do asset / endereço / grupo** e abrir a sessão diretamente.

### Segurança e privacidade

- **Foco em offline**: todos os dados ficam na máquina local, sem conta, sem telemetria e sem exigência de conexão de rede.
- **As senhas não são salvas por padrão**; ao marcar «Lembrar senha», são criptografadas em disco com o Windows DPAPI (usuário atual).
- **Protection passphrase**: é possível definir uma passphrase que bloqueia o aplicativo por inatividade ou manualmente na hora; se você esquecê-la, pode redefinir com o **recovery code**. Após 5 erros seguidos de passphrase, apenas o campo de passphrase é desativado; só após mais 5 erros seguidos do recovery code restará apenas «Limpar dados locais» (que precisa ser permitido antes nas configurações).

### Interface

- Alternância instantânea entre tema claro / escuro, sincronizada em todas as janelas.
- Idioma da interface: 13 opções (简体中文, 繁體中文, English, 日本語, 한국어, Deutsch, Français, Español, Русский, Português, Italiano, Türkçe, Tiếng Việt); a troca é aplicada na hora e fica memorizada.

## Download e execução

Página de releases: <https://github.com/OpenNodeSaas/OpenNodeDesk/releases>

| Pacote | Descrição |
| --- | --- |
| `OpenNodeDesk-win-x64.zip` | Versão em pasta, inicia um pouco mais rápido e facilita substituir arquivos por conta própria |
| `OpenNodeDesk-single-win-x64.zip` | Versão em arquivo único; após descompactar há apenas um `OpenNodeDesk.exe`, prática de transportar |

- Requisitos: Windows 10 / 11 (x64).
- Os pacotes são self-contained, sem necessidade de instalar o runtime do .NET separadamente; descompacte e execute diretamente o `OpenNodeDesk.exe`, portátil e sem instalação.

## Primeiros passos

1. Abra o «Asset hub», clique em «Novo asset» e preencha nome, host, porta e protocolo (padrão do RDP 3389, do SSH 22 e do VNC 5900).
2. Os protocolos que exigem credenciais solicitarão a senha; ela só será salva se você marcar «Lembrar senha».
3. Dê um duplo clique no asset ou clique em «Conectar»; o RDP abrirá a janela do mstsc e o SSH, o terminal integrado.
4. Após estabelecer a conexão SSH, clique em «Arquivos SFTP» na barra de ferramentas do terminal para enviar / baixar arquivos.
5. Para ver várias máquinas ao mesmo tempo, vá para o «Espaço de trabalho de sessões», clique em «Abrir», pesquise assets e adicione-os.
6. Salve os parâmetros de uso frequente como «Modelo de conexão» e aplique-os diretamente ao criar um asset.

## Compilação

Requer o [.NET SDK 10](https://dotnet.microsoft.com/download).

```powershell
dotnet build src\OpenNodeDesk\OpenNodeDesk.csproj -c Release
dotnet test tests\OpenNodeDesk.Tests
pwsh -File scripts\publish.ps1   # gera os dois pacotes em artifacts
```

## Dados e segurança

- Os dados de assets ficam em `%LOCALAPPDATA%\FastRdp\RemoteOps\assets.json`; para backup ou migração, basta copiar esse arquivo.
- Por compatibilidade com versões anteriores, o diretório de dados continua sendo `%LOCALAPPDATA%\FastRdp\RemoteOps\`, independentemente do nome do programa.
- As senhas não são salvas por padrão. Só são salvas se você marcar «Lembrar senha», criptografadas com o Windows DPAPI (usuário atual), e só podem ser descriptografadas no mesmo usuário do Windows.
- A cada conexão RDP é gerado um arquivo `.rdp` temporário em `%TEMP%\OpenNodeDesk\{pasta aleatória}\`, que contém o campo de senha criptografado com DPAPI e pode ser excluído com segurança.
- Recomenda-se fazer um backup dos assets com «Exportar selecionados» antes de ativar a protection passphrase.

## Perguntas frequentes

- **Marcou os redirecionamentos do RDP, mas eles não funcionaram?** Após atualizações de segurança mais recentes do Windows, ao abrir um `.rdp` não assinado aparece primeiro uma caixa de diálogo de segurança, na qual os redirecionamentos de área de transferência, unidades, impressoras etc. ficam todos desativados por padrão, e você precisa marcá-los manualmente uma vez. O aplicativo já grava no `.rdp` chaves como `redirectclipboard:i:1` conforme suas marcações, mas essa etapa não pode ser marcada no seu lugar. Para dispensar completamente a marcação, é preciso assinar o `.rdp` ou que o TI confie no publicador indicado por meio de diretivas de grupo.
- **O título da janela RDP mostra «nome (IP)»?** O título do mstsc vem do nome do arquivo `.rdp`, e o aplicativo o nomeia como «nome do asset (endereço do host).rdp».
- **Depois de conectar por VNC, aparece uma página de espaço reservado no espaço de trabalho?** A imagem remota é exibida na janela do próprio visualizador VNC local; o aplicativo não inclui um cliente VNC.
- **Senha do SSH errada?** O aplicativo solicita digitá-la novamente e reconecta; para um mesmo asset, solicita apenas uma vez, evitando janelas repetidas.
- **Não consegue conectar ao jump host?** O próprio asset do jump host precisa ter uma senha ou chave privada utilizável; se faltarem ambas, a conexão falha logo no início.

Para uma descrição mais completa, consulte a página «Manual de ajuda» dentro do aplicativo.

## Dependências de terceiros

| Componente | Licença |
| --- | --- |
| [ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader) | MIT |
| [SSH.NET](https://github.com/sshnet/SSH.NET) | MIT |
| [BouncyCastle.Cryptography](https://www.bouncycastle.org/) | MIT |
| Microsoft.Extensions.Logging.Abstractions | MIT |
| [VirtualTerminal.WPF](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |
| [VirtualTerminal.SecureShell](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |

Todas são licenças de código aberto permissivas, sem componentes comerciais, compatíveis com Apache 2.0.

## Assinatura de código

No momento, os pacotes não são assinados. Está planejado solicitar a assinatura de código gratuita do Windows pelo SignPath Foundation; veja detalhes em [CODE_SIGNING.md](../CODE_SIGNING.md).

## Patrocínio

A solicitação do GitHub Sponsors ainda não foi enviada (o mantenedor precisa solicitá-la em github.com/sponsors e conectar o recebimento via Stripe). O repositório já tem o `.github/FUNDING.yml` configurado (`github: [OpenNodeSaas]`); após a aprovação, a página do repositório exibirá um botão Sponsor.

## Feedback

- E-mail: remotemate@protonmail.com
- Issue: <https://github.com/OpenNodeSaas/OpenNodeDesk/issues>

## Licença

Este projeto é de código aberto sob a [Apache License 2.0](../LICENSE).
