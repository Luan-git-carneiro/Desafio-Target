using Desafio.Application.Strategies;

namespace Desafio.Domain.Models
{
    public class Venda
    {
        public required string  Vendedor { get; set; }
        public required decimal Valor { get; set; }  
   
   
        public Venda()
        {
            
        }
        public Venda( string vendedor, decimal valor)
        {
            Vendedor = vendedor;
            Valor = valor;
        }

        public decimal CalcuComissao()
        {
            return Valor switch
            {
                < 100m => new SemComissao().CalcularComissao(Valor),
                >= 100m and < 500m => new Comissao1().CalcularComissao(Valor),
                >= 500m => new Comissao5().CalcularComissao(Valor)
            };
        }

    }

    

}