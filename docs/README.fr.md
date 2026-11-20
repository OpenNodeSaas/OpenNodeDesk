# OpenNodeDesk

[简体中文](../README.md) · [繁體中文](README.zh-TW.md) · [English](README.en.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [Deutsch](README.de.md) · Français · [Español](README.es.md) · [Русский](README.ru.md) · [Português (Brasil)](README.pt-BR.md) · [Italiano](README.it.md) · [Türkçe](README.tr.md) · [Tiếng Việt](README.vi.md)

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](../LICENSE)
[![Release](https://img.shields.io/github/v/release/OpenNodeSaas/OpenNodeDesk)](https://github.com/OpenNodeSaas/OpenNodeDesk/releases)
[![Platform](https://img.shields.io/badge/platform-Windows%2010%20%2F%2011%20x64-lightgrey)](#téléchargement-et-exécution)

OpenNodeDesk est un client d'exploitation à distance gratuit, portable et axé sur le hors-ligne, destiné aux administrateurs Windows, construit avec WPF (.NET 10). Il réunit les connexions RDP, SSH et VNC, l'inventaire des assets et les modèles de connexion dans une seule application locale : aucun compte requis, et les données ne quittent pas la machine.

> Anciennement nommé RemotePilot, renommé le 2026-11-14. Les paquets de publication de v2.3.0 et antérieurs portent encore le nom de fichier `RemotePilot-*.zip` et lancent `RemotePilot.exe` après extraction ; à partir de cette version, tout est nommé OpenNodeDesk.

## Fonctionnalités principales

### Connexions

- **RDP direct** : génère dynamiquement un fichier `.rdp` et se connecte via le `mstsc.exe` intégré au système, sans aucun proxy tiers ; prend en charge la résolution, la profondeur de couleur, la redirection des disques / du presse-papiers / de l'audio, ainsi que les réglages rapides d'écran partagé gauche-droite et haut-bas.
- **Terminal SSH intégré** : basé sur SSH.NET et VirtualTerminal.WPF, avec authentification par mot de passe et par clé privée (clé privée prioritaire), et prise en charge du relais par **jump host SSH**.
- **Transfert de fichiers SFTP** : ouvre la fenêtre de transfert en un clic depuis la barre d'outils du terminal ; parcourir les répertoires, téléverser, télécharger, actualiser et accéder à un chemin ; les scénarios de jump host réutilisent le même tunnel de redirection.
- **VNC** : lance un lecteur VNC installé localement (TightVNC / UltraVNC / RealVNC / TigerVNC) ; prend en charge des modèles de paramètres personnalisés et la détection automatique du chemin du client.

### Gestion des assets

- **Asset hub** : groupes, favoris, éléments récents, rappels d'expiration, notes et champs personnalisés, avec vue tableau et vue carte.
- **Import et export** : import en masse depuis CSV / Excel (déduplication automatique, confirmation des conflits), export par champ ; il est également possible de récupérer les assets depuis votre propre point d'accès HTTP (tableau JSON ou texte CSV).
- **Test de connectivité** : tester l'accessibilité d'un hôte ou de plusieurs hôtes, et réécrire le résultat dans l'asset.
- **Modèles de connexion** : modèles réutilisables de paramètres de connexion, avec préréglages de résolution et d'écran partagé.

### Espace de travail des sessions

- Plusieurs sessions sur un même écran, avec des cellules auto-adaptatives ; plein écran d'une cellule, déconnexion et indicateurs d'état visibles d'un coup d'œil.
- Le bouton « Ouvrir » de la barre d'outils permet de rechercher par **nom d'asset / adresse / groupe** et d'ouvrir directement une session.

### Sécurité et confidentialité

- **Axé sur le hors-ligne** : toutes les données sont stockées localement ; aucun compte, aucune télémétrie, aucune exigence de connexion réseau.
- **Les mots de passe ne sont pas enregistrés par défaut** ; ils ne sont chiffrés sur le disque avec Windows DPAPI (utilisateur courant) que si vous cochez « Mémoriser le mot de passe ».
- **Phrase secrète de protection** : vous pouvez définir une phrase secrète pour verrouiller après un délai d'inactivité ou verrouiller immédiatement à la main ; en cas d'oubli, vous pouvez réinitialiser avec le **code de récupération**. Après 5 erreurs consécutives de phrase secrète, seule la saisie de la phrase secrète est désactivée ; ce n'est qu'après 5 nouvelles erreurs consécutives du code de récupération qu'il ne reste plus que « Effacer les données locales » (à autoriser au préalable dans les paramètres).

### Interface

- Basculement instantané entre thème clair et thème sombre, appliqué simultanément à toutes les fenêtres.
- Langue de l'interface : 13 choix (简体中文, 繁體中文, English, 日本語, 한국어, Deutsch, Français, Español, Русский, Português, Italiano, Türkçe, Tiếng Việt) ; le changement est immédiat et mémorisé.

## Téléchargement et exécution

Page des versions : <https://github.com/OpenNodeSaas/OpenNodeDesk/releases>

| Paquet de publication | Description |
| --- | --- |
| `OpenNodeDesk-win-x64.zip` | Version en répertoire, démarre un peu plus vite et facilite le remplacement des fichiers |
| `OpenNodeDesk-single-win-x64.zip` | Version fichier unique, une seule `OpenNodeDesk.exe` après extraction, facile à transporter |

- Configuration requise : Windows 10 / 11 (x64).
- Les paquets de publication sont self-contained ; aucune installation séparée du runtime .NET n'est nécessaire. Après extraction, lancez directement `OpenNodeDesk.exe`, portable et sans installation.

## Prise en main rapide

1. Ouvrez « Asset hub » et cliquez sur « Nouvel asset », puis renseignez le nom, l'hôte, le port et le protocole (RDP : 3389 par défaut, SSH : 22, VNC : 5900).
2. Les protocoles nécessitant des identifiants demandent un mot de passe ; il n'est enregistré que si vous cochez « Mémoriser le mot de passe ».
3. Double-cliquez sur l'asset ou cliquez sur « Connecter » ; RDP ouvre une fenêtre mstsc, SSH ouvre le terminal intégré.
4. Une fois la connexion SSH établie, cliquez sur « Fichier SFTP » dans la barre d'outils du terminal pour téléverser / télécharger des fichiers.
5. Pour voir plusieurs machines à la fois, passez à « Espace de travail des sessions » et cliquez sur « Ouvrir » pour rechercher des assets et les ajouter.
6. Enregistrez les paramètres fréquents comme « Modèle de connexion » et appliquez-les directement lors de la création de nouveaux assets.

## Compilation

Nécessite le [SDK .NET 10](https://dotnet.microsoft.com/download).

```powershell
dotnet build src\OpenNodeDesk\OpenNodeDesk.csproj -c Release
dotnet test tests\OpenNodeDesk.Tests
pwsh -File scripts\publish.ps1   # génère les deux paquets de publication dans artifacts
```

## Données et sécurité

- Les données des assets sont stockées dans `%LOCALAPPDATA%\FastRdp\RemoteOps\assets.json` ; pour sauvegarder ou migrer, il suffit de copier ce fichier.
- Pour rester compatible avec les anciennes versions, le répertoire de données utilise toujours `%LOCALAPPDATA%\FastRdp\RemoteOps\`, indépendamment du nom du programme.
- Les mots de passe ne sont pas enregistrés par défaut. Ils ne sont enregistrés que si vous cochez « Mémoriser le mot de passe », et sont chiffrés avec Windows DPAPI (utilisateur courant), de sorte qu'ils ne peuvent être déchiffrés que sous le même utilisateur Windows.
- Chaque connexion RDP génère un fichier `.rdp` temporaire dans `%TEMP%\OpenNodeDesk\{random folder}\`, contenant le champ de mot de passe chiffré par DPAPI, qui peut être supprimé sans risque.
- Avant d'activer la phrase secrète de protection, il est conseillé de sauvegarder vos assets avec « Exporter la sélection ».

## Questions fréquentes

- **La redirection RDP est cochée mais ne fonctionne pas ?** Après les mises à jour de sécurité Windows récentes, l'ouverture d'un `.rdp` non signé affiche d'abord une boîte de dialogue de sécurité dans laquelle le presse-papiers, les lecteurs, les imprimantes et autres redirections sont tous désactivés par défaut ; vous devez les cocher une fois manuellement dans la boîte de dialogue. L'application écrit déjà les clés telles que `redirectclipboard:i:1` dans le `.rdp` selon vos choix, mais cette étape ne peut pas être faite à votre place. Pour l'éviter complètement, signez le `.rdp` ou faites approuver un éditeur donné par la stratégie de groupe de l'IT.
- **Le titre de la fenêtre RDP affiche « nom (IP) » ?** Le titre de mstsc provient du nom du fichier `.rdp`, et l'application le nomme « nom de l'asset (adresse de l'hôte).rdp ».
- **Après connexion VNC, l'espace de travail affiche une page de remplacement ?** L'image distante est affichée dans la propre fenêtre du lecteur VNC local ; l'application n'intègre pas de client VNC.
- **Mot de passe SSH erroné ?** L'application invite à le ressaisir et se reconnecte ; elle ne demande qu'une fois par asset pour éviter les fenêtres répétées.
- **Impossible de se connecter au jump host ?** L'asset du jump host doit lui-même disposer d'un mot de passe ou d'une clé privée utilisables ; si les deux manquent, la connexion échoue avant même de commencer.

Une description plus complète se trouve sur la page « Manuel d'aide » de l'application.

## Dépendances tierces

| Composant | Licence |
| --- | --- |
| [ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader) | MIT |
| [SSH.NET](https://github.com/sshnet/SSH.NET) | MIT |
| [BouncyCastle.Cryptography](https://www.bouncycastle.org/) | MIT |
| Microsoft.Extensions.Logging.Abstractions | MIT |
| [VirtualTerminal.WPF](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |
| [VirtualTerminal.SecureShell](https://github.com/Rikitav/VirtualTerminal) | Apache 2.0 |

Toutes sont des licences open source permissives, sans composant commercial, compatibles avec Apache 2.0.

## Signature de code

Les paquets de publication ne sont actuellement pas signés. Il est prévu de demander une signature de code Windows gratuite auprès de la SignPath Foundation ; voir [CODE_SIGNING.md](../CODE_SIGNING.md) pour plus de détails.

## Sponsoring

La demande GitHub Sponsors n'a pas encore été soumise (le mainteneur doit faire la demande sur github.com/sponsors et connecter Stripe pour les paiements). Le dépôt est déjà configuré avec `.github/FUNDING.yml` (`github: [OpenNodeSaas]`) ; une fois approuvée, un bouton Sponsor apparaîtra sur la page du dépôt.

## Commentaires

- E-mail : remotemate@protonmail.com
- Issue : <https://github.com/OpenNodeSaas/OpenNodeDesk/issues>

## Licence

Ce projet est open source sous [Apache License 2.0](../LICENSE).
