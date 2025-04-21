using Insomniac.Calculator.Data.Models;

namespace Insomniac.Calculator.Services.Models
{
    public sealed class CalculateResult(CalculatorType calculator, decimal tax)
    {
        public CalculatorType Calculator { get; set; } = calculator;

        public decimal Tax { get; set; } = tax;
    }
}