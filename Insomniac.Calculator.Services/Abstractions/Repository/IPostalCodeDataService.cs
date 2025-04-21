using Insomniac.Calculator.Data.Models;

namespace Insomniac.Calculator.Services.Abstractions.Repository
{
    public interface IPostalCodeDataService
    {
        Task<List<PostalCode>> GetPostalCodesAsync();

        Task<CalculatorType?> CalculatorTypeAsync(string code);
    }
}