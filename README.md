# Painel em Folhas

Ferramenta para transformar uma imagem em um painel dividido em folhas para impressão e montagem. Funciona no navegador de computador ou celular e gera um PDF com páginas numeradas, sobreposição de colagem, linha de corte e uma folha-guia opcional.

## Abrir ou baixar

- **Usar no navegador:** [Painel em Folhas](https://painel-em-folhas.tontoon.chatgpt.site)
- **Baixar o aplicativo portátil para Windows:** [baixar o .exe](https://painel-em-folhas.tontoon.chatgpt.site/baixar-windows.html)

O aplicativo Windows usa Microsoft Edge WebView2 Runtime. Ele contém o HTML e a biblioteca de PDF, mas as fontes personalizadas podem depender de conexão; fontes do sistema são usadas como alternativa.

## O que faz

1. Recebe JPG, PNG, WebP ou outra imagem que o navegador consiga abrir.
2. Permite escolher círculo, quadrado ou imagem inteira e definir tamanho de 5 a 300 cm.
3. Mostra uma prévia interativa com arraste, zoom e grade das folhas.
4. Oferece A4, A3 ou Carta, orientação automática/manual, margem, sobreposição e sangria.
5. Gera o PDF no próprio dispositivo, com mapa de montagem e régua de teste quando a folha-guia está ativada.

As imagens são processadas no navegador; esta versão não envia a arte para um servidor para gerar o PDF.

## Arquivos

| Caminho | Finalidade |
| --- | --- |
| `web/index.html` | Ferramenta para abrir no navegador e publicar como site estático |
| `windows/index.html` | Variante da ferramenta que carrega o jsPDF localmente |
| `windows/jspdf.umd.min.js` | Biblioteca PDF incluída no aplicativo Windows |
| `windows/Program.cs` e `windows/PainelEmFolhas.Portatil.csproj` | Janela Windows com WebView2 e empacotamento |
| `docs/COMO-FUNCIONA.md` | Lógica da paginação, impressão e pontos de manutenção |

## Rodar e editar o site

Abra `web/index.html` em um navegador atual. O site é estático: HTML, CSS e JavaScript estão no mesmo arquivo. Para publicar em outra hospedagem estática, use esse arquivo como `index.html`.

A versão web carrega o jsPDF 2.5.1 de um CDN. Para uso sem internet, substitua o endereço do script pelo arquivo local incluído em `windows/`.

## Compilar o aplicativo Windows

Requisitos: Windows, .NET SDK 8 e pacote NuGet `Microsoft.Web.WebView2`. Dentro da pasta `windows`, execute:

```powershell
dotnet publish .\PainelEmFolhas.Portatil.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o .\release
```

O resultado será `release/PainelEmFolhas.exe`. Para abri-lo em outro computador, o Microsoft Edge WebView2 Runtime precisa estar instalado. O executável extrai os recursos internos para a pasta de dados do usuário ao iniciar; não exige instalador.

## Tamanho de impressão

O tamanho escolhido define a **linha de corte** no PDF: um painel circular de 50 cm usa diâmetro de 500 mm. A sobreposição aumenta a área repetida entre folhas e não reduz o diâmetro final. A sangria acrescenta imagem além da linha de corte.

Imprima em **100% / tamanho real**, sem “ajustar à página”, e meça a régua de 10 cm da folha-guia. Em um teste com impressão por smartphone, essa régua saiu com 9,7 cm; isso indica redução no caminho de impressão. O projeto não compensa automaticamente essa diferença, pois ela depende da impressora e das opções do aplicativo de impressão.

## Para continuar o desenvolvimento

Leia [Como funciona](docs/COMO-FUNCIONA.md) antes de alterar o cálculo das folhas ou a geração do PDF. Para mudanças de interface, preserve os IDs usados pelo JavaScript. Compare a prévia e um PDF real em ao menos uma tela pequena e uma tela desktop.

O projeto usa jsPDF 2.5.1, distribuído sob licença MIT. O arquivo incluído mantém o aviso de licença original. Este repositório ainda não define uma licença própria para o restante do código.

