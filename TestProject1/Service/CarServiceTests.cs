using Car_App.Controllers.DTOModels;
using Car_App.Data.Context;
using Car_App.Data.Models;
using Car_App.Data.Models.Filtering;
using Car_App.Data.Models.Sorting;
using Car_App.Service.Interface;
using Car_App.Services;
using Microsoft.EntityFrameworkCore;

namespace Car_App.Tests
{
    public class CarServiceTests
    {
        private ICarService _carService;
        private DatabaseContext _dbContext;

        public CarServiceTests()
        {
            // Setting up the DbContext and services before tests
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new DatabaseContext(options);
            _carService = new CarService(_dbContext);

            SeedData();
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
                PasswordHash = "PasswordHash1",
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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


            // Add the data to the database context
            _dbContext.Owners.AddRange(owner1, owner2, owner3);
            _dbContext.Cars.AddRange(car1, car2, car3, car4, car5);

            // Save the changes
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetAllCarsServiceTest()
        {
            // Arrange
            var paginationParameters = new PaginationParameters() { Page = 1, PageSize = 10 };
            var filter = new CarFilter() { Make = "Tesla", Distance = 10000 };
            var sortBy = CarSortBy.Distance;
            var sortingDirection = SortingDirection.Ascending;

            // Act
            var result = await _carService.GetAllCarsAsync(paginationParameters, filter, sortBy, sortingDirection);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(10, result.PageSize);
            Assert.Equal(1, result.TotalPages); // Only one car matches the filter
            Assert.Equal(1, result.TotalCount); // Only one car matches the filter
            Assert.False(result.HasNextPage); // There's only one page
            Assert.Single(result.Results); // Only one car matches the filter
            Assert.Equal("Tesla", result.Results.First().Make);
            Assert.Equal(10000, result.Results.First().Distance);
        }

        [Fact]
        public async Task GetCarByIdServiceTest()
        {
            // Arrange
            var existingId = _dbContext.Cars.First().Id;

            // Act
            var result = await _carService.GetCarByIdAsync(existingId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingId, result.Id);
        }


        [Fact]
        public async Task CreateNewCarServiceTest()
        {
            // Arrange
            var owner1Id = Guid.Parse("d6e0b9f0-8c9d-4f7a-8f0d-7b8e6a5f4c7b");

            var newCarDto = new CarDto()
            {
                Title = "Hyundai Elantra",
                Make = "Hyundai",
                Model = "Elantra",
                Year = 2021,
                Distance = 1000,
                FuelType = "Gasoline",
                Power = 140,
                OwnerId = owner1Id
            };

            // Act
            await _carService.CreateNewCarAsync(newCarDto);

            // Assert
            var addedCar = _dbContext.Cars.FirstOrDefault(c => c.Title == newCarDto.Title);
            Assert.NotNull(addedCar);
            Assert.Equal(newCarDto.Make, addedCar.Make);
            Assert.Equal(newCarDto.Model, addedCar.Model);
            Assert.Equal(newCarDto.Year, addedCar.Year);
            Assert.Equal(newCarDto.Distance, addedCar.Distance);
            Assert.Equal(newCarDto.FuelType, addedCar.FuelType);
            Assert.Equal(newCarDto.Power, addedCar.Power);
            Assert.Equal(newCarDto.OwnerId, addedCar.OwnerId);
        }



        [Fact]
        public async Task DeleteCarServiceTest()
        {
            // Arrange
            var existingId = Guid.NewGuid(); // create a new unique id
            var ownerId = Guid.NewGuid(); // create a new unique owner id

            var newOwner = new Owner
            {
                Id = ownerId,
                FirstName = "Test Owner",
                LastName = "Owner Last Name",
                Email = "owner@example.com",
                Emso = "1234567890123",
                UserName = "UserName1",
                PasswordHash = "PasswordHash1",
                TelephoneNumber = "123456789"
            };

            _dbContext.Owners.Add(newOwner);

            var newCar = new Car
            {
                Id = existingId,
                Title = "Test Car",
                Make = "Test Make",
                Model = "Test Model",
                Year = 2023,
                Distance = 1000,
                FuelType = "Gasoline",
                Power = 200,
                OwnerId = ownerId
            };

            _dbContext.Cars.Add(newCar);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _carService.DeleteCarAsync(existingId);

            // Assert
            Assert.True(result);
            var deletedCar = await _dbContext.Cars.FindAsync(existingId);
            Assert.Null(deletedCar);
        }




        [Fact]
        public async Task UpdateCarServiceTest()
        {
            // Arrange
            var existingId = Guid.NewGuid(); // create a new unique id
            var ownerId = Guid.NewGuid(); // create a new unique owner id

            var newOwner = new Owner
            {
                Id = ownerId,
                FirstName = "Test Owner",
                LastName = "Owner Last Name",
                Email = "owner@example.com",
                Emso = "1234567890123",
                UserName = "UserName1",
                PasswordHash = "PasswordHash1",
                TelephoneNumber = "123456789"
            };

            _dbContext.Owners.Add(newOwner);

            var newCar = new Car
            {
                Id = existingId,
                Title = "Test Car",
                Make = "Test Make",
                Model = "Test Model",
                Year = 2023,
                Distance = 1000,
                FuelType = "Gasoline",
                Power = 200,
                OwnerId = ownerId
            };

            _dbContext.Cars.Add(newCar);
            await _dbContext.SaveChangesAsync();

            var updatedCarDto = new CarDto()
            {
                Title = "Tesla Model S",
                Make = "Tesla",
                Model = "Model S",
                Year = 2021,
                Distance = 5000,
                FuelType = "Electric",
                Power = 400,
                OwnerId = ownerId
            };

            // Act
            var result = await _carService.UpdateCarAsync(existingId, updatedCarDto);

            // Assert
            Assert.True(result);
            var updatedCar = await _dbContext.Cars.FindAsync(existingId);
            Assert.NotNull(updatedCar);
            Assert.Equal(updatedCarDto.Title, updatedCar.Title);
            Assert.Equal(updatedCarDto.Make, updatedCar.Make);
            Assert.Equal(updatedCarDto.Model, updatedCar.Model);
            Assert.Equal(updatedCarDto.Year, updatedCar.Year);
            Assert.Equal(updatedCarDto.Distance, updatedCar.Distance);
            Assert.Equal(updatedCarDto.FuelType, updatedCar.FuelType);
            Assert.Equal(updatedCarDto.Power, updatedCar.Power);
            Assert.Equal(updatedCarDto.OwnerId, updatedCar.OwnerId);
        }

        //        public CarServiceTests()
        //        {
        //            // Mock the database context for CarService
        //            _mockContext = new Mock<DatabaseContext>();
        //            _carService = new CarService(_mockContext.Object);
        //        }

    }
}
