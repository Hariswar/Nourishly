using Microsoft.EntityFrameworkCore;
using UserAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? ["http://localhost:4200"];
        policy.WithOrigins(origins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Create the schema on first run; the map tables are used through raw SQL, so EF doesn't create them
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserContext>();
    db.Database.EnsureCreated();
    db.Database.ExecuteSqlRaw(@"
        CREATE TABLE IF NOT EXISTS user_auth_map (user_id int NOT NULL, auth_id int NOT NULL, PRIMARY KEY (user_id, auth_id));
        CREATE TABLE IF NOT EXISTS user_fund_map (user_id int NOT NULL, fund_id int NOT NULL, PRIMARY KEY (user_id, fund_id));");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // In production the host terminates TLS in front of the container
    app.UseHttpsRedirection();
}

app.UseCors();
app.MapControllers();

app.Run();
