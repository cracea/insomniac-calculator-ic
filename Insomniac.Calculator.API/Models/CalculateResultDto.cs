using Insomniac.Calculator.Services.Models;

namespace Insomniac.Calculator.API.Models
{
    public readonly struct CalculateResultDto(CalculateResult calculateResult)
    {
        public string Calculator { get; } = calculateResult.Calculator.ToString();
        public decimal Tax { get; } = calculateResult.Tax;
    }
}