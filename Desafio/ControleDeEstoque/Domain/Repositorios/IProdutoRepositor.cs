using Desafio.ControleDeEstoque.Domain.Entidades;
using Desafio.ControleDeEstoque.Domain.Models;

namespace Desafio.ControleDeEstoque.Domain.Repositorios;

public interface IProdutoRepositorio
{
    Produto ObterProdutoPorCodigo(int codigoProduto);
    void SalvarProduto(Produto produto);
    void RegistrarMovimentacao(Movimentacao movimentacao);
}