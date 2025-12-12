using Desafio.Domain.Interfaces;

namespace Desafio.Application.Strategies;

public class Comissao1 : IComissaoStrategy
{
    public decimal CalcularComissao(decimal valorVenda)
    {
        
        return valorVenda * 0.01m; 
    }
    
}