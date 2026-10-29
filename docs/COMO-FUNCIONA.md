# Como funciona

## Fluxo

O HTML mantém o estado da configuração em `state` e guarda preferências de tamanho/papel no `localStorage`. A imagem enviada é desenhada em um canvas local. O arquivo original pode ser reduzido antes do PDF: o limite atual é de 8.000 pixels no maior lado e cerca de 16 milhões de pixels no total. A prévia usa outra cópia menor, de até 1.600 pixels no maior lado.

`PF.geom(state, src)` calcula a caixa física do painel em milímetros, o enquadramento da imagem e a resolução estimada. Círculo e quadrado usam `size × 10` como largura e altura; “imagem inteira” usa a largura informada e a proporção da imagem.

`PF.layout(state, geom)` calcula a área imprimível de cada folha. O avanço entre folhas é a área imprimível menos a sobreposição: `passoX = larguraImprimível − sobreposição` e `passoY = alturaImprimível − sobreposição`. O modo automático escolhe a orientação com menos folhas; em empate, mantém retrato. Para círculos, a opção de pular folhas elimina somente as que ficam totalmente fora da região com sangria.

`PF.makePdf(...)` gera a folha-guia opcional e depois uma página por folha de impressão. A arte é colocada nas coordenadas físicas do painel e recortada pelo contorno com sangria. A linha tracejada marca o corte final. A folha-guia traz o mapa numerado, as instruções e uma régua de 100 mm.

## Interface

O canvas mostra a grade e o enquadramento antes de gerar o PDF. A imagem pode ser movida com arraste e ampliada com roda/pinça ou controle de zoom. No celular, a prévia participa da rolagem normal. No desktop, a área principal ocupa a altura da janela; em janelas muito baixas, uma coluna pode ter rolagem interna.

O botão de tema troca fundo claro e escuro. A preferência é salva no navegador. A imagem de exemplo é gerada em canvas e aparece ao abrir a ferramenta.

## Textos e logos

O passo 4 abre um editor sobre a mesma prévia. Os elementos ficam em `layers`, uma lista na ordem de empilhamento (o último fica por cima). As posições e os tamanhos são guardados como **fração do painel** (`x` e `y` de 0 a 1; `fs` e `w` em relação à largura), então a composição acompanha o painel quando o diâmetro muda. Os elementos não são salvos no `localStorage`.

Os textos não usam a rasterização do navegador. O opentype.js lê a fonte e entrega os contornos dos glifos, que viram um caminho em unidades da fonte, centralizado em (0, 0). A prévia desenha esse caminho no canvas e o PDF recebe os mesmos pontos como operadores vetoriais (`m`, `l`, `c`), com o contorno branco, a borda preta e o preenchimento desenhados nessa ordem. Por isso a prévia e o PDF coincidem e a nitidez não depende do tamanho do painel.

`buildItems(g)` converte `layers` em itens já em milímetros e `PF.makePdf(..., items)` os desenha dentro do mesmo recorte da arte, na folha-guia e em cada folha que o item toca. Folhas que o item não alcança não recebem esse desenho. Logos PNG entram por `addImage` com canal alfa (máscara).

Detalhes, limites e como incluir novas fontes estão em [Editor de textos e logos](EDITOR-DE-TEXTOS.md).

## Salvamento

No site, o PDF é criado com jsPDF e baixado pelo navegador. No aplicativo Windows, a mesma lógica de download funciona dentro do WebView2. A versão Windows inclui cópias locais do jsPDF e do opentype.js; a web carrega essas bibliotecas por CDN.

## Limites conhecidos

- A régua da folha-guia é a referência para detectar redução feita pelo aplicativo de impressão ou pela impressora.
- Digitar margem zero no PDF não transforma uma impressora comum em impressora sem bordas.
- O indicador de DPI usa os pixels da imagem já processada e é uma estimativa.
- Um PDF com muitas folhas ou uma imagem grande pode consumir bastante memória no navegador.
- Fontes anexadas pelo usuário precisam ser TTF, OTF ou WOFF. WOFF2 não é lido pelo opentype.js.
- Textos e logos não têm rotação e não são salvos ao fechar a página.
- Mudanças na geometria devem ser verificadas com medidas no PDF e com uma montagem impressa; a prévia sozinha não confirma escala física.
