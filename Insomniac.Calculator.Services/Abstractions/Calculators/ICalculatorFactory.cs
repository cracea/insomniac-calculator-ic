namespace Insomniac.Calculator.Services.Abstractions.Calculators
{
    public interface ICalculatorFactory
    {
        Task<ICalculator> GetCalculator(string code);
    }
}
