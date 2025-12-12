using System.Text.Json.Serialization;

namespace Desafio.ControleDeEstoque.Domain.Entidades;

public class Produto
{
    [JsonPropertyName("codigoProduto")]
    public int CodigoProduto {get; private set; }
    [JsonPropertyName("descricaoProduto")]
    public string DescricaoProduto { get; private set; }
    
    [JsonPropertyName("estoque")]
    public int Estoque {  get; private set; }

    public Produto(int codigoProduto, string descricaoProduto, int estoque)
    {
        CodigoProduto = codigoProduto;
        DescricaoProduto = descricaoProduto;
        Estoque = estoque;
    }

    public void RegistrarSaida(int quantidade)
    {
        if(quantidade <= 0)
        {
            throw new InvalidOperationException("A quantidade de saída deve ser maior que zero.");
        }
        if(quantidade > Estoque)
        {
            throw new InvalidOperationException("A quantidade de saída não pode ser maior que a quantidade em estoque.");
        }
        Estoque -= quantidade;   
    }

    public void RegistrarEntrada(int quantidade)
    {
        if(quantidade <= 0)
        {
            throw new InvalidOperationException("A quantidade de entrada deve ser maior que zero.");
        }
        Estoque += quantidade;   
    }
}