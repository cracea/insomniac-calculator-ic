using Insomniac.Calculator.Data.Models;

namespace Insomniac.Calculator.API.Models
{
    public readonly struct CalculatorHistoryDto(CalculatorHistory calculatorHistory)
    {
        public string PostalCode { get; } = calculatorHistory.PostalCode;
        public DateTime Timestamp { get; } = calculatorHistory.Timestamp;
        public decimal Income { get; } = calculatorHistory.Income;
        public decimal Tax { get; } = calculatorHistory.Tax;
        public string Calculator { get; } = calculatorHistory.Calculator.ToString();
    }
}