using Insomniac.Calculator.Data.Models;
using Insomniac.Calculator.Services.Abstractions.Calculators;
using Insomniac.Calculator.Services.Models;

namespace Insomniac.Calculator.Services.Calculators
{
    internal abstract class BaseCalculator(CalculatorType calculatorType) : ICalculator
    {
        /// <summary>
        /// Make sure all calculators set up their calculator type.
        /// This is required for calculator factory when deciding which calculator to return.
        /// </summary>
        public CalculatorType CalculatorType { get; } = calculatorType;

        /// <summary>
        /// All calculators must implement the required CalculateAsync method
        /// </summary>
        /// <param name="income"></param>
        /// <returns></returns>
        public abstract Task<CalculateResult> CalculateAsync(decimal income);
    }
}
