using FluentAssertions;

namespace GestaoResiduosAPI.Tests
{
    public class ColetasControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ColetasControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetColetas_ReturnsStatus200()
        {
            // Act
            var response = await _client.GetAsync("/coletas?page=1&pageSize=10");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue(); 
        }
    }
}
