using Car_App.Controllers.DTOModels;
using Car_App.Data.Context;
using Car_App.Data.Models;
using Car_App.Data.Models.Filtering;
using Car_App.Data.Models.Sorting;
using Car_App.Service.Interface;
using Car_App.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Car_App.Tests
{
    public class CarServiceTests
    {
        [Fact]
        public async Task GetAllCarsAsync_ReturnsPagedResults()
        {
            // Arrange
            var mockContext = new Mock<DatabaseContext>();
            var carData = GetQueryableMockDbSet(new List<Car>
            {
                new Car { Id = Guid.NewGuid(), Make = "BMW", Year = 2015, Distance = 50000, FuelType = "Petrol", Power = 200 },
                new Car { Id = Guid.NewGuid(), Make = "Audi", Year = 2020, Distance = 10000, FuelType = "Diesel", Power = 250 }
            });

            mockContext.Setup(c => c.Cars).Returns(carData.Object);
            var carService = new CarService(mockContext.Object);
            var paginationParameters = new PaginationParameters { PageSize = 1, Page = 1 };
            var filter = new CarFilter();
            var sortBy = new CarSortBy();
            var sortingDirection = SortingDirection.Ascending;

            // Act
            var result = await carService.GetAllCarsAsync(paginationParameters, filter, sortBy, sortingDirection);

            // Assert
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(2, result.TotalPages); 
            Assert.True(result.HasNextPage);
        }

        [Fact]
        public async Task GetCarByIdAsync_ReturnsCorrectCar()
        {
            // Arrange
            var mockContext = new Mock<DatabaseContext>();
            var carId = Guid.NewGuid();
            var expectedCar = new Car { Id = carId, Make = "BMW", Year = 2021, Distance = 10000, FuelType = "Petrol", Power = 200 };

            var mockSet = new Mock<DbSet<Car>>();
            mockSet.Setup(m => m.FindAsync(carId)).ReturnsAsync(expectedCar);
            mockContext.Setup(m => m.Cars).Returns(mockSet.Object);

            var carService = new CarService(mockContext.Object);

            // Act
            var result = await carService.GetCarByIdAsync(carId);

            // Assert
            Assert.Equal(expectedCar, result);
        }


    }
}
