using System;
using System.Text.Json;
using Desafio.Application.Strategies;
using System.Linq;
using Desafio.Domain.Models;

namespace Desafio.Application.Services;

public class ProcessadorDeVendas
{
    private List<Venda>? _vendas;

    public ProcessadorDeVendas()
    {
    }

    public List<Vendedor> ProcessarVendas()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "ComissoesProjeto", "Infrastructure", "Data", "Dado.json");

        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Não foi possível localizar 'Dado.json' em: {path}");
        }

        string json = File.ReadAllText(path);

        using var doc = JsonDocument.Parse(json);
        
        if (!doc.RootElement.TryGetProperty("vendas", out var vendasElement))
        {
            _vendas = new List<Venda>();
        }
        else
        {
            var vendasJson = vendasElement.GetRawText();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _vendas = JsonSerializer.Deserialize<List<Venda>>(vendasJson, options) ?? new List<Venda>();
        }

        var vendedores = _vendas.GroupBy(v => v.Vendedor)
                                .Select(g =>
                                    new Vendedor(
                                        g.Key,
                                        g.Select(v => v.Valor).ToList(),
                                        g.Sum(v => v.CalcuComissao())
                                    )
                                )
                                .ToList();


        return vendedores;
    }
}

