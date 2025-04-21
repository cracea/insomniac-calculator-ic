using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Insomniac.Calculator.Data;
using Insomniac.Calculator.Data.Models;
using Insomniac.Calculator.Services.Abstractions.Repository;

namespace Insomniac.Calculator.Services.Repository
{
    internal sealed class PostalCodeDataRepository(CalculatorContext context, IMemoryCache memoryCache) : IPostalCodeDataService
    {
        public async Task<List<PostalCode>> GetPostalCodesAsync()
        {
            return await memoryCache.GetOrCreateAsync("PostalCodes", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                
                return await context.PostalCode
                    .AsNoTracking()
                    .ToListAsync();
            }) ?? [];
        }

        public async Task<CalculatorType?> CalculatorTypeAsync(string code)
        {
            // Retrieve the list of cached entities
            var postalCodes = await GetPostalCodesAsync();
            if (postalCodes.Count == 0)
                return null;

            // Filter by postalCode
            return postalCodes
                .Find(_ => _.Code == code)?.Calculator ?? null;
        }
    }
}