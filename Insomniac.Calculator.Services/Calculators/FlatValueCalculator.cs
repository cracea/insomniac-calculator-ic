using Insomniac.Calculator.Data.Models;
using Insomniac.Calculator.Services.Abstractions.Calculators;
using Insomniac.Calculator.Services.Exceptions;
using Insomniac.Calculator.Services.Models;

namespace Insomniac.Calculator.Services.Calculators
{
    internal sealed class FlatValueCalculator : BaseCalculator, IConfigurableCalculator
    {
        public FlatValueCalculator() : base(CalculatorType.FlatValue) { }

        private decimal _highIncomeThreshold = 0;
        private decimal _highIncomeFixedTax = 0;
        private decimal _lowIncomeTaxRate = 0;

        public override Task<CalculateResult> CalculateAsync(decimal income)
        {
            decimal totalTax;

            if (income < 0)
            {
                totalTax = 0;
            }
            else if (income >= _highIncomeThreshold)
            {
                totalTax = _highIncomeFixedTax;
            }
            else
            {
                totalTax = (income * _lowIncomeTaxRate) / 100m;
            }

            totalTax = Math.Round(totalTax, 2, MidpointRounding.AwayFromZero);
            var result = new CalculateResult(CalculatorType, totalTax);

            return Task.FromResult(result);
        }

        /// <summary>
        /// Configures the **FlatValue** calculator with data from the DB.
        /// </summary>
        /// <param name="settings">All calculator settings from the DB.</param>
        public void Configure(IEnumerable<CalculatorSetting> settings)
        {
            ArgumentNullException.ThrowIfNull(settings);

            // Filter settings for the current calculator type
            var flatValueSettings = settings
                .Where(s => s.Calculator == CalculatorType)
                .ToList();

            if (flatValueSettings.Count == 0)
            {
                throw new CalculatorConfigurationException(CalculatorType);
            }

            // Set low income setting
            _lowIncomeTaxRate = flatValueSettings
                .Find(s => s.RateType == RateType.Percentage)?.Rate 
                ?? throw new CalculatorConfigurationException(CalculatorType);

            // Set hight income settings
            var highIncomeSetting = flatValueSettings
                .Find(s => s.RateType == RateType.Amount)
                ?? throw new CalculatorConfigurationException(CalculatorType);

            _highIncomeFixedTax = highIncomeSetting.Rate;
            _highIncomeThreshold = highIncomeSetting.From;
        }
    }
}