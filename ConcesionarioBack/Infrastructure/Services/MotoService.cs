using ConcesionarioBack.Common.Models;
using ConcesionarioBack.Domain.DTOs;
using ConcesionarioBack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ConcesionarioBack.Infrastructure.Services
{
    public class MotoService : ICommonVehiculosService<MotoDto>
    {
        private ConcesionarioContext _context;
        

        public MotoService(ConcesionarioContext context)
        {
            _context = context;
            
        }

        public async Task<IEnumerable<MotoDto>> Get()=>            
            await _context.Motos.Select(m => new MotoDto
            {
                MotoId = m.MotoId,
                Modelo = m.Modelo,
                Color = m.Color,
                Kilometraje = m.Kilometraje,
                Valor = m.Valor,
                Imagen = m.Imagen,
                FechaRegistro = m.FechaRegistro,
                Nuevo = m.Nuevo,
                Activo = m.Activo,
                Cilindraje = m.Cilindraje,
                Velocidades = m.Velocidades,
            }).ToListAsync();

        public async Task<MotoDto> GetById(int id)
        {
            var moto = await _context.Motos.FindAsync(id);

            if (moto == null)
               return null;
            

            var motoDto= new MotoDto
            {
                MotoId = moto.MotoId,
                Modelo = moto.Modelo,
                Color = moto.Color,
                Kilometraje = moto.Kilometraje,
                Valor = moto.Valor,
                Imagen = moto.Imagen,
                FechaRegistro = moto.FechaRegistro,
                Nuevo = moto.Nuevo,
                Activo = moto.Activo,
                Cilindraje = moto.Cilindraje,
                Velocidades = moto.Velocidades,
            };

            return motoDto;
        }

        public async Task<MotoDto> Add(MotoDto motoDto)
        {
            var moto = new Moto
            {
                Modelo = motoDto.Modelo,
                Color = motoDto.Color,
                Kilometraje = motoDto.Nuevo ? "0" : motoDto.Kilometraje,
                Valor = motoDto.Valor.Value,
                Imagen = "moto.jpeg",
                FechaRegistro = DateTime.Now,
                Nuevo = motoDto.Nuevo,
                Activo = motoDto.Activo,
                Cilindraje = motoDto.Cilindraje,
                Velocidades = motoDto.Velocidades,
            };

            await _context.Motos.AddAsync(moto);
            await _context.SaveChangesAsync();

            var newMotoDto = new MotoDto
            {
                MotoId = moto.MotoId,
                Modelo = moto.Modelo,
                Color = moto.Color,
                Kilometraje = moto.Kilometraje,
                Valor = moto.Valor,
                Imagen = moto.Imagen,
                FechaRegistro = moto.FechaRegistro,
                Nuevo = moto.Nuevo,
                Activo = true,
                Cilindraje = moto.Cilindraje,
                Velocidades = moto.Velocidades,
            };

            return newMotoDto;
        }

        public async Task<MotoDto> Update(int id,MotoDto motoDto)
        {
            var moto = await _context.Motos.FindAsync(id);

            if (moto == null)
               return null;
            

            if (motoDto.Modelo != null)
                moto.Modelo = motoDto.Modelo;

            if (motoDto.Color != null)
                moto.Color = motoDto.Color;

            if (motoDto.Kilometraje != null)
                moto.Kilometraje = motoDto.Kilometraje;

            if (motoDto.Valor.HasValue)
                moto.Valor = motoDto.Valor.Value;

            if (motoDto.Imagen != null)
                moto.Imagen = motoDto.Imagen;

            if (motoDto.Cilindraje != null)
                moto.Cilindraje = motoDto.Cilindraje;

            if (motoDto.Velocidades != null)
                moto.Velocidades = motoDto.Velocidades;


            moto.FechaRegistro = DateTime.Now;
            moto.Nuevo = motoDto.Nuevo;
            moto.Activo = motoDto.Activo;

            await _context.SaveChangesAsync();

            var updateMotoDto = new MotoDto
            {
                MotoId = moto.MotoId,
                Modelo = moto.Modelo,
                Color = moto.Color,
                Kilometraje = moto.Kilometraje,
                Valor = moto.Valor,
                Imagen = moto.Imagen,
                FechaRegistro = moto.FechaRegistro,
                Nuevo = moto.Nuevo,
                Activo = moto.Activo,
                Cilindraje = moto.Cilindraje,
                Velocidades = moto.Velocidades,
            };
            return updateMotoDto;
        }

        public async Task<MotoDto> Delete(int motoId)
        {
            var moto = await _context.Motos.FindAsync(motoId);

            if (moto == null)
                return null;

            var deleteMotoDto = new MotoDto
            {
                MotoId = moto.MotoId,
                Modelo = moto.Modelo,
                Color = moto.Color,
                Kilometraje = moto.Kilometraje,
                Valor = moto.Valor,
                Imagen = moto.Imagen,
                FechaRegistro = moto.FechaRegistro,
                Nuevo = moto.Nuevo,
                Activo = moto.Activo,
                Cilindraje = moto.Cilindraje,
                Velocidades = moto.Velocidades,
            };

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();


            return deleteMotoDto;
        }

        public async Task<string> SumarValores()
        {
            var sumaValores = await _context.Motos.SumAsync(c => c.Valor);
            var sumaValoresFormateada = sumaValores.ToString("N0", new CultureInfo("es-ES"));
            var response = $"La suma de los valores de las motos es: ${sumaValoresFormateada}";
            return (response);
        }
    }
}
