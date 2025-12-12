using Desafio.Domain.Interfaces;


namespace Desafio.Application.Strategies;


    public class SemComissao : IComissaoStrategy
    {
        public decimal CalcularComissao(decimal valorVenda)
        {
            return 0.0m;
        }
    }
