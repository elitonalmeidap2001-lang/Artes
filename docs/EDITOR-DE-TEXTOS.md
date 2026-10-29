# Editor de textos e logos

Passo opcional da ferramenta (passo 4, antes de "Gerar PDF"). Serve para colocar o nome da criança, outros textos e logos PNG sobre o painel já enquadrado.

## O que o usuário faz

1. Clica em **Adicionar texto ou logo**. Os controles do painel dão lugar ao editor e a prévia continua ao lado (no celular, a prévia fica fixa no topo e o editor aparece embaixo).
2. **Texto:** digita uma ou duas linhas, escolhe a fonte, a cor, a espessura da **borda preta** e do **contorno branco**.
3. **Logo ou PNG:** anexa uma imagem, de preferência PNG com fundo transparente.
4. Move o elemento arrastando na prévia. Arrasta um canto, usa a roda do mouse ou a pinça no celular para mudar o tamanho. O tamanho é sempre proporcional; nada estica nem achata. Guias azuis marcam o centro do painel.
5. **Travar e adicionar outro texto** fixa posição e tamanho do atual e cria o próximo. Na lista, cada elemento pode ser travado, destravado ou excluído.
6. **Concluir** volta ao painel. O PDF gerado inclui todos os elementos.

Teclado: com a prévia selecionada, as setas movem (Shift move mais), `+` e `-` mudam o tamanho e Delete exclui.

## Fontes

Oito fontes vêm embutidas no `index.html` (subconjunto latino, com acentos do português): Lilita One, Luckiest Guy, Bangers, Fredoka (700), Titan One, Anton, Pacifico e Dancing Script (700). O usuário também pode anexar a própria fonte (TTF, OTF ou WOFF) pelo botão **Anexar minha fonte**. A fonte anexada vale só enquanto a página estiver aberta. WOFF2 não é aceito.

### Incluir uma nova fonte

1. Obtenha o arquivo `.woff` ou `.ttf` do subconjunto latino (os pacotes `@fontsource/<nome>` no npm trazem esses arquivos).
2. Converta para base64 (por exemplo, `base64 -w0 arquivo.woff`).
3. No `index.html`, localize `FONT_DEFS` e acrescente `{"id": "minha-fonte", "name": "Nome na lista", "b64": "..."}`.
4. Confirme a licença da fonte e guarde uma cópia em `fonts-licencas/`.

Para uma lista maior de fontes, vale guardar os arquivos em uma pasta `fonts/` e carregá-los com `fetch`, em vez de embutir tudo no HTML.

## Como o desenho funciona

Veja a seção "Textos e logos" em [Como funciona](COMO-FUNCIONA.md). Em resumo: contornos dos glifos pelo opentype.js, desenhados no canvas na prévia e como vetor no PDF.

## Limites atuais

- Não há rotação.
- Os elementos não são salvos ao fechar a página.
- No máximo 14 elementos.
- Texto em línguas que exigem formas contextuais complexas (árabe, devanágari) não é tratado.
- Se a fonte não tiver um caractere, o opentype.js desenha o símbolo de "glifo ausente".
- Verifique sempre um PDF real e a régua de 10 cm antes de imprimir um painel grande.

## Testes recomendados ao alterar o editor

- Texto com acentos e "ç" em cada fonte.
- Duas linhas, com e sem borda e contorno.
- Logo PNG com transparência.
- Painel de 30 cm e de 150 cm, comparando o PDF ampliado ao máximo (as bordas devem continuar nítidas).
- Arrastar e redimensionar com o mouse e com o dedo, em tela de computador e de celular.
