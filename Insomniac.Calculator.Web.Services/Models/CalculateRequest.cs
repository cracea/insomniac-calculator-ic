namespace Insomniac.Calculator.Web.Services.Models
{
    public sealed class CalculateRequest
    {
        public required string PostalCode { get; set; }

        public decimal Income { get; set; }
    }
}