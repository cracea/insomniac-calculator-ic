using Insomniac.Calculator.Data.Models;
using Insomniac.Calculator.Services.Models;

namespace Insomniac.Calculator.Services.Abstractions.Repository
{
    public interface IHistoryDataService
    {
        Task<PaginatedResult<CalculatorHistory>> GetPaginatedHistoryAsync(int skip = 0, int take = 25);

        Task AddAsync(CalculatorHistory calculatorHistory);
    }
}