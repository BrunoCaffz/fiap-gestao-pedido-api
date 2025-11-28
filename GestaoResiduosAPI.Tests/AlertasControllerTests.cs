using FluentAssertions;

namespace GestaoResiduosAPI.Tests
{
    public class AlertasControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AlertasControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAlertas_ReturnsStatus200()
        {
            var response = await _client.GetAsync("/alertas");
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}
