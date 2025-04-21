namespace Insomniac.Calculator.Web.Services.Models
{
    public sealed class CalculateResult
    {
        public required string Calculator { get; set; }

        public decimal Tax { get; set; }
    }
}