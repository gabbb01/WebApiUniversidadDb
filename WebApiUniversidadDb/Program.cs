using Microsoft.EntityFrameworkCore;
using WebApiUniversidadDb.Infrastructure.Databases;

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

// Swagger / OpenAPI
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

app.UseAuthorization();

app.MapControllers();

app.Run();