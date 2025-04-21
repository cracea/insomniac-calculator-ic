using System.Net.Http.Json;
using Insomniac.Calculator.Web.Services.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Insomniac.Calculator.Tests.Component
{
    [TestFixture]
    public class CalculatorControllerTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [TestCase("A100", 100000, 5000)]
        [TestCase("A100", 250000, 10000)]
        [TestCase("7000", 80000, 14000)]
        public async Task CalculateTax_ShouldReturnCorrectTax(string postalCode, decimal income, decimal expectedTax)
        {
            // Arrange
            var request = new API.Models.CalculateRequest
            {
                PostalCode = postalCode,
                Income = income
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/calculator/calculate-tax", request);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CalculateResult>();

            Assert.IsNotNull(result, "The API response should not be null.");
            Assert.That(result.Tax, Is.EqualTo(expectedTax), "The calculated tax is not as expected.");
        }
    }
}
