using Xunit;
using Moq;
using DiningAPI.Features.Menus.Handlers;
using DiningAPI.Features.Menus.Queries;
using DiningAPI.Features.Menus.Commands;
using DiningAPI.Repositories;
using DiningAPI.Models;

namespace DiningAPI.Tests;

public class MenuHandlerTests
{
    [Fact]
    public async Task GetAllMenuItems_ReturnsAllItems()
    {
        var mockRepo = new Mock<IMenuRepository>();
        var items = new List<MenuItem>
        {
            new MenuItem { ItemId = 1, Name = "Test Item", Price = 9.99m }
        };
        mockRepo.Setup(r => r.GetAllMenuItemsAsync()).ReturnsAsync(items);

        var handler = new GetAllMenuItemsHandler(mockRepo.Object);
        var result = await handler.Handle(new GetAllMenuItemsQuery(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetMenuItemDetails_ReturnsItem_WhenExists()
    {
        var mockRepo = new Mock<IMenuRepository>();
        var nutrition = new Nutrition { NutritionId = 1, Calories = 200, Protein = 10, Fat = 5, Carbs = 30 };
        var item = new MenuItem { ItemId = 1, Name = "Test Item", Price = 9.99m, Nutritions = new List<Nutrition> { nutrition } };
        mockRepo.Setup(r => r.MenuItemExistsAsync(1)).ReturnsAsync(true);
        mockRepo.Setup(r => r.GetMenuItemByIdAsync(1)).ReturnsAsync(item);

        var handler = new GetMenuItemDetailsHandler(mockRepo.Object);
        var result = await handler.Handle(new GetMenuItemDetailsQuery(1), CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetMenuItemDetails_ReturnsNull_WhenNotExists()
    {
        var mockRepo = new Mock<IMenuRepository>();
        mockRepo.Setup(r => r.MenuItemExistsAsync(999)).ReturnsAsync(false);

        var handler = new GetMenuItemDetailsHandler(mockRepo.Object);
        var result = await handler.Handle(new GetMenuItemDetailsQuery(999), CancellationToken.None);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetMenuItems_ReturnsItems_WhenMenuExists()
    {
        var mockRepo = new Mock<IMenuRepository>();
        var nutrition = new Nutrition { NutritionId = 1, Calories = 200, Protein = 10, Fat = 5, Carbs = 30 };
        var items = new List<MenuItem> { new MenuItem { ItemId = 1, Name = "Test", Nutritions = new List<Nutrition> { nutrition } } };
        mockRepo.Setup(r => r.MenuExistsAsync(1)).ReturnsAsync(true);
        mockRepo.Setup(r => r.GetMenuItemsAsync(1)).ReturnsAsync(items);

        var handler = new GetMenuItemsHandler(mockRepo.Object);
        var result = await handler.Handle(new GetMenuItemsQuery(1), CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetMenuItems_ReturnsNull_WhenMenuNotExists()
    {
        var mockRepo = new Mock<IMenuRepository>();
        mockRepo.Setup(r => r.MenuExistsAsync(999)).ReturnsAsync(false);

        var handler = new GetMenuItemsHandler(mockRepo.Object);
        var result = await handler.Handle(new GetMenuItemsQuery(999), CancellationToken.None);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateMenuItem_CreatesSuccessfully()
    {
        var mockRepo = new Mock<IMenuRepository>();
        var nutrition = new Nutrition { NutritionId = 1, Calories = 200, Protein = 10, Fat = 5, Carbs = 30 };
        var item = new MenuItem { ItemId = 1, Name = "Test", Nutritions = new List<Nutrition> { nutrition } };
        mockRepo.Setup(r => r.MenuExistsAsync(1)).ReturnsAsync(true);
        mockRepo.Setup(r => r.CreateMenuItemAsync(1, "Test", "Desc", 9.99m, 200, 10, 5, 30)).ReturnsAsync(item);

        var handler = new CreateMenuItemHandler(mockRepo.Object);
        var result = await handler.Handle(new CreateMenuItemCommand(1, "Test", "Desc", 9.99m, 200, 10, 5, 30), CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task CreateMenuItem_ReturnsNull_WhenMenuNotExists()
    {
        var mockRepo = new Mock<IMenuRepository>();
        mockRepo.Setup(r => r.MenuExistsAsync(999)).ReturnsAsync(false);

        var handler = new CreateMenuItemHandler(mockRepo.Object);
        var result = await handler.Handle(new CreateMenuItemCommand(999, "Test", "Desc", 9.99m, 200, 10, 5, 30), CancellationToken.None);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateMenuItem_UpdatesSuccessfully()
    {
        var mockRepo = new Mock<IMenuRepository>();
        var nutrition = new Nutrition { NutritionId = 1, Calories = 200, Protein = 10, Fat = 5, Carbs = 30 };
        var item = new MenuItem { ItemId = 1, Name = "Updated", Nutritions = new List<Nutrition> { nutrition } };
        mockRepo.Setup(r => r.UpdateMenuItemAsync(1, 1, "Updated", "Desc", 9.99m, 200, 10, 5, 30)).ReturnsAsync(item);

        var handler = new UpdateMenuItemHandler(mockRepo.Object);
        var result = await handler.Handle(new UpdateMenuItemCommand(1, 1, "Updated", "Desc", 9.99m, 200, 10, 5, 30), CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateMenuItem_ReturnsNull_WhenNotExists()
    {
        var mockRepo = new Mock<IMenuRepository>();
        mockRepo.Setup(r => r.UpdateMenuItemAsync(999, 999, "Test", "Desc", 9.99m, 200, 10, 5, 30)).ReturnsAsync((MenuItem?)null);

        var handler = new UpdateMenuItemHandler(mockRepo.Object);
        var result = await handler.Handle(new UpdateMenuItemCommand(999, 999, "Test", "Desc", 9.99m, 200, 10, 5, 30), CancellationToken.None);

        Assert.Null(result);
    }
}
