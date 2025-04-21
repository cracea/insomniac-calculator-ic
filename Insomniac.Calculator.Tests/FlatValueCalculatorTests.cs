using NUnit.Framework;
using Insomniac.Calculator.Data.Models;
using Insomniac.Calculator.Services.Calculators;
using Insomniac.Calculator.Services.Models;

namespace Insomniac.Calculator.Tests
{
    [TestFixture]
    internal sealed class FlatValueCalculatorTests
    {
        private FlatValueCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new FlatValueCalculator();
        }

        [TestCase(-1, 0)]
        [TestCase(0, 0)]
        [TestCase(100, 5)]
        [TestCase(255.55, 12.78)]
        [TestCase(199999, 9999.95)]
        [TestCase(200000, 10000)]
        [TestCase(200001, 10000)]
        [TestCase(6000000, 10000)]
        [TestCase(199999.99, 10000.00)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange
            var calcSettings = new List<CalculatorSetting>
            {
                new ()
                {
                    Calculator = _calculator.CalculatorType,
                    RateType = RateType.Percentage,
                    Rate = 5m,
                    From = 0,
                    To = 199999
                },
                new ()
                {
                    Calculator = _calculator.CalculatorType,
                    RateType = RateType.Amount,
                    Rate = 10000,
                    From = 200000,
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