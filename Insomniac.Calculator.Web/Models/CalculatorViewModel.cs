using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Insomniac.Calculator.Web.Models
{
    public sealed class CalculatorViewModel
    {
        public SelectList? PostalCodes { get; set; }

        [Required(ErrorMessage = "The Postal code field is required.")]
        [StringLength(20, ErrorMessage = "Postal code must not exceed 20 characters.")]
        [Display(Name = "Postal Code", Description = "Enter your postal code.")]
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "The Income field is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Income must be greater than zero.")]
        [Display(Name = "Income", Description = "Enter your income.")]
        public decimal Income { get; set; }
    }
}