using Microsoft.Extensions.DependencyInjection;
using Insomniac.Calculator.Services.Abstractions.Calculators;
using Insomniac.Calculator.Services.Abstractions.Repository;
using Insomniac.Calculator.Services.Calculators;
using Insomniac.Calculator.Services.Repository;
using Insomniac.Calculators.Services.Calculators;
using System.Reflection;

namespace Insomniac.Calculator.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCalculatorServices(this IServiceCollection services)
        {
            services.AddScoped<IPostalCodeDataService, PostalCodeDataRepository>();
            services.AddScoped<IHistoryDataService, HistoryDataRepository>();
            services.AddScoped<ICalculatorSettingsDataService, CalculatorSettingsRepository>();

            services.AddScoped<ICalculator, FlatRateCalculator>();
            services.AddScoped<ICalculator, FlatValueCalculator>();

            services.AddScoped<ICalculatorFactory, CalculatorFactory>();

            // Register mediatR services
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddMemoryCache();
        }
    }
}