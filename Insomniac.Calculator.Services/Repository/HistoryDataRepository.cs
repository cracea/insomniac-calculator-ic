using Microsoft.EntityFrameworkCore;
using Insomniac.Calculator.Data;
using Insomniac.Calculator.Data.Models;
using Insomniac.Calculator.Services.Abstractions.Repository;
using Insomniac.Calculator.Services.Models;

namespace Insomniac.Calculator.Services.Repository
{
    internal sealed class HistoryDataRepository(CalculatorContext context) : IHistoryDataService
    {
        public async Task AddAsync(CalculatorHistory calculatorHistory)
        {
            calculatorHistory.Timestamp = DateTime.Now;

            await context.AddAsync(calculatorHistory);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Return paginated list of history items.
        /// </summary>
        /// <param name="page">Must be greater than or equal to 1</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PaginatedResult<CalculatorHistory>> GetPaginatedHistoryAsync(int skip = 0, int take = 25)
        {
            var query = context.CalculatorHistory
                .AsNoTracking()
                .AsQueryable()
                .OrderByDescending(_ => _.Timestamp);

            // Count for pagination
            var totalCount = query.CountAsync();

            // Apply pagination
            var items = query
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            
            return new PaginatedResult<CalculatorHistory>
            {
                Items = await items,
                TotalCount = await totalCount,
                Page = (skip / take) + 1,
                PageSize = take
            };

        }
    }
}