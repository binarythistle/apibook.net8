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

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
