using Desafio.Domain.Interfaces;

namespace Desafio.Application.Strategies;

public class Comissao5 : IComissaoStrategy
{
    public decimal CalcularComissao(decimal valorVenda)
    {
        
        return valorVenda * 0.05m; 
    }
    
}