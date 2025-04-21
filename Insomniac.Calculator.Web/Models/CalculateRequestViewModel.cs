using System.ComponentModel.DataAnnotations;

namespace Insomniac.Calculator.Web.Models
{
    public sealed class CalculateRequestViewModel
    {
        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; }

        [Required]
        [Range(0.001, double.MaxValue, ErrorMessage = "Income must be greater than 0")]
        public decimal Income { get; set; }
    }
}