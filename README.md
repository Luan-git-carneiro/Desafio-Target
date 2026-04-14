 Novíssima Gramática da Língua Portuguesa - Domingos Paschoal Cegalla
## CAPÍTULO: SINTAXE (Transcrição Literal e Integral)

---

### PÁGINA 392

"Há saudades que a gente nunca esquece." (OLEGÁRIO MARIANO)
"Escolheu a rua que o levaria ao bairro dos clubes." (FERNANDO NAMORA)
"As pessoas a que a gente se dirige sorriem." (GRACILIANO RAMOS)
"A vida me ensinou a conhecer os homens com os quais eu lido." (JOSUÉ GUIMARÃES)
"Existem coisas cujo alcance nos escapa; nem por isso deixam de existir." (IGNÁCIO LOYOLA BRANDÃO)

A oração adjetiva do primeiro exemplo restringe, limita, reduz a categoria das pedras: não são todas as pedras que não criam limo, mas só as que rolam.

**Observação:**
Não se faz pausa entre a oração principal e a adjetiva restritiva; por isso, não tem cabimento a vírgula. Há, no entanto, autores que, mesmo neste caso, usam vírgula.

* As orações adjetivas vêm precedidas de preposição (ou locução prepositiva), sempre que esta for reclamada pelo verbo que as constitui. Exemplos:
    * Este é um título a que toda moça bonita aspira. [aspirar a algo]
    * A velhinha era uma dessas pessoas às quais não se pode mentir.
    * Trouxe-lhe as frutas de que você gosta. [gostar de algo]
    * Algumas colegas com quem estudo são alunas brilhantes.
    * Havia ali pessoas por quem eu não queria ser visto.
    * Este é um ideal por que sempre lutei. [lutar por algo]
    * "A casa em que Antônia morava foi posta abaixo." (MANUEL BANDEIRA) [morar em um lugar]
    * Não desespere, recorra a Deus, em cujas mãos está a nossa vida.
    * De repente, achei-me num mundo desconhecido, desconcertante, com o qual eu nunca mantivera qualquer contato. [manter contato com algo]
    * Os doentes foram instalados num galpão, perto do qual acendemos grandes fogueiras.
    * "A doença de Margarida durou dois dias, no fim dos quais levantou-se a viúva um pouco abatida." (MACHADO DE ASSIS)

* Orações adjetivas podem estar coordenadas. Exemplos:
    * "Cerca-o uma corte que o adula e explora." (RAMALHO ORTIGÃO) [e explora = e que o explora]
    * "Não assim o panorama do mar, que é vário e a cada instante se recria." (OTTO DOS ANJOS) [Isto é: e que a cada instante se recria.]

**LISTA 52**







Este projeto reúne dois programas principais controlados pelo Program.cs, cada um com uma finalidade diferente:

Processar vendas e calcular comissão,

Gerenciar estoque com registros de movimentação.

A seguir está toda a documentação do funcionamento interno, fluxo, arquitetura e regras que cada módulo utiliza.

🚪 Entrada Principal (Program.cs)

Arquivo: Program.cs

Comportamento: exibe o menu inicial solicitando escolha entre:

1 — Programa de Vendas

2 — Programa de Estoque

Quando o usuário escolhe:

1: chama DesafioTarget.PrimeiroPrograma()

2: chama DesafioTarget.SegundoPrograma()

🛒 Programa 1 — Processador de Vendas
📥 Entrada / Leitura

O método:

ProcessadorDeVendas.ProcessarVendas()


Lê um arquivo JSON chamado Dado.json.

O caminho é obtido com:

AppContext.BaseDirectory

Como o JSON é tratado:

Usa JsonDocument para acessar a propriedade "vendas".

Extrai o array e desserializa para:

List<Venda>


com:

PropertyNameCaseInsensitive = true

🧱 Modelos Principais
Venda (Venda.cs)

Contém:

Vendedor

Valor

E possui o método:

CalcuComissao()


Esse método usa switch expression e delega o cálculo para diferentes estratégias.

Estratégias de Comissão (Strategy Pattern)

Implementam a interface:

IComissaoStrategy


E existem três estratégias:

SemComissao

Comissao1

Comissao5

Cada uma representa um cálculo diferente aplicado ao valor da venda.

📊 Agregação dos Resultados

Após o cálculo, as vendas são agrupadas:

_vendas.GroupBy(v => v.Vendedor)


Para cada grupo é criado:

new Vendedor(
    nomeDoVendedor,
    listaDeValores,
    somaDasComissoes
)


O método retorna:

List<Vendedor>


Com:

Nome

Vendas (lista de valores)

ComissaoTotal

📤 Saída do Programa 1

PrimeiroPrograma() imprime no console:

Vendedor: {v.Nome}
Comissão Total: {v.ComissaoTotal:C2}

📦 Programa 2 — Gerenciador de Estoque
📚 Repositório

Arquivo principal:

JsonProdutoRepository.cs


Ele mantém os arquivos dentro de:

AppDomain.CurrentDomain.BaseDirectory/Db


Arquivos utilizados:

estoque.json → lista de produtos (wrapper RootObject com a propriedade estoque)

movimentacoes.json → histórico das operações feitas no estoque

Métodos disponíveis:

PegarTodosProdutos()

SalvarProduto(Produto)

ObterProdutoPorCodigo(int)

RegistrarMovimentacao(Movimentacao)

⚙️ Serviço (Regras de Negócio)

Arquivo:

GerenciadorEstoqueService.cs


Responsável por centralizar a lógica do estoque.

O método:

ProcessarMovimentacao(...)


faz:

Valida a quantidade informada

Ajusta o campo Estoque do produto

Chama SalvarProduto(...)

Registra a movimentação chamando RegistrarMovimentacao(...)

🔄 Fluxo Interativo — SegundoPrograma()

Lista todos os produtos disponíveis

Usuário insere:

código

descrição

quantidade

tipo (entrada ou saída)

O método chama:

gerenciadorEstoque.ProcessarMovimentacao(...)


O console exibe o novo estoque atualizado

🏛️ Arquitetura / Padrões Usados

Strategy Pattern
Aplicado no cálculo das comissões (IComissaoStrategy + suas três implementações).

Repository Pattern (light)
Para persistência via JSON, mantendo leitura/escrita organizada.

required em modelos
Garante inicialização correta das propriedades.

AppContext.BaseDirectory / AppDomain.CurrentDomain.BaseDirectory
Usados para localizar os arquivos no ambiente de execução, sem caminhos hardcoded externos.
