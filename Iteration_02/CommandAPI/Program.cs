using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var pgsqlConnection = new NpgsqlConnectionStringBuilder();
pgsqlConnection.ConnectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
pgsqlConnection.Username = builder.Configuration["DbUserId"];
pgsqlConnection.Password = builder.Configuration["DbPassword"];

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(pgsqlConnection.ConnectionString));

builder.Services.AddControllers();

var app = builder.Build();

// Run migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // Applies pending migrations
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
