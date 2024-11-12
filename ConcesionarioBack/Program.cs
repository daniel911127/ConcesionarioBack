using ConcesionarioBack.Common.Models;
using ConcesionarioBack.Domain.DTOs;
using ConcesionarioBack.Domain.Interfaces;
using ConcesionarioBack.Infrastructure.Services;
using ConcesionarioBack.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Permite cualquier origen
              .AllowAnyMethod()  // Permite cualquier método HTTP (GET, POST, etc.)
              .AllowAnyHeader(); // Permite cualquier encabezado
    });
});

// Add services to the container.
builder.Services.AddKeyedScoped<ICommonVehiculosService<CarroDto>, CarroService>("vehiculosService");
builder.Services.AddKeyedScoped<ICommonVehiculosService<MotoDto>, MotoService>("vehiculosService");
builder.Services.AddKeyedScoped<ICommonService<VentaDto>, VentaService>("ventaService");
builder.Services.AddKeyedScoped<ICommonService<ListadoDto>, ListadoCarroService>("listadoCarro");
builder.Services.AddKeyedScoped<ICommonService<ListadoDto>, ListadoMotoService>("listadoMoto");


//configuracion de la base de datos entity framework
builder.Services.AddDbContext<ConcesionarioContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConcesionarioConnection"));
});

// Validators
builder.Services.AddScoped<IValidator<CarroDto>, CarroValidator>();
builder.Services.AddScoped<IValidator<MotoDto>, MotoValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del middleware de CORS
app.UseCors("AllowAllOrigins");

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
