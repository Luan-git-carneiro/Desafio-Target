

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
