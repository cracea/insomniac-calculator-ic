using Insomniac.Calculator.Web.Services.Models;

namespace Insomniac.Calculator.Web.Services.Abstractions
{
    public interface ICalculatorHttpService
    {
        Task<List<PostalCode>> GetPostalCodesAsync();

        Task<PaginatedResult<CalculatorHistory>?> GetHistoryAsync(int skip = 1, int take = 10);

        Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest);
    }
}