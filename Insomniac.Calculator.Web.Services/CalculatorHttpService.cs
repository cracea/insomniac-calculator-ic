using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Insomniac.Calculator.Web.Services.Abstractions;
using Insomniac.Calculator.Web.Services.Models;

namespace Insomniac.Calculator.Web.Services
{
    public class CalculatorHttpService(HttpClient httpClient) : ICalculatorHttpService
    {
        private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        public async Task<List<PostalCode>> GetPostalCodesAsync()
        {
            var response = await _httpClient.GetAsync("postal-codes");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<PostalCode>>() ?? [];
        }

        public async Task<PaginatedResult<CalculatorHistory>?> GetHistoryAsync(int skip = 1, int take = 10)
        {
            var queryParams = new Dictionary<string, string?>
            {
                { "skip", skip.ToString() },
                { "take", take.ToString() },
            };

            string urlWithQuery = QueryHelpers.AddQueryString("history", queryString: queryParams);

            var response = await _httpClient.GetAsync(urlWithQuery);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PaginatedResult<CalculatorHistory>>();
        }

        public async Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("calculate-tax", calculationRequest);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CalculateResult>() 
                ?? throw new InvalidOperationException(); // deserialisation of the response has failed
        }
    }
}