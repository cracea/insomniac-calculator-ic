using Microsoft.EntityFrameworkCore;
using Insomniac.Calculator.Data.Models;

namespace Insomniac.Calculator.Data
{
    public class CalculatorContext(DbContextOptions<CalculatorContext> options) : DbContext(options)
    {
        public DbSet<PostalCode> PostalCode { get; set; }
        public DbSet<CalculatorSetting> CalculatorSetting { get; set; }
        public DbSet<CalculatorHistory> CalculatorHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostalCode>();
            modelBuilder.Entity<CalculatorSetting>();
            modelBuilder.Entity<CalculatorHistory>();
        }
    }
}