using Desafio.ControleDeEstoque.Domain.Enum;

namespace Desafio.ControleDeEstoque.Domain.Models;

public class Movimentacao
{
    public Guid Id { get; private set; } 
    public string Descricao { get; private set; } // <-- Adicionado conforme enunciado
    public int CodigoProduto { get; private set; }
    public int Quantidade { get; private set; }
    public DateTime DataMovimentacao { get; private set; }
    public TipoMovimentacao Tipo { get; private set; }

    public Movimentacao( string descricao, int codigoProduto, int quantidade, TipoMovimentacao tipo)
    {
        Id = Guid.NewGuid();
        Descricao = descricao;
        CodigoProduto = codigoProduto;
        Quantidade = quantidade;
        DataMovimentacao = DateTime.Now;
        Tipo = tipo;
    }
}