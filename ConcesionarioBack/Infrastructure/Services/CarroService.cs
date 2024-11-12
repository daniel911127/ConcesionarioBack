using ConcesionarioBack.Common.Models;
using ConcesionarioBack.Domain.DTOs;
using ConcesionarioBack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ConcesionarioBack.Infrastructure.Services
{
    public class CarroService : ICommonVehiculosService<CarroDto>
    {

        private ConcesionarioContext _context;

        public CarroService(ConcesionarioContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarroDto>> Get()=>
          await _context.Carros.Select(c => new CarroDto
            {
                CarroId = c.CarroId,
                Modelo = c.Modelo,
                Color = c.Color,
                Kilometraje = c.Kilometraje,
                Valor = c.Valor,
                Imagen = c.Imagen,
                FechaRegistro = c.FechaRegistro,
                Nuevo = c.Nuevo,
                Activo = c.Activo,
            }).ToListAsync();
        

        public async Task<CarroDto> GetById(int id)
        {
            var carro = await _context.Carros.FindAsync(id);

            if (carro != null)
            {
                var carroDto = new CarroDto
                {
                    CarroId = carro.CarroId,
                    Modelo = carro.Modelo,
                    Color = carro.Color,
                    Kilometraje = carro.Kilometraje,
                    Valor = carro.Valor,
                    Imagen = carro.Imagen,
                    FechaRegistro = carro.FechaRegistro,
                    Nuevo = carro.Nuevo,
                    Activo = carro.Activo,
                };

                return carroDto;
            }

                return null;
        }

        public async Task<CarroDto> Add(CarroDto carroDto)
        {
            var carro = new Carro
            {
                Modelo = carroDto.Modelo,
                Color = carroDto.Color,
                Kilometraje = carroDto.Nuevo ? "0" : carroDto.Kilometraje,
                Valor = carroDto.Valor,
                Imagen = "carro2.jpg",
                FechaRegistro = DateTime.Now,
                Nuevo = carroDto.Nuevo,
                Activo = carroDto.Activo,
            };

            await _context.Carros.AddAsync(carro);
            await _context.SaveChangesAsync();

            var newCarroDto = new CarroDto
            {
                CarroId = carro.CarroId,
                Modelo = carro.Modelo,
                Color = carro.Color,
                Kilometraje = carro.Kilometraje,
                Valor = carro.Valor,
                Imagen = carro.Imagen,
                FechaRegistro = carro.FechaRegistro,
                Nuevo = carro.Nuevo,
                Activo = carro.Activo,
            };

            return newCarroDto;
        }

        public async Task<CarroDto> Update(int id, CarroDto carroDto)
        {
            var carro = await _context.Carros.FindAsync(id);

            if (carro != null)
            {
                if (carroDto.Modelo != null)
                    carro.Modelo = carroDto.Modelo;

                if (carroDto.Color != null)
                    carro.Color = carroDto.Color;

                if (carroDto.Kilometraje != null)
                    carro.Kilometraje = carroDto.Kilometraje;

                if (carroDto.Valor != null)
                    carro.Valor = carroDto.Valor;

                if (carroDto.Imagen != null)
                    carro.Imagen = carroDto.Imagen;

                carro.FechaRegistro = DateTime.Now;
                carro.Nuevo = carroDto.Nuevo;
                carro.Activo = carroDto.Activo;

                await _context.SaveChangesAsync();

                var updateCarroDto = new CarroDto
                {
                    CarroId = carro.CarroId,
                    Modelo = carro.Modelo,
                    Color = carro.Color,
                    Kilometraje = carro.Kilometraje,
                    Valor = carro.Valor,
                    Imagen = carro.Imagen,
                    FechaRegistro = carro.FechaRegistro,
                    Nuevo = carro.Nuevo,
                    Activo = carro.Activo,
                };

                return updateCarroDto;
            }

            return null;
        }

        public async Task<CarroDto> Delete(int id)
        {
            var carro = await _context.Carros.FindAsync(id);

            if (carro == null)
               return null;

            var deleteCarroDto = new CarroDto
            {
                CarroId = carro.CarroId,
                Modelo = carro.Modelo,
                Color = carro.Color,
                Kilometraje = carro.Kilometraje,
                Valor = carro.Valor,
                Imagen = carro.Imagen,
                FechaRegistro = carro.FechaRegistro,
                Nuevo = carro.Nuevo,
                Activo = carro.Activo,
            };

            _context.Carros.Remove(carro);
            await _context.SaveChangesAsync();


            return deleteCarroDto;
        }

        public async Task<string> SumarValores()
        {
            var sumaValores = await _context.Carros.SumAsync(c => c.Valor);
            var sumaValoresFormateada = sumaValores.ToString("N0", new CultureInfo("es-ES"));
            var response = $"La suma de los valores de los carros es: ${sumaValoresFormateada}";
            return (response);
        }

    }
}
