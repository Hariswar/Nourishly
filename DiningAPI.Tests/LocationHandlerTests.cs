using Xunit;
using Moq;
using DiningAPI.Features.Locations.Handlers;
using DiningAPI.Features.Locations.Queries;
using DiningAPI.Features.Locations.Commands;
using DiningAPI.Repositories;
using DiningAPI.Models;

namespace DiningAPI.Tests;

public class LocationHandlerTests
{
    [Fact]
    public async Task GetAllLocations_ReturnsAllLocations()
    {
        var mockRepo = new Mock<ILocationRepository>();
        var locations = new List<Location>
        {
            new Location { LocationId = 1, Name = "Test Location", Address = "123 Test St" }
        };
        mockRepo.Setup(r => r.GetAllLocationsAsync()).ReturnsAsync(locations);

        var handler = new GetAllLocationsHandler(mockRepo.Object);
        var result = await handler.Handle(new GetAllLocationsQuery(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetLocationById_ReturnsLocation_WhenExists()
    {
        var mockRepo = new Mock<ILocationRepository>();
        var mockMediator = new Mock<MediatR.IMediator>();
        var location = new Location { LocationId = 1, Name = "Test Location" };
        mockRepo.Setup(r => r.GetLocationByIdAsync(1)).ReturnsAsync(location);

        var handler = new GetLocationByIdHandler(mockRepo.Object, mockMediator.Object);
        var result = await handler.Handle(new GetLocationByIdQuery(1), CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetLocationById_ReturnsNull_WhenNotExists()
    {
        var mockRepo = new Mock<ILocationRepository>();
        var mockMediator = new Mock<MediatR.IMediator>();
        mockRepo.Setup(r => r.GetLocationByIdAsync(999)).ReturnsAsync((Location?)null);

        var handler = new GetLocationByIdHandler(mockRepo.Object, mockMediator.Object);
        var result = await handler.Handle(new GetLocationByIdQuery(999), CancellationToken.None);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetMenusByLocation_ReturnsMenus_WhenLocationExists()
    {
        var mockRepo = new Mock<ILocationRepository>();
        var menus = new List<Menu> { new Menu { MenuId = 1, Name = "Test Menu" } };
        mockRepo.Setup(r => r.LocationExistsAsync(1)).ReturnsAsync(true);
        mockRepo.Setup(r => r.GetMenusByLocationAsync(1)).ReturnsAsync(menus);

        var handler = new GetMenusByLocationHandler(mockRepo.Object);
        var result = await handler.Handle(new GetMenusByLocationQuery(1), CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetMenusByLocation_ReturnsNull_WhenLocationNotExists()
    {
        var mockRepo = new Mock<ILocationRepository>();
        mockRepo.Setup(r => r.LocationExistsAsync(999)).ReturnsAsync(false);

        var handler = new GetMenusByLocationHandler(mockRepo.Object);
        var result = await handler.Handle(new GetMenusByLocationQuery(999), CancellationToken.None);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateLocation_CreatesSuccessfully()
    {
        var mockRepo = new Mock<ILocationRepository>();
        var location = new Location { LocationId = 1, Name = "Test", Address = "Address" };
        mockRepo.Setup(r => r.CreateLocationAsync("Test", "Address")).ReturnsAsync(location);

        var handler = new CreateLocationHandler(mockRepo.Object);
        var result = await handler.Handle(new CreateLocationCommand("Test", "Address"), CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateLocation_UpdatesSuccessfully()
    {
        var mockRepo = new Mock<ILocationRepository>();
        var location = new Location { LocationId = 1, Name = "Updated", Address = "New Address" };
        mockRepo.Setup(r => r.UpdateLocationAsync(1, "Updated", "New Address")).ReturnsAsync(location);

        var handler = new UpdateLocationHandler(mockRepo.Object);
        var result = await handler.Handle(new UpdateLocationCommand(1, "Updated", "New Address"), CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateLocation_ReturnsNull_WhenNotExists()
    {
        var mockRepo = new Mock<ILocationRepository>();
        mockRepo.Setup(r => r.UpdateLocationAsync(999, "Test", "Address")).ReturnsAsync((Location?)null);

        var handler = new UpdateLocationHandler(mockRepo.Object);
        var result = await handler.Handle(new UpdateLocationCommand(999, "Test", "Address"), CancellationToken.None);

        Assert.Null(result);
    }
}
