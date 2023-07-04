using Car_App.Controllers;
using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Car_App.Data.Models.Filtering;
using Car_App.Data.Models.Sorting;
using Car_App.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CarApp.Tests.Controller
{
    public class CarControllerTests
    {
        private readonly Mock<ICarService> _carServiceMock;

        public CarControllerTests()
        {
            _carServiceMock = new Mock<ICarService>();
        }

        [Fact]
        public async Task GetCars_Returns_OkResult()
        {
            // ARRANGE
            var cars = new PagedResult<Car>
            {
                TotalCount = 3,
                TotalPages = 1,
                CurrentPage = 1,
                Results = new List<Car>
                {
                    new Car { Id = Guid.NewGuid(), OwnerId = Guid.NewGuid(), Make = "Toyota", Model = "Camry", Year = 2018 },
                    new Car { Id = Guid.NewGuid(), OwnerId = Guid.NewGuid(), Make = "Honda", Model = "Civic", Year = 2017 },
                    new Car { Id = Guid.NewGuid(), OwnerId = Guid.NewGuid(), Make = "Ford", Model = "F-150", Year = 2019 },
                }
            };

            var paginationParameters = new PaginationParameters { Page = 1, PageSize = 10 };
            var filter = new CarFilter();
            var sortBy = CarSortBy.Year;
            var sortingDirection = SortingDirection.Ascending;

            _carServiceMock.Setup(service => service.GetAllCarsAsync(paginationParameters, filter, sortBy, sortingDirection))
                .ReturnsAsync(cars);

            var controller = new CarController(_carServiceMock.Object);

            // ACT
            var result = await controller.GetCars(paginationParameters, filter, sortBy, sortingDirection);

            // ASSERT
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCars = Assert.IsType<PagedResult<Car>>(okResult.Value);

            Assert.Equal(3, returnedCars.TotalCount);
            Assert.Equal(1, returnedCars.CurrentPage);
            Assert.Equal(1, returnedCars.TotalPages);
            Assert.False(returnedCars.HasNextPage);
        }

        [Fact]
        public async Task GetCar_Returns_CorrectCar()
        {
            // Arrange
            var expectedCar = new Car { Id = Guid.NewGuid(), OwnerId = Guid.NewGuid(), Make = "Toyota", Model = "Camry", Year = 2018 };

            _carServiceMock.Setup(service => service.GetCarByIdAsync(expectedCar.Id)).ReturnsAsync(expectedCar);

            var controller = new CarController(_carServiceMock.Object);

            // Act
            var result = await controller.GetCar(expectedCar.Id);

            // Assert
            var returnedCar = Assert.IsType<Car>(result.Value);
            Assert.Equal(expectedCar.Id, returnedCar.Id);

            _carServiceMock.Verify(service => service.GetCarByIdAsync(expectedCar.Id), Times.Once());
        }
        [Fact]
        public async Task CreateNewCar_Returns_CreatedAtActionResult()
        {
            // Arrange
            var newCarDto = new CarDto { OwnerId = Guid.NewGuid(), Make = "Toyota", Model = "Camry", Year = 2018 };

            _carServiceMock.Setup(service => service.CreateNewCarAsync(newCarDto)).Returns(Task.CompletedTask);

            var controller = new CarController(_carServiceMock.Object);

            // Act
            var result = await controller.CreateNewCar(newCarDto);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            var returnedCarDto = Assert.IsType<CarDto>(createdResult.Value);
            Assert.Equal(newCarDto.OwnerId, returnedCarDto.OwnerId);

            _carServiceMock.Verify(service => service.CreateNewCarAsync(newCarDto), Times.Once());

        }
        [Fact]
        public async Task DeleteCar_Returns_OkResult_When_CarExists()
        {
            // Arrange
            var carId = Guid.NewGuid();

            _carServiceMock.Setup(service => service.DeleteCarAsync(carId)).ReturnsAsync(true);

            var controller = new CarController(_carServiceMock.Object);

            // Act
            var result = await controller.DeleteCar(carId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<bool>>(result);
            Assert.True(actionResult.Result is OkObjectResult);

            var okResult = actionResult.Result as OkObjectResult;
            var returnedValue = Assert.IsType<bool>(okResult.Value);
            Assert.True(returnedValue);

            _carServiceMock.Verify(service => service.DeleteCarAsync(carId), Times.Once());
        }

        [Fact]
        public async Task UpdateCar_Returns_OkResult()
        {
            // Arrange
            var carId = Guid.NewGuid();
            var updatedCarDto = new CarDto
            {
                Title = "Test Title",
                Make = "Toyota",
                Model = "Camry",
                Year = 2019,
                Distance = 54321.6,
                FuelType = "gasoline",
                Power = 100.0,
                OwnerId = Guid.NewGuid()
            };

            _carServiceMock.Setup(service => service.UpdateCarAsync(carId, updatedCarDto)).Returns(Task.FromResult(true));

            var controller = new CarController(_carServiceMock.Object);

            // Act
            var result = await controller.UpdateCar(updatedCarDto, carId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCarDto = Assert.IsType<CarDto>(okResult.Value);
            Assert.Equal(updatedCarDto.Title, returnedCarDto.Title);

            _carServiceMock.Verify(service => service.UpdateCarAsync(carId, updatedCarDto), Times.Once());
        }


    }
}
