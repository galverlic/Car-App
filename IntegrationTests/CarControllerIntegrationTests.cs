using Car_App.Data.Context; // <-- make sure to add this
using Car_App.Data.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;

namespace Car_App.Tests.Controller
{
    public class CarControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public CarControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;

            // Create a new service provider to add the in-memory database service.
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            // Create a new WebHost with the in-memory database service.
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the existing context configuration and add the in-memory database context.
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<DatabaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(serviceProvider);
                    });
                });
            }).CreateClient();
        }






        [Fact]
        public async Task GetCars_ReturnsCorrectResponse()
        {
            var response = await _client.GetAsync("/car");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<List<Car>>(stringResponse);

            Assert.NotNull(cars);
        }

        [Fact]
        public async Task GetCar_ReturnsCorrectResponse()
        {
            var id = "AEB9CFC4-6A84-4DCD-BBA8-0E24D80BFF22";
            var response = await _client.GetAsync($"/car/get-car-by-id/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                var car = JsonConvert.DeserializeObject<Car>(stringResponse);

                Assert.NotNull(car);
                Assert.Equal(id, car.Id.ToString());
            }
            else
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

    }
}
