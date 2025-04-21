using MediatR;
using Microsoft.Extensions.Logging;
using Insomniac.Calculator.Data.Models;
using Insomniac.Calculator.Services.Abstractions.Calculators;
using Insomniac.Calculator.Services.Abstractions.Repository;
using Insomniac.Calculator.Services.Exceptions;
using Insomniac.Calculator.Services.Models;

namespace Insomniac.Calculator.Services.Mediators
{
    public class CalculateTaxHandler(
        ICalculatorFactory calculatorFactory,
        IHistoryDataService historyDataService,
        ILogger<CalculateTaxHandler> logger)
                : IRequestHandler<CalculateTaxCommand, CalculateResult>
    {
        private readonly ICalculatorFactory _calculatorFactory = calculatorFactory;
        private readonly IHistoryDataService _historyDataService = historyDataService;
        private readonly ILogger<CalculateTaxHandler> _logger = logger;

        // Handle the calculateTaxCommand
        public async Task<CalculateResult> Handle(
            CalculateTaxCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the right calculator from the factory
                var calculator = await _calculatorFactory.GetCalculator(request.PostalCode);

                // Calculate the tax
                var calculationResult = await calculator.CalculateAsync(request.Income);

                // Save to history
                var historyRecord = new CalculatorHistory
                {
                    Calculator = calculationResult.Calculator,
                    Tax = calculationResult.Tax,
                    PostalCode = request.PostalCode,
                    Income = request.Income,
                    Timestamp = DateTime.UtcNow,
                };

                await _historyDataService.AddAsync(historyRecord);

                return calculationResult;
            }
            catch (CalculatorException ex)
            {
                // Log and re-throw
                _logger.LogError(ex, "CalculatorException in CalculateTaxHandler");
                throw;
            }
            catch (Exception ex)
            {
                // Log and re-throw
                _logger.LogError(ex, "Unexpected exception in CalculateTaxHandler");
                throw;
            }
        }
    }
}
