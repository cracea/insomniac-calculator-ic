using Insomniac.Calculator.Data.Models;
using Insomniac.Calculator.Services.Abstractions.Calculators;
using Insomniac.Calculator.Services.Calculators;
using Insomniac.Calculator.Services.Exceptions;
using Insomniac.Calculator.Services.Models;

namespace Insomniac.Calculators.Services.Calculators
{
    internal sealed class FlatRateCalculator : BaseCalculator, IConfigurableCalculator
    {
        public FlatRateCalculator() : base(CalculatorType.FlatRate) { }

        // Store the fix rate percentage
        private decimal _fixTaxPercentage;

        public override Task<CalculateResult> CalculateAsync(decimal income)
        {
            // Logic for flat rate
            var totalTax = (income <= 0)
                            ? 0
                            : (income * _fixTaxPercentage) / 100m;

            totalTax = Math.Round(totalTax, 2, MidpointRounding.AwayFromZero);
            var result = new CalculateResult(this.CalculatorType, totalTax);

            // Since we don't have any asynchronous logic in the async method, we can return a FromResult object
            return Task.FromResult(result);
        }

        /// <summary>
        /// Configures the **FlatRate** calculator with data from the DB.
        /// </summary>
        /// <param name="settings">All calculator settings from the DB.</param>
        public void Configure(IEnumerable<CalculatorSetting> settings)
        {
            ArgumentNullException.ThrowIfNull(settings);

            _fixTaxPercentage = settings
                .Where(s => s.Calculator == CalculatorType)
                .First(s => s.RateType == RateType.Percentage)?.Rate
                ?? throw new CalculatorConfigurationException(CalculatorType);
        }
    }
}