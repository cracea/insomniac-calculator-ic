using System.ComponentModel.DataAnnotations;

namespace Insomniac.Calculator.API.Models
{
    public sealed class CalculateRequest
    {
        [Required]
        [MaxLength(20)]
        public required string PostalCode { get; set; }

        [Required]
        public decimal Income { get; set; }
    }
}