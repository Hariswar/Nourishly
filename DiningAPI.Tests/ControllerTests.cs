using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DiningAPI.Controllers;
using DiningAPI.Models;
using DiningAPI.Features.Locations.Queries;
using DiningAPI.Features.Locations.Commands;
using DiningAPI.Features.Menus.Queries;
using DiningAPI.Features.Menus.Commands;

namespace DiningAPI.Tests;

public class ControllerTests
{
    [Fact]
    public async Task LocationController_GetLocations_ReturnsOk()
    {
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<GetAllLocationsQuery>(), default)).ReturnsAsync(new List<object>());
        var controller = new LocationController(mockMediator.Object);

        var result = await controller.GetLocations();

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task LocationController_GetLocationById_ReturnsOk()
    {
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<GetLocationByIdQuery>(), default)).ReturnsAsync(new { });
        var controller = new LocationController(mockMediator.Object);

        var result = await controller.GetLocationById(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task LocationController_CreateLocation_ReturnsCreated()
    {
        var mockMediator = new Mock<IMediator>();
        var response = new { LocationId = 1, Name = "Test", Address = "Address" };
        mockMediator.Setup(m => m.Send(It.IsAny<CreateLocationCommand>(), default)).ReturnsAsync((object)response);
        var controller = new LocationController(mockMediator.Object);

        var result = await controller.CreateLocation(new CreateLocationCommand("Test", "Address"));

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task LocationController_UpdateLocation_ReturnsOk()
    {
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<UpdateLocationCommand>(), default)).ReturnsAsync(new { });
        var controller = new LocationController(mockMediator.Object);

        var result = await controller.UpdateLocation(1, new UpdateLocationCommand(1, "Test", "Address"));

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task LocationController_UpdateLocation_ReturnsNotFound()
    {
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<UpdateLocationCommand>(), default)).ReturnsAsync((object?)null);
        var controller = new LocationController(mockMediator.Object);

        var result = await controller.UpdateLocation(999, new UpdateLocationCommand(999, "Test", "Address"));

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task LocationController_GetLocationById_ReturnsNotFound()
    {
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<GetLocationByIdQuery>(), default)).ReturnsAsync((object?)null);
        var controller = new LocationController(mockMediator.Object);

        var result = await controller.GetLocationById(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task LocationController_GetMenuByLocation_ReturnsOk()
    {
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<GetMenusByLocationQuery>(), default)).ReturnsAsync(new { });
        var controller = new LocationController(mockMediator.Object);

        var result = await controller.GetMenuByLocation(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task LocationController_GetMenuByLocation_ReturnsNotFound()
    {
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<GetMenusByLocationQuery>(), default)).ReturnsAsync((object?)null);
        var controller = new LocationController(mockMediator.Object);

        var result = await controller.GetMenuByLocation(999);

        Assert.IsType<NotFoundResult>(result);
    }



    [Fact]
    public async Task MenuItemController_GetMenuItemDetails_ReturnsOk()
    {
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<GetMenuItemDetailsQuery>(), default)).ReturnsAsync(new { });
        var controller = new MenuItemController(mockMediator.Object);

        var result = await controller.GetMenuItemDetails(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task MenuController_GetMenuItems_ReturnsOk()
    {
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<GetMenuItemsQuery>(), default)).ReturnsAsync(new List<object>());
        var controller = new MenuController(mockMediator.Object);

        var result = await controller.GetMenuItems(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task MenuController_GetAllMenuItems_ReturnsOk()
    {
        var mockMediator = new Mock<IMediator>();
        var items = new List<MenuItem> { new MenuItem { ItemId = 1, Name = "Test" } };
        mockMediator.Setup(m => m.Send(It.IsAny<GetAllMenuItemsQuery>(), default)).ReturnsAsync((IEnumerable<MenuItem>)items);
        var controller = new MenuController(mockMediator.Object);

        var result = await controller.GetAllMenuItems();

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task MenuController_CreateMenuItem_ReturnsCreated()
    {
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<CreateMenuItemCommand>(), default)).ReturnsAsync(new { ItemId = 1 });
        var controller = new MenuController(mockMediator.Object);

        var result = await controller.CreateMenuItem(1, new CreateMenuItemCommand(1, "Test", "Desc", 9.99m, 200, 10, 5, 30));

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task MenuController_UpdateMenuItem_ReturnsOk()
    {
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<UpdateMenuItemCommand>(), default)).ReturnsAsync(new { });
        var controller = new MenuController(mockMediator.Object);

        var result = await controller.UpdateMenuItem(1, 1, new UpdateMenuItemCommand(1, 1, "Test", "Desc", 9.99m, 200, 10, 5, 30));

        Assert.IsType<OkObjectResult>(result);
    }
}
