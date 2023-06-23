using Car_App.Data.Context;
using Car_App.Data.Models;
using Car_App.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Moq.EntityFrameworkCore;

public class OwnerServiceTests
{
    private readonly Mock<DbSet<Car>> _mockSetCar;
    private readonly Mock<DbSet<Owner>> _mockSetOwner;
    private readonly Mock<DatabaseContext> _mockContext;
    private readonly IOptions<JwtSettings> _jwtSettings;
    private readonly Guid _ownerId;
    private readonly Owner _owner;
    private readonly List<Car> _cars;

    public OwnerServiceTests()
    {
        _mockSetOwner = new Mock<DbSet<Owner>>();

        _mockSetCar = new Mock<DbSet<Car>>();
        _mockContext = new Mock<DatabaseContext>();
        _jwtSettings = Options.Create(new JwtSettings());
        _ownerId = Guid.NewGuid();
        _owner = new Owner { Id = _ownerId };
        _cars = new List<Car>
    {
        new Car { OwnerId = _ownerId },
        new Car { OwnerId = _ownerId }
    };
    }


    [Fact]
    public async Task GetOwnerByIdAsyncTest()
    {
        // Arrange
        _mockContext.Setup(c => c.Owners.FindAsync(_ownerId)).ReturnsAsync(_owner);
        var service = new OwnerService(_mockContext.Object, _jwtSettings);

        // Act
        var result = await service.GetOwnerByIdAsync(_ownerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_ownerId, result.Id);
    }

    [Fact]
    public async Task GetCarsByOwnerIdAsyncTest()
    {
        // Arrange
        _mockContext.Setup(x => x.Cars).ReturnsDbSet(_cars);
        var service = new OwnerService(_mockContext.Object, null);

        // Act
        var result = await service.GetCarsByOwnerIdAsync(_ownerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count()); // Assuming there are 2 cars with the same ownerId
    }
}
