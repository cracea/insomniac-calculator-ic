using NUnit.Framework;
using Insomniac.Calculator.Data.Models;
using Insomniac.Calculator.Services.Models;
using Insomniac.Calculators.Services.Calculators;

namespace Insomniac.Calculator.Tests
{
    [TestFixture]
    internal sealed class FlatRateCalculatorTests
    {
        private FlatRateCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new FlatRateCalculator();
        }

        [TestCase(0, 0)]
        [TestCase(-1, 0)]
        [TestCase(5, 0.88)]
        [TestCase(255.73, 44.75)]
        [TestCase(1000, 175)]
        [TestCase(999999, 174999.83)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange
            var calcSettings = new List<CalculatorSetting>
            {
                new ()
                {
                    Calculator = _calculator.CalculatorType,
                    RateType = RateType.Percentage,
                    Rate = 17.5m,
                    From = 0,
                    To = null
                },
            };

            _calculator.Configure(calcSettings);
            
            // Act
            var calculationResult = await _calculator.CalculateAsync(income);

            // Assert
            Assert.That(calculationResult, Is.InstanceOf(typeof(CalculateResult))); // make sure we receive the right result type
            Assert.That(calculationResult.Tax, Is.EqualTo(expectedTax).Within(0.0001), $"Failed for income={income}. Expected {expectedTax}, got {calculationResult.Tax}");
        }
    }
}