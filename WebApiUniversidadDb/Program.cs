using Microsoft.EntityFrameworkCore;
using WebApiUniversidadDb.Features.Universidad.AppServices;
using WebApiUniversidadDb.Features.Universidad.DomainServices;
using WebApiUniversidadDb.Features.Universidad.Interfaces;
using WebApiUniversidadDb.Infrastructure.Databases;
using WebApiUniversidadDb.Infrastructure.Interfaces;
using WebApiUniversidadDb.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Base de datos
builder.Services.AddDbContext<UniversidadDbContext>(
    options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "DbUniversidadConnectionString"
        )
    )
);

// SERVICIOS PARA ASIGNATURAS
builder.Services.AddScoped<
    IAsignaturasRepository,
    AsignaturasRepository>();
builder.Services.AddScoped<
    IAsignaturasAppService,
    AsignaturasAppService>();
builder.Services.AddScoped<AsignaturasDomainService>();

// SERVICIOS PARA AULAS
builder.Services.AddScoped<
    IAulasRepository,
    AulasRepository>();
builder.Services.AddScoped<
    IAulasAppService,
    AulasAppService>();
builder.Services.AddScoped<AulasDomainService>();

// SERVICIOS PARA ESTUDIANTES
builder.Services.AddScoped<
    IEstudiantesRepository,
    EstudiantesRepository>();
builder.Services.AddScoped<
    IEstudiantesAppService,
    EstudiantesAppService>();
builder.Services.AddScoped<EstudiantesDomainService>();

// SERVICIOS PARA PROFESORES
builder.Services.AddScoped<
    IProfesoresRepository,
    ProfesoresRepository>();
builder.Services.AddScoped<
    IProfesoresAppService,
    ProfesoresAppService>();
builder.Services.AddScoped<ProfesoresDomainService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();




var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();