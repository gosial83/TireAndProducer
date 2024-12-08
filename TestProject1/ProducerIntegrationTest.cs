using System.Net;
using System.Net.Http.Json;
using TireAndProducerAPI.ViewModels;

namespace TireAndProducerTests
{
    public class ProducerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProducerIntegrationTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllProducers()
        {
            var response = await _client.GetAsync("/api/Producer");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var producers = await response.Content.ReadFromJsonAsync<List<ProducerViewModel>>();
            Assert.NotNull(producers);
            Assert.Equal(2, producers.Count);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenProducerExists()
        {
            var response = await _client.GetAsync("/api/Producer/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenProducerNotExists()
        {
            var response = await _client.GetAsync("/api/Producer/10");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Add_ShouldReturnCreatedProducer()
        {
            var newProducer = new ProducerViewModel
            {
                Name = "New Producer",
                Class = "premium"
            };

            var response = await _client.PostAsJsonAsync("/api/Producer", newProducer);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var producer = await response.Content.ReadFromJsonAsync<ProducerViewModel>();
            Assert.NotNull(producer);
            Assert.Equal("New Producer", producer.Name);
        }

        [Fact]
        public async Task Edit_ShouldChangeProducerName()
        {
            var getResponse = await _client.GetAsync("/api/Producer/1");
            var producer = await getResponse.Content.ReadFromJsonAsync<ProducerViewModel>();
            Assert.NotNull(producer);
            Assert.Equal("Producent ABC", producer.Name);

            producer.Name = "Producent ABC updated";
            var response = await _client.PutAsJsonAsync("/api/Producer/1", producer);
            var updatedProducer = await response.Content.ReadFromJsonAsync<ProducerViewModel>();

            Assert.NotNull(updatedProducer);
            Assert.Equal("Producent ABC updated", updatedProducer.Name);
        }

        [Fact]
        public async Task Delete_ShouldDeleteProducer()
        {
            var response = await _client.DeleteAsync("/api/Producer/1");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
