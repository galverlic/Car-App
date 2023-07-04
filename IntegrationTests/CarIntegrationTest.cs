using Car_App.Controllers.DTOModels;
using Car_App.Data.Context; 
using Car_App.Data.Models;
using Car_App.Service.Interface;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Car_App.Tests.CarIntegrationTest
{
    public class CarIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly IOwnerService _userService;
        private readonly DatabaseContext _dbContext;
        private Guid CarId1 { get; set; }
        private Guid CarId5 { get; set; }

        public CarIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;

            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            // Create a new WebHost with the in-memory database service.
            var appFactory = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the existing context configuration.
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    // Add a new service provider.
                    services.AddEntityFrameworkInMemoryDatabase();

                    // Add a new context using an in-memory database for testing.
                    services.AddDbContext<DatabaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(serviceProvider);
                    });
                });
            });

            _client = appFactory.CreateClient();

            using var scope = appFactory.Server.Services.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            _userService = scope.ServiceProvider.GetRequiredService<IOwnerService>();

            SeedData();

           

            var result = _userService.AuthenticateAsync(new AuthenticateRequestDto() { Password = "PasswordHash1", Username = "UserName1" }).Result;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);


        }
        private void SeedData()
        {
            var owner1Id = Guid.Parse("d6e0b9f0-8c9d-4f7a-8f0d-7b8e6a5f4c7b");

            // Create sample owners
            var owner1 = new Owner()
            {
                Id = owner1Id,
                FirstName = "Alice",
                Email = "alice@example.com",
                Emso = "1234567890123",
                LastName = "LastName1",
                UserName = "UserName1",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("PasswordHash1"),
                TelephoneNumber = "123456789"
            };

            var owner2 = new Owner()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                Email = "bob@example.com",
                Emso = "1234567890123",
                LastName = "LastName2",
                UserName = "UserName2",
                PasswordHash = "PasswordHash2",
                TelephoneNumber = "123456789"
            };

            var owner3 = new Owner()
            {
                Id = Guid.NewGuid(),
                FirstName = "Charlie",
                Email = "charlie@example.com",
                Emso = "1234567890123",
                LastName = "LastName3",
                UserName = "UserName3",
                PasswordHash = "PasswordHash3",
                TelephoneNumber = "123456789"
            };


            // Create sample cars
            var car1 = new Car()
            {
                Id = Guid.Parse("AEB9CFC4-6A84-4DCD-BBA8-0E24D80BFF22"),
                Title = "Tesla Model 3",
                Make = "Tesla",
                Model = "Model 3",
                Year = 2020,
                Distance = 10000,
                FuelType = "Electric",
                Power = 250,
                OwnerId = owner1.Id,
                YearOfRegistration = 2020,
                YearOfFirstService = 2021
            };
            CarId1 = car1.Id;
            var car2 = new Car()
            {
                Id = Guid.NewGuid(),
                Title = "Toyota Corolla",
                Make = "Toyota",
                Model = "Corolla",
                Year = 2019,
                Distance = 20000,
                FuelType = "Gasoline",
                Power = 150,
                OwnerId = owner2.Id,
                YearOfRegistration = 2019,
                YearOfFirstService = 2020
            };

            var car3 = new Car()
            {
                Id = Guid.NewGuid(),
                Title = "Ford Mustang",
                Make = "Ford",
                Model = "Mustang",
                Year = 2018,
                Distance = 30000,
                FuelType = "Gasoline",
                Power = 300,
                OwnerId = owner3.Id,
                YearOfRegistration = 2018,
                YearOfFirstService = 2019
            };

            var car4 = new Car()
            {
                Id = Guid.NewGuid(),
                Title = "Honda Civic",
                Make = "Honda",
                Model = "Civic",
                Year = 2017,
                Distance = 40000,
                FuelType = "Hybrid",
                Power = 180,
                OwnerId = owner1.Id,
                YearOfRegistration = 2017,
                YearOfFirstService = 2018
            };

            var car5 = new Car()
            {
                Id = Guid.Parse("4b33a8f4-d3d6-41e2-b709-3cb5b72869f2"),
                Title = "Nissan Leaf",
                Make = "Nissan",
                Model = "Leaf",
                Year = 2016,
                Distance = 50000,
                FuelType = "Electric",
                Power = 110,
                OwnerId = owner2.Id,
                YearOfRegistration = 2016,
                YearOfFirstService = 2017
            };
            CarId5 = car5.Id;


            // Add the data to the database context
            _dbContext.Owners.AddRange(owner1, owner2, owner3);
            _dbContext.Cars.AddRange(car1, car2, car3, car4, car5);

            // Save the changes
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetCars_ReturnsCorrectResponse()
        {
            var response = await _client.GetAsync("/car/cars");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var pagedResult = JsonConvert.DeserializeObject<PagedResult<Car>>(stringResponse);
            var cars = pagedResult.Results;

            Assert.NotNull(cars);
        }



        [Fact]
        public async Task GetCar_ReturnsCorrectResponse_WhenCarExists()
        {
            var id = "4b33a8f4-d3d6-41e2-b709-3cb5b72869f2";
            var response = await _client.GetAsync($"/car/get-car-by-id/{id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var car = JsonConvert.DeserializeObject<Car>(stringResponse);

            Assert.NotNull(car);
            Assert.Equal(id, car.Id.ToString());
        }

        [Fact]
        public async Task GetCar_ReturnsNotFound_WhenCarDoesNotExist()
        {
            var id = Guid.NewGuid();
            var response = await _client.GetAsync($"/car/get-car-by-id/{id}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task CreateNewCar_ReturnsCorrectResponse()
        {
            // Arrange
            var newCar = new CarDto
            {
                Title = "Test Car",
                Make = "Test Make",
                Model = "Test Model",
                Year = 2022,
                Distance = 50000,
                FuelType = "diesel",
                Power = 300,
                OwnerId = Guid.Parse("d6e0b9f0-8c9d-4f7a-8f0d-7b8e6a5f4c7b")

            };
            var newCarObject = JsonConvert.SerializeObject(newCar);
            var content = new StringContent(JsonConvert.SerializeObject(newCar), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/car", content);
            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var returnedCar = JsonConvert.DeserializeObject<CarDto>(await response.Content.ReadAsStringAsync());

            Assert.Equal(newCar.Title, returnedCar.Title);
            Assert.Equal(newCar.Make, returnedCar.Make);
            Assert.Equal(newCar.Model, returnedCar.Model);
            Assert.Equal(newCar.Year, returnedCar.Year);
            Assert.Equal(newCar.Distance, returnedCar.Distance);
            Assert.Equal(newCar.FuelType, returnedCar.FuelType);
            Assert.Equal(newCar.Power, returnedCar.Power);


        }

        [Fact]
        public async Task DeleteCar_ReturnsOkResponse()
        {
            var id = CarId5;
            var response = await _client.DeleteAsync($"/car/delete/{id}");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var operationResult = JsonConvert.DeserializeObject<bool>(stringResponse);

            Assert.True(operationResult);
        }


        [Fact]
        public async Task DeleteCar_ReturnsCarNotFoundResponse()
        {
            var id = Guid.NewGuid();
            var response = await _client.DeleteAsync($"/car/{id}"); // Adjusted to follow the routing convention

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateCar_ReturnsUpdatedCar_When_CarExists()
        {
            // Arrange
            var id = CarId5;
            var updatedCar = new CarDto
            {
                Title = "Updated Car",
                Make = "Updated Make",
                Model = "Updated Model",
                Year = 2023,
                Distance = 60000,
                FuelType = "diesel",
                Power = 80
                // Assume OwnerId is the same so not updating it
            };
            var content = new StringContent(JsonConvert.SerializeObject(updatedCar), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/car/update/{id}", content);

            // Assert
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var returnedCar = JsonConvert.DeserializeObject<CarDto>(stringResponse);

            Assert.NotNull(returnedCar);
            Assert.Equal(updatedCar.Title, returnedCar.Title);
            Assert.Equal(updatedCar.Make, returnedCar.Make);
            Assert.Equal(updatedCar.Model, returnedCar.Model);
            Assert.Equal(updatedCar.Year, returnedCar.Year);
            Assert.Equal(updatedCar.Distance, returnedCar.Distance);
            Assert.Equal(updatedCar.FuelType, returnedCar.FuelType);
            Assert.Equal(updatedCar.Power, returnedCar.Power);
        }

        [Fact]
        public async Task UpdateCar_ReturnsNotFound_When_CarDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid(); // a car with this id should not exist
            var updatedCar = new CarDto
            {
                Title = "Updated Car",
                Make = "Updated Make",
                Model = "Updated Model",
                Year = 2023,
                Distance = 60000,
                FuelType = "diesel",
                Power = 80
                // Assume OwnerId is the same so not updating it
            };
            var content = new StringContent(JsonConvert.SerializeObject(updatedCar), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/car/update/{id}", content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }



    }

}



