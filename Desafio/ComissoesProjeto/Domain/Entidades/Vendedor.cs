using System.Diagnostics.CodeAnalysis;

namespace Desafio.Domain.Models
{
    public class Vendedor
    {
        public required string Nome { get; set; }
        public required List<decimal> Vendas { get; set; }
        public required decimal ComissaoTotal { get; set; }  
   
   
        public Vendedor()
        {
            Vendas = new List<decimal>();
            ComissaoTotal = 0m;
            Nome = string.Empty;
        }

        [SetsRequiredMembers]
        public Vendedor(string nome, List<decimal> vendas, decimal comissao)
        {
            Nome = nome;
            Vendas = vendas;
            ComissaoTotal = comissao;
        }

    }
}