using Insomniac.Calculator.Data.Models;

namespace Insomniac.Calculator.Services.Exceptions
{
    public sealed class CalculatorConfigurationException(CalculatorType calculatorType) : 
        InvalidOperationException($"{calculatorType} configuration settings hasn't been provided.");
}