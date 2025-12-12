
using Desafio.Application.Services;
using Desafio.ControleDeEstoque.Application;
using Desafio.ControleDeEstoque.Domain.Entidades;
using Desafio.ControleDeEstoque.Domain.Enum;
using Desafio.ControleDeEstoque.Infraestrutura.Repositorios;

Console.WriteLine("----- Programa 1: Processador de Vendas -----");
Console.WriteLine("-------Programa 2: Gerenciador de Estoque -------");

Console.WriteLine("Digite 1 para executar o Programa 1 ou 2 para o Programa 2:");
var escolha = Console.ReadLine();
if (escolha == "1")
{
    DesafioTarget.PrimeiroPrograma();
}
else if (escolha == "2")
{
    DesafioTarget.SegundoPrograma();
}
else
{
    Console.WriteLine("Escolha inválida. Encerrando o programa.");
}

public static class DesafioTarget
{
    public static void PrimeiroPrograma()
    {
        var processador = new ProcessadorDeVendas();
        var vendedores = processador.ProcessarVendas();

        foreach (var vendedor in vendedores)
        {
            Console.WriteLine($"Vendedor: {vendedor.Nome} - Comissão Total: {vendedor.ComissaoTotal:C2}");
        }
    }
    public static void SegundoPrograma()
    {
        
        var repositorio = new JsonProdutoRepository();

        List<Produto> Produtos = repositorio.PegarTodosProdutos();

        var gerenciadorEstoque = new GerenciadorEstoqueService(repositorio);

        Console.WriteLine("Estoque Atual dos Produtos:");

        Produtos.ForEach(p => 
            Console.WriteLine($"Produto: {p.DescricaoProduto}- Codigo do Produto: {p.CodigoProduto} - Estoque Atual: {p.Estoque}")
        );

        while (true)
        {
            TipoMovimentacao tipoMovimentacao;
            
            Console.WriteLine("Digite o código do produto para Realizar uma movimentação (ou 'sair' para encerrar):");
            var inputCodigo = Console.ReadLine();
            if (inputCodigo?.ToLower() == "sair") break;

            if (!int.TryParse(inputCodigo, out int codigoProduto))
            {
                Console.WriteLine("Código inválido. Tente novamente.");
                continue;
            }
            Console.WriteLine("Digite o que essa movimentação representa:");
            var descricaoMovimentacao = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Digite a quantidade:");
            var inputQuantidade = Console.ReadLine();
            
            if (!int.TryParse(inputQuantidade, out int quantidade))
            {
                Console.WriteLine("Quantidade inválida. Tente novamente.");
                continue;
            }

            Console.WriteLine("Digite o tipo de movimentação. se(Entrada) DIGITE: 1 ou se(Saida) DIGITE: 2");
            var inputTipo = Console.ReadLine();
            
            if (inputTipo == "1" ||  inputTipo == "2")
            {
                tipoMovimentacao = inputTipo == "1" ? TipoMovimentacao.Entrada : TipoMovimentacao.Saida;
                
            }else
            {
                Console.WriteLine("Tipo de movimentação inválido. Tente novamente.");
                continue;
            }

            try
            {
                int estoqueAtualizado = gerenciadorEstoque.ProcessarMovimentacao(descricaoMovimentacao, codigoProduto, quantidade, tipoMovimentacao);
                var produtoAtual = Produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto);

                Console.WriteLine($"Estoque atualizado do produto {produtoAtual.DescricaoProduto}: {estoqueAtualizado}");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("Digite 'D' Para outra movimentação ou 'sair' para encerrar:");
                var continuar = Console.ReadLine();
                if (continuar?.ToLower() == "sair")
                {
                    break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar movimentação: {ex.Message}");
                break;
            }
        }
    }
}




