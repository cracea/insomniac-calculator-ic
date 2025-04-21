using MediatR;
using Insomniac.Calculator.Services.Models;

namespace Insomniac.Calculator.Services.Mediators
{
    // The request includes the postal code and income
    // We want to return a CalculateResult as the response
    public record CalculateTaxCommand(string PostalCode, decimal Income)
        : IRequest<CalculateResult>;
}
