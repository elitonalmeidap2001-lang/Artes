# Como funciona

## Fluxo

O HTML mantém o estado da configuração em `state` e guarda preferências de tamanho/papel no `localStorage`. A imagem enviada é desenhada em um canvas local. O arquivo original pode ser reduzido antes do PDF: o limite atual é de 8.000 pixels no maior lado e cerca de 16 milhões de pixels no total. A prévia usa outra cópia menor, de até 1.600 pixels no maior lado.

`PF.geom(state, src)` calcula a caixa física do painel em milímetros, o enquadramento da imagem e a resolução estimada. Círculo e quadrado usam `size × 10` como largura e altura; “imagem inteira” usa a largura informada e a proporção da imagem.

`PF.layout(state, geom)` calcula a área imprimível de cada folha. O avanço entre folhas é a área imprimível menos a sobreposição: `passoX = larguraImprimível − sobreposição` e `passoY = alturaImprimível − sobreposição`. O modo automático escolhe a orientação com menos folhas; em empate, mantém retrato. Para círculos, a opção de pular folhas elimina somente as que ficam totalmente fora da região com sangria.

`PF.makePdf(...)` gera a folha-guia opcional e depois uma página por folha de impressão. A arte é colocada nas coordenadas físicas do painel e recortada pelo contorno com sangria. A linha tracejada marca o corte final. A folha-guia traz o mapa numerado, as instruções e uma régua de 100 mm.

## Interface

O canvas mostra a grade e o enquadramento antes de gerar o PDF. A imagem pode ser movida com arraste e ampliada com roda/pinça ou controle de zoom. No celular, a prévia participa da rolagem normal. No desktop, a área principal ocupa a altura da janela; em janelas muito baixas, uma coluna pode ter rolagem interna.

O botão de tema troca fundo claro e escuro. A preferência é salva no navegador. A imagem de exemplo é gerada em canvas e aparece ao abrir a ferramenta.

## Salvamento

No site, o PDF é criado com jsPDF e baixado pelo navegador. No aplicativo Windows, a mesma lógica de download funciona dentro do WebView2. A versão Windows inclui uma cópia local do jsPDF; a web carrega essa biblioteca por CDN.

## Limites conhecidos

- A régua da folha-guia é a referência para detectar redução feita pelo aplicativo de impressão ou pela impressora.
- Digitar margem zero no PDF não transforma uma impressora comum em impressora sem bordas.
- O indicador de DPI usa os pixels da imagem já processada e é uma estimativa.
- Um PDF com muitas folhas ou uma imagem grande pode consumir bastante memória no navegador.
- Mudanças na geometria devem ser verificadas com medidas no PDF e com uma montagem impressa; a prévia sozinha não confirma escala física.
