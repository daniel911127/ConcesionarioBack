using ConcesionarioBack.Common.Models;
using ConcesionarioBack.Domain.DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConcesionarioBack.Validators
{
    public class MotoValidator : AbstractValidator<MotoDto>
    {
        private readonly ConcesionarioContext _context;

        public MotoValidator(ConcesionarioContext context) 
        { 
            _context = context;

            RuleFor(x => x.Activo).NotNull().WithMessage("'Activo' no debería estar vacío.");
            RuleFor(x => x.Nuevo).NotNull().WithMessage("'Nuevo' no debería estar vacío.");
            RuleFor(x => x.Cilindraje).LessThan(401).WithMessage("El cilindraje de la moto no debería ser menor o igual a 400cc.");

            RuleFor(x => x)
                .MustAsync(async (moto, cancellation) => await ValidarCantidadDeMotos(moto))
                .WithMessage("No se pueden crear más de 15 motos.");

            RuleFor(x => x.Valor)
                .MustAsync(async (moto, valor, cancellation) => await ValidarPrecioMotoNueva(moto))
                .WithMessage("El precio de la moto nueva debe ser igual al precio listado.");

            RuleFor(x => x.Valor)
                .MustAsync(async (moto, valor, cancellation) => await valorMotoActualizacion(moto))
                .WithMessage("El precio de la moto no puede ser mayor al precio de la lista.");
        }

        private async Task<bool> ValidarCantidadDeMotos(MotoDto moto)
        {
            if(moto.EsActualizacion)
                return true;

            var cantidadDeMotos = await _context.Motos.CountAsync();
            return cantidadDeMotos < 15;
        }

        private async Task<bool>valorMotoActualizacion(MotoDto moto)
        {
            if (moto.EsActualizacion)
            {
                if (moto.EsActualizacion)
                {
                    var motoActual = await _context.ListadoMotos.FirstOrDefaultAsync(m => m.Modelo == moto.Modelo);

                    if (motoActual != null && moto.Valor > motoActual.Precio)
                    {
                        return false;
                    }
                }
                
            }
            return true;
        }

        private async Task<bool> ValidarPrecioMotoNueva(MotoDto moto)
        {
            if (moto.Nuevo)
            {
                var listadoMoto = await _context.ListadoMotos.FirstOrDefaultAsync(l => l.Modelo == moto.Modelo);

                if (listadoMoto == null)
                    return true;

                return moto.Valor == listadoMoto.Precio;
            }

            return true;
        }

    }
}
