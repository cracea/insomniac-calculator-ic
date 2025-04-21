using System.ComponentModel.DataAnnotations;

namespace Insomniac.Calculator.Data.Models
{
    public sealed class CalculatorHistory
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(20)]
        public required string PostalCode { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal Income { get; set; }

        public decimal Tax { get; set; }

        public CalculatorType Calculator { get; set; }
    }
}