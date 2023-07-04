using Car_App.Controllers;
using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Car_App.Data.Models.NewFolder;
using Car_App.Data.Models.Sorting;
using Car_App.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CarApp.UnitTests.Controller
{
    public class OwnerControllerTests
    {
        private readonly Mock<IOwnerService> _mockOwnerService;
        private readonly OwnerController _ownerController;

        public OwnerControllerTests()
        {
            _mockOwnerService = new Mock<IOwnerService>();
            _ownerController = new OwnerController(_mockOwnerService.Object);
        }

        [Fact]
        public async Task GetOwners_ReturnsOkResult_WhenCalled()
        {
            // Arrange
            var paginationParameters = new PaginationParameters();
            var filter = new OwnerFilter();
            var sortBy = new OwnerSortBy();
            var sortingDirection = SortingDirection.Ascending;
            _mockOwnerService.Setup(service =>
                    service.GetAllOwnersAsync(paginationParameters, filter, sortBy, sortingDirection))
                .ReturnsAsync(new PagedResult<Owner>());

            // Act
            var result = await _ownerController.GetOwners(paginationParameters, filter, sortBy, sortingDirection);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetCarsByOwnerId_ReturnsNotFound_WhenOwnerDoesNotExist()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            _mockOwnerService.Setup(service => service.GetOwnerWithCarsByIdAsync(ownerId)).ReturnsAsync((Owner)null);

            // Act
            var result = await _ownerController.GetCarsByOwnerId(ownerId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Register_ReturnsOkResult_WhenCalled()
        {
            // Arrange
            var registerOwnerDto = new RegisterOwnerDto();
            _mockOwnerService.Setup(service => service.RegisterAsync(registerOwnerDto)).Returns(Task.CompletedTask);

            // Act
            var result = await _ownerController.Register(registerOwnerDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Authenticate_ReturnsOkResult_WhenCalled()
        {
            // Arrange
            var authenticateRequestDto = new AuthenticateRequestDto();

            var owner = new Owner();
            var token = "someJWT";

            _mockOwnerService.Setup(service => service.AuthenticateAsync(authenticateRequestDto))
                .ReturnsAsync(new AuthenticateResponseDto(owner, token));


            // Act
            var result = await _ownerController.Authenticate(authenticateRequestDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteOwner_ReturnsNotFound_WhenOwnerDoesNotExist()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            _mockOwnerService.Setup(service => service.DeleteOwnerAsync(ownerId)).ReturnsAsync(false);

            // Act
            var result = await _ownerController.DeleteOwner(ownerId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateOwner_ReturnsOkResult_WhenCalled()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var newOwner = new OwnerDto();
            _mockOwnerService.Setup(service => service.UpdateOwnerAsync(ownerId, newOwner))
                .Returns(Task.FromResult(true));

            // Act
            var result = await _ownerController.UpdateOwner(newOwner, ownerId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}