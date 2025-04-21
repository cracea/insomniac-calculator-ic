using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Insomniac.Calculator.Data.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public sealed class PostalCode
    {
        [Key]
        public long Id { get; set; }
        
        [MaxLength(20)]
        public required string Code { get; set; }

        public CalculatorType Calculator { get; set; }
    }
}