using Microsoft.EntityFrameworkCore;
using WebApiUniversidadDb.Infrastructure.Databases;

var builder = WebApplication.CreateBuilder(args);

// Base de datos
builder.Services.AddDbContext<UniversidadDbContet>(
    options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "DbUniversidadConnectionString"
        )
    )
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
