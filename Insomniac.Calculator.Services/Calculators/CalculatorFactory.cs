using Insomniac.Calculator.Data.Models;
using Insomniac.Calculator.Services.Abstractions.Calculators;
using Insomniac.Calculator.Services.Abstractions.Repository;
using Insomniac.Calculator.Services.Exceptions;

namespace Insomniac.Calculator.Services.Calculators
{
    internal class CalculatorFactory(
        IPostalCodeDataService postalCodeDataService,
        ICalculatorSettingsDataService calculatorSettingsDataService,
        IEnumerable<ICalculator> calculators) : ICalculatorFactory
    {
        private readonly IEnumerable<ICalculator> _calculators = calculators;
        private readonly IPostalCodeDataService _postalCodeDataServices = postalCodeDataService;
        private readonly ICalculatorSettingsDataService _calculatorSettingsDataService = calculatorSettingsDataService;

        /// <summary>
        /// Fetch tax-calculator by postalCode
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        /// <exception cref="CalculatorException"></exception>
        public async Task<ICalculator> GetCalculator(string code)
        {
            // Get calculatorType by postCode
            var calculatorType = await _postalCodeDataServices.CalculatorTypeAsync(code) 
                ?? throw new CalculatorException();

            return await GetCalculatorAsync(calculatorType);
        }

        /// <summary>
        /// Fetch tax-calculator by calculator type
        /// </summary>
        /// <param name="calculatorType"></param>
        /// <returns></returns>
        public async Task<ICalculator> GetCalculator(CalculatorType calculatorType)
        {
            return await GetCalculatorAsync(calculatorType);
        }

        /// <summary>
        /// Get and configure the right type of tax-calculator by calculatorType 
        /// </summary>
        /// <param name="calculatorType"></param>
        /// <returns></returns>
        /// <exception cref="CalculatorException"></exception>
        private async Task<ICalculator> GetCalculatorAsync(CalculatorType calculatorType)
        {
            // Pick from the injected calculators
            var calculator = _calculators
                .FirstOrDefault(c => c.CalculatorType == calculatorType)
                ?? throw new CalculatorException();

            // Configure calculator settings if required
            if (calculator is IConfigurableCalculator config)
            {
                // Fetch calculator settings
                var settings = await _calculatorSettingsDataService.GetSettingsAsync(calculatorType);
                config.Configure(settings);
            }

            return calculator;
        }
    }
}
