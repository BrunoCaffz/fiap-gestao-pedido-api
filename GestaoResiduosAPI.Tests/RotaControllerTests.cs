using FluentAssertions;
using System.Net.Http.Json;
using GestaoResiduosAPI.ViewModels;

namespace GestaoResiduosAPI.Tests
{
    public class RotaControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public RotaControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostRota_ReturnsStatus200()
        {
            var request = new RotaOtimizadaRequest
            {
                Pontos = new List<PontoRotaViewModel>
                {
                    new PontoRotaViewModel { Id = 1, Latitude = -23.5, Longitude = -46.6 },
                    new PontoRotaViewModel { Id = 2, Latitude = -23.6, Longitude = -46.65 }
                }
            };

            var response = await _client.PostAsJsonAsync("/rota", request);

            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}
