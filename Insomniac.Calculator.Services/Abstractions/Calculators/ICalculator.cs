using Insomniac.Calculator.Data.Models;
using Insomniac.Calculator.Services.Models;

namespace Insomniac.Calculator.Services.Abstractions.Calculators
{
    public interface ICalculator
    {
        Task<CalculateResult> CalculateAsync(decimal income);
        CalculatorType CalculatorType { get; }
    }
}
