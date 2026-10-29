# Painel em Folhas

Ferramenta para transformar uma imagem em um painel dividido em folhas para impressão e montagem. Funciona no navegador de computador ou celular e gera um PDF com páginas numeradas, sobreposição de colagem, linha de corte e uma folha-guia opcional.

## Abrir ou baixar

- **Usar no navegador:** [Painel em Folhas](https://painel-em-folhas-editor-teste.tontoon.chatgpt.site)
- **Baixar o HTML:** [web/index.html](https://raw.githubusercontent.com/elitonalmeidap2001-lang/Artes/main/web/index.html)
- **Baixar o aplicativo portátil para Windows:** [PainelEmFolhas.exe](https://github.com/elitonalmeidap2001-lang/Artes/releases/download/v1.1.0/PainelEmFolhas.exe)

O aplicativo Windows usa Microsoft Edge WebView2 Runtime. Ele contém o HTML, a biblioteca de PDF, o opentype.js e as oito fontes incluídas.

## O que faz

1. Recebe JPG, PNG, WebP ou outra imagem que o navegador consiga abrir.
2. Permite escolher círculo, quadrado ou imagem inteira e definir tamanho de 5 a 300 cm.
3. Mostra uma prévia interativa com arraste, zoom e grade das folhas.
4. Oferece A4, A3 ou Carta, orientação automática/manual, margem, sobreposição e sangria.
5. Gera o PDF no próprio dispositivo, com mapa de montagem e régua de teste quando a folha-guia está ativada.
6. Opcional: adiciona textos e logos PNG sobre o painel antes de gerar o PDF. Os textos têm fonte, cor, borda preta e contorno branco, podem ser movidos e ampliados sem deformar e saem como vetor no PDF. Veja [Editor de textos e logos](docs/EDITOR-DE-TEXTOS.md).
7. Baixa uma imagem PNG quadrada para divulgação, com arte, textos e logos, sem guias de montagem.

As imagens são processadas no navegador; esta versão não envia a arte para um servidor para gerar o PDF.

## Arquivos

| Caminho | Finalidade |
| --- | --- |
| `web/index.html` | Ferramenta para abrir no navegador e publicar como site estático |
| `windows/index.html` | Variante da ferramenta que carrega o jsPDF localmente |
| `windows/jspdf.umd.min.js` e `windows/opentype.min.js` | Bibliotecas incluídas no aplicativo Windows |
| `windows/Program.cs` e `windows/PainelEmFolhas.Portatil.csproj` | Janela Windows com WebView2 e empacotamento |
| `docs/COMO-FUNCIONA.md` | Lógica da paginação, impressão e pontos de manutenção |
| `docs/EDITOR-DE-TEXTOS.md` | Como funciona o editor de textos e logos, e como adicionar fontes |
| `docs/LICENCAS-DE-TERCEIROS.md` | Licenças do jsPDF, do opentype.js e das fontes incluídas |
| `fonts-licencas/` | Cópias dos textos de licença de cada fonte e do opentype.js |

## Rodar e editar o site

Abra `web/index.html` em um navegador atual. O site é estático: HTML, CSS e JavaScript estão no mesmo arquivo. Para publicar em outra hospedagem estática, use esse arquivo como `index.html`.

A versão web carrega o jsPDF 2.5.1 e o opentype.js 1.3.4 de CDNs. Para uso sem internet, baixe os dois arquivos e troque o endereço das tags `<script>` por caminhos locais, como a versão Windows faz.

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

O projeto usa jsPDF 2.5.1 e opentype.js 1.3.4 (ambos MIT) e oito fontes embutidas no HTML (SIL OFL 1.1, exceto Luckiest Guy, que é Apache 2.0). Os detalhes e os textos das licenças estão em [docs/LICENCAS-DE-TERCEIROS.md](docs/LICENCAS-DE-TERCEIROS.md) e em `fonts-licencas/`. Este repositório ainda não define uma licença própria para o restante do código.



