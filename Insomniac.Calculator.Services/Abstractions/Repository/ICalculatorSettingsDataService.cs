using Insomniac.Calculator.Data.Models;

namespace Insomniac.Calculator.Services.Abstractions.Repository
{
    public interface ICalculatorSettingsDataService
    {
        Task<List<CalculatorSetting>> GetSettingsAsync(CalculatorType calculatorType);
    }
}