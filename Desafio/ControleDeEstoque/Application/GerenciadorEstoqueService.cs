using Desafio.ControleDeEstoque.Domain.Enum;
using Desafio.ControleDeEstoque.Domain.Models;
using Desafio.ControleDeEstoque.Domain.Repositorios;

namespace Desafio.ControleDeEstoque.Application;

public class GerenciadorEstoqueService
{
    private readonly IProdutoRepositorio _produtoRepositorio;
    public GerenciadorEstoqueService(IProdutoRepositorio produtoRepositorio)
    {
        _produtoRepositorio = produtoRepositorio;
    }

    public int ProcessarMovimentacao(string descricaoMovimentacao, int codigoProduto, int quantidade, TipoMovimentacao tipoMovimentacao)
    {
        var produto = _produtoRepositorio.ObterProdutoPorCodigo(codigoProduto);

        if (produto == null)
        {
            throw new InvalidOperationException("Produto não encontrado no estoque.");
        }

        if(tipoMovimentacao == TipoMovimentacao.Saida)
        {
            produto.RegistrarSaida(quantidade);

        }
        else if (tipoMovimentacao == TipoMovimentacao.Entrada)
        {
            produto.RegistrarEntrada(quantidade);
        }
        else
        {
            throw new InvalidOperationException("Tipo de movimentação inválido.");
        }

        var movimentacao = new Movimentacao("Saida", codigoProduto ,quantidade, tipoMovimentacao);

        _produtoRepositorio.SalvarProduto(produto);

        _produtoRepositorio.RegistrarMovimentacao(movimentacao);
        Console.WriteLine($"[SUCESSO] Movimentação {movimentacao.Id}: {descricaoMovimentacao} realizada.");
         
        return produto.Estoque;
    }
}