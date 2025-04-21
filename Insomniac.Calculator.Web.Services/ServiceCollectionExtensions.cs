using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Insomniac.Calculator.Web.Services.Abstractions;

namespace Insomniac.Calculator.Web.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCalculatorHttpServices(this IServiceCollection services)
        {
            services.AddScoped<ICalculatorHttpService, CalculatorHttpService>();

            services.AddHttpClient<ICalculatorHttpService, CalculatorHttpService>((serviceProvider, client) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var logger = serviceProvider.GetRequiredService<ILogger<CalculatorHttpService>>();

                // Retrieve the base URL from configuration
                string baseUrl = configuration["CalculatorSettings:ApiUrl"] 
                    ?? throw new InvalidOperationException("Calculator API base URL is not configured. Check 'CalculatorSettings:ApiUrl' in your configuration.");

                // Construct the full URI
                string uri = baseUrl.TrimEnd('/') + "/api/calculator/";
                client.BaseAddress = new Uri(uri);

                logger.LogInformation("CalculatorHttpService configured with BaseAddress: {BaseAddress}", client.BaseAddress);
            });
        }
    }
}