using System.Text;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional
{
    public class SalesApiFunctionalTests : IClassFixture<ApiTestFixture>
    {
        private readonly HttpClient _client;

        public SalesApiFunctionalTests(ApiTestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task PostCreateSale_ThenGetById_ShouldReturnCreatedSale()
        {
            // Arrange
            var json = @"
            {
                ""saleNumber"": ""S100"",
                ""saleDate"": ""2025-07-30T00:00:00"",
                ""clientName"": ""Cliente Functional"",
                ""branchName"": ""Filial Functional"",
                ""items"": [
                    { ""productName"": ""Produto 1"", ""quantity"": 5, ""unitPrice"": 10 }
                ]
            }";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act - Create
            var postResponse = await _client.PostAsync("/api/sales", content);
            postResponse.EnsureSuccessStatusCode();

            var createdLocation = postResponse.Headers.Location;

            // Act - Get
            var getResponse = await _client.GetAsync(createdLocation);
            getResponse.EnsureSuccessStatusCode();

            var responseString = await getResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("S100", responseString);
            Assert.Contains("Cliente Functional", responseString);
        }
    }
}