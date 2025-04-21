using Insomniac.Calculator.Data.Models;

namespace Insomniac.Calculator.Services.Abstractions.Calculators
{
    public interface IConfigurableCalculator
    {
        void Configure(IEnumerable<CalculatorSetting> settings);
    }
}
