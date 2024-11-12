using ConcesionarioBack.Common.Models;
using ConcesionarioBack.Domain.DTOs;
using ConcesionarioBack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConcesionarioBack.Infrastructure.Services
{
    public class VentaService : ICommonService<VentaDto>
    {
        private ConcesionarioContext _context;
      

        public VentaService(ConcesionarioContext context)
        {
            _context = context;
            
        }

        public async Task<IEnumerable<VentaDto>> Get()=>            
            await _context.Ventas.Select(v => new VentaDto
            {
                VentaId = v.VentaId,
                NombreComprador = v.NombreComprador,
                TelefonoComprador = v.TelefonoComprador,
                CorreoComprador = v.CorreoComprador,
                CarroId = v.CarroId,
                MotoId = v.MotoId
            }).ToListAsync();

        public async  Task<VentaDto> GetById(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);

            if (venta == null)
                return null;

            var ventaDto= new VentaDto
            {
                VentaId = venta.VentaId,
                NombreComprador = venta.NombreComprador,
                TelefonoComprador = venta.TelefonoComprador,
                CorreoComprador = venta.CorreoComprador,
                CarroId = venta.CarroId,
                MotoId = venta.MotoId
            };

            return ventaDto;
        }

        public async Task<VentaDto> Add(VentaDto ventaDto)
        {
            var venta = new Venta
            {
                NombreComprador = ventaDto.NombreComprador,
                TelefonoComprador = ventaDto.TelefonoComprador,
                CorreoComprador = ventaDto.CorreoComprador,
                CarroId = ventaDto.CarroId,
                MotoId = ventaDto.MotoId
            };

            await _context.Ventas.AddAsync(venta);

            if (ventaDto.CarroId.HasValue)
            {
                var carro = await _context.Carros.FindAsync(ventaDto.CarroId.Value);
                if (carro != null)
                {
                    carro.Activo = false;
                }
            }

            if (ventaDto.MotoId.HasValue)
            {
                var moto = await _context.Motos.FindAsync(ventaDto.MotoId.Value);
                if (moto != null)
                {
                    moto.Activo = false;
                }
            }

            await _context.SaveChangesAsync();

            var newVentaDto = new VentaDto
            {
                VentaId = venta.VentaId,
                NombreComprador = venta.NombreComprador,
                TelefonoComprador = venta.TelefonoComprador,
                CorreoComprador = venta.CorreoComprador,
                CarroId = venta.CarroId,
                MotoId = venta.MotoId
            };

            return newVentaDto;
        }

        public async Task<VentaDto> Update(int id,VentaDto ventaDto)
        {
            var venta = await _context.Ventas.FindAsync(id);

            if (venta == null)
                return null;

            venta.NombreComprador = ventaDto.NombreComprador;
            venta.TelefonoComprador = ventaDto.TelefonoComprador;
            venta.CorreoComprador = ventaDto.CorreoComprador;
            venta.CarroId = ventaDto.CarroId;
            venta.MotoId = ventaDto.MotoId;

            await _context.SaveChangesAsync();

            var updateVenta = new VentaDto
            {
                VentaId = venta.VentaId,
                NombreComprador = venta.NombreComprador,
                TelefonoComprador = venta.TelefonoComprador,
                CorreoComprador = venta.CorreoComprador,
                CarroId = venta.CarroId,
                MotoId = venta.MotoId
            };

            return updateVenta;
        }

        public async Task<VentaDto> Delete(int ventaId)
        {
            var venta = await _context.Ventas.FindAsync(ventaId);

            if (venta == null)
                return null;

            // Actualizar el estado de Activo a true para el Carro o Moto correspondiente
            if (venta.CarroId.HasValue)
            {
                var carro = await _context.Carros.FindAsync(venta.CarroId.Value);
                if (carro != null)
                {
                    carro.Activo = true;
                }
            }

            if (venta.MotoId.HasValue)
            {
                var moto = await _context.Motos.FindAsync(venta.MotoId.Value);
                if (moto != null)
                {
                    moto.Activo = true;
                }
            }

            var deleteVenta = new VentaDto
            {
                VentaId = venta.VentaId,
                NombreComprador = venta.NombreComprador,
                TelefonoComprador = venta.TelefonoComprador,
                CorreoComprador = venta.CorreoComprador,
                CarroId = venta.CarroId,
                MotoId = venta.MotoId
            };

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();


            return deleteVenta;
        }

    }
}
