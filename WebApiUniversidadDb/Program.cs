using Microsoft.EntityFrameworkCore;
using WebApiUniversidadDb.Infrastructure.Databases;
using WebApiUniversidadDb.Infrastructure.Interfaces;
using WebApiUniversidadDb.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Base de datos
builder.Services.AddDbContext<UniversidadDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DbUniversidadConnectionString")
        )
);

// Add services to the container.
builder.Services.AddControllers();
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