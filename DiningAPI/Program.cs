var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// --- Static dining data (sample) ---
var diningLocations = new[]
{
    new DiningLocation(1, "Main Dining Hall", "123 Campus Rd", "07:00-20:00"),
    new DiningLocation(2, "Cafe Express", "45 Student Way", "08:00-18:00"),
    new DiningLocation(3, "Vegan Corner", "12 Health Ave", "10:00-16:00")
};

var foodOptions = new[]
{
    new FoodOption(1, "Burger", "Entree", 1, new NutritionFacts(650, 40.0, 30.0, 45.0, "Beef patty, bun, cheese")),
    new FoodOption(2, "Salad", "Side", 1, new NutritionFacts(180, 12.0, 4.0, 12.0, "Mixed greens with dressing")),
    new FoodOption(3, "Latte", "Beverage", 2, new NutritionFacts(190, 8.0, 8.0, 18.0, "Espresso with whole milk")),
    new FoodOption(4, "Veg Bowl", "Entree", 3, new NutritionFacts(420, 15.0, 18.0, 50.0, "Quinoa and roasted vegetables"))
};

app.MapGet("/dining/locations", () => diningLocations)
   .WithName("GetDiningLocations")
   .WithOpenApi();

app.MapGet("/dining/locations/{id:int}", (int id) =>
{
    var loc = Array.Find(diningLocations, d => d.Id == id);
    return loc is not null ? Results.Ok(loc) : Results.NotFound();
})
   .WithName("GetDiningLocationById")
   .WithOpenApi();

app.MapGet("/dining/locations/{id:int}/options", (int id) =>
{
    var options = Array.FindAll(foodOptions, o => o.LocationId == id);
    return options;
})
   .WithName("GetFoodOptionsByLocation")
   .WithOpenApi();

app.MapGet("/dining/options", () => foodOptions)
   .WithName("GetFoodOptions")
   .WithOpenApi();

app.MapGet("/dining/options/{id:int}", (int id) =>
{
    var opt = Array.Find(foodOptions, f => f.Id == id);
    return opt is not null ? Results.Ok(opt) : Results.NotFound();
})
   .WithName("GetFoodOptionById")
   .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record DiningLocation(int Id, string Name, string? Address, string? Hours);

record FoodOption(int Id, string Name, string Category, int LocationId, NutritionFacts Nutrition);

record NutritionFacts(int Calories, double FatGrams, double ProteinGrams, double CarbsGrams, string? Notes);
