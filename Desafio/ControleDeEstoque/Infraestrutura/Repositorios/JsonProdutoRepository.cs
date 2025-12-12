using System.Text.Json; // A mágica acontece aqui
using System.Collections.Generic; // Para usar List<>
using System.Linq; // Para usar métodos de busca como RemoveAll

using Desafio.ControleDeEstoque.Domain.Repositorios;
using Desafio.ControleDeEstoque.Domain.Models;
using Desafio.ControleDeEstoque.Domain.Entidades;
using System.Text.Json.Serialization;




namespace Desafio.ControleDeEstoque.Infraestrutura.Repositorios;


public class RootObject
{
    [JsonPropertyName("estoque")]
    public List<Produto> ListaProdutos { get; set; }
}

public class JsonProdutoRepository : IProdutoRepositorio
{
    private readonly string _caminhoArquivoEstoque;
    private readonly string _caminhoArquivoMovimentacoes;

    public JsonProdutoRepository()
    {
        var pastaAtual = AppDomain.CurrentDomain.BaseDirectory;

        var pastaDb = Path.Combine(pastaAtual, "Db");

        Directory.CreateDirectory(pastaDb);

        _caminhoArquivoEstoque = Path.Combine(pastaDb, "estoque.json");
        _caminhoArquivoMovimentacoes = Path.Combine(pastaDb, "movimentacoes.json");

    }

    private List<Produto> LerTodosProdutos()
    {
        if (!File.Exists(_caminhoArquivoEstoque)) return new List<Produto>();
        try 
        {
            string json = File.ReadAllText(_caminhoArquivoEstoque);
            var root = JsonSerializer.Deserialize<RootObject>(json);
            return root?.ListaProdutos ?? new List<Produto>();
        }
        catch { return new List<Produto>(); }
    }

    public List<Produto> PegarTodosProdutos()
    {
        return LerTodosProdutos();
    }

    public void SalvarProduto(Produto produto)
    {
        var lista = LerTodosProdutos();
        var existente = lista.FirstOrDefault(p => p.CodigoProduto == produto.CodigoProduto);
        if (existente != null) lista.Remove(existente);
        lista.Add(produto);

        var root = new RootObject { ListaProdutos = lista };
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(_caminhoArquivoEstoque, JsonSerializer.Serialize(root, options));
    }

    public Produto ObterProdutoPorCodigo(int codigo)
    {
        return LerTodosProdutos().FirstOrDefault(p => p.CodigoProduto == codigo);
    }

    public void RegistrarMovimentacao(Movimentacao movimentacao)
    {
        List<Movimentacao> listaMovimentacao;
        if (File.Exists(_caminhoArquivoMovimentacoes))
        {
            try
            {
                string json = File.ReadAllText(_caminhoArquivoMovimentacoes);
                listaMovimentacao = JsonSerializer.Deserialize<List<Movimentacao>>(json) ?? new List<Movimentacao>();
            }
            catch
            {
                listaMovimentacao = new List<Movimentacao>();
            }
        }
        else
        {
            listaMovimentacao = new List<Movimentacao>();
        }

        listaMovimentacao.Add(movimentacao);

        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(_caminhoArquivoMovimentacoes, JsonSerializer.Serialize(listaMovimentacao, options));
    }



}