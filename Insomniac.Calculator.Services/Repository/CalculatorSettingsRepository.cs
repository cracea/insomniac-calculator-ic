using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Insomniac.Calculator.Data;
using Insomniac.Calculator.Data.Models;
using Insomniac.Calculator.Services.Abstractions.Repository;

namespace Insomniac.Calculator.Services.Repository
{
    internal sealed class CalculatorSettingsRepository(CalculatorContext context, IMemoryCache memoryCache) : ICalculatorSettingsDataService
    {
        public async Task<List<CalculatorSetting>> GetSettingsAsync(CalculatorType calculatorType)
        {
            var cacheKey = $"CalculatorSetting:{calculatorType}";

            return await memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);

                return await context.CalculatorSetting
                    .AsNoTracking()
                    .Where(_ => _.Calculator == calculatorType)
                    .ToListAsync();
            }) ?? [];
        }
    }
}