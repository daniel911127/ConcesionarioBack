using ConcesionarioBack.Common.Models;
using ConcesionarioBack.Domain.DTOs;
using ConcesionarioBack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConcesionarioBack.Infrastructure.Services
{
    public class ListadoMotoService : ICommonService<ListadoDto>
    {
        private ConcesionarioContext _context;

        public ListadoMotoService(ConcesionarioContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ListadoDto>> Get()=>
            await _context.ListadoMotos.Select(l => new ListadoDto
            {
                Id = l.ListaMotoId,
                Modelo = l.Modelo,
                Precio = l.Precio
            }).ToListAsync();

        public async Task<ListadoDto> GetById(int id)
        {
            var listado = await _context.ListadoMotos.FindAsync(id);

            if (listado == null)
                return null;

            var listadoDto= new ListadoDto
            {
                Id = listado.ListaMotoId,
                Modelo = listado.Modelo,
                Precio = listado.Precio
            };

            return listadoDto;
        }

        public async  Task<ListadoDto> Add(ListadoDto listadoDto)
        {
            var listado = new ListadoMoto
            {
                Modelo = listadoDto.Modelo,
                Precio = listadoDto.Precio
            };

            _context.ListadoMotos.Add(listado);
            await _context.SaveChangesAsync();

            var newListadoDto = new ListadoDto
            {
                Id = listado.ListaMotoId,
                Modelo = listado.Modelo,
                Precio = listado.Precio
            };

            return newListadoDto;
        }

        public async Task<ListadoDto> Delete(int listadoId)
        {
            var listado = await _context.ListadoMotos.FindAsync(listadoId);

            if (listado == null)
                return null;

            var listadoEliminado = new ListadoDto
            {
                Id = listado.ListaMotoId,
                Modelo = listado.Modelo,
                Precio = listado.Precio
            };

            _context.ListadoMotos.Remove(listado);
            await _context.SaveChangesAsync();

            return listadoEliminado;
        }

        public async Task<ListadoDto> Update(int id, ListadoDto listadoDto)
        {
            var listado = await _context.ListadoMotos.FindAsync(id);

            if (listado == null)
                return null;

            listado.Modelo = listadoDto.Modelo;
            listado.Precio = listadoDto.Precio;

            await _context.SaveChangesAsync();

            var listadoActualizado = new ListadoDto
            {
                Id = listado.ListaMotoId,
                Modelo = listado.Modelo,
                Precio = listado.Precio
            };

            return listadoActualizado;
        }
    }
}
