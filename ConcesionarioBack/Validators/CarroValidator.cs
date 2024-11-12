using ConcesionarioBack.Domain.DTOs;
using FluentValidation;
using ConcesionarioBack.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcesionarioBack.Validators
{
    public class CarroValidator : AbstractValidator<CarroDto>
    {
        private readonly ConcesionarioContext _context;

        public CarroValidator(ConcesionarioContext context)
        {
            _context = context;

            RuleFor(x => x.Activo).NotNull().WithMessage("'Activo' no debería estar vacío.");
            RuleFor(x => x.Nuevo).NotNull().WithMessage("'Nuevo' no debería estar vacío.");

            RuleFor(x => x.Valor)
                .MustAsync(async (carro, valor, cancellation) => await ValidarPrecio(carro))
                .WithMessage("El precio del carro usado debe ser al menos el 85% del precio listado.");
            RuleFor(x => x.Valor).LessThan(250000000).WithMessage("El precio del carro no debería ser mayor a 250,000,000.");

            RuleFor(x => x)
                .MustAsync(async (carro, cancellation) => await ValidarCantidadDeCarros(carro))
                .WithMessage("No se pueden crear más de 10 carros.");

            RuleFor(x => x.Valor)
                .MustAsync(async (carro, valor, cancellation) => await ValidarPrecioCarroNuevo(carro))
                .WithMessage("El precio del carro nuevo debe ser igual al precio listado.");

            RuleFor(x => x.Valor)
                .MustAsync(async (carro, valor, cancellation) => await valorMotoActualizacion(carro))
                .WithMessage("El Carro no puede costar mas de su valor en la lista");
        }

        private async Task<bool> ValidarPrecio(CarroDto carro)
        {
            if (carro.Nuevo)
            {
                return true;
            }

            var listadoCarro = await _context.ListadoCarros.FirstOrDefaultAsync(l => l.Modelo == carro.Modelo);
            
            if (listadoCarro == null)
            {
                return true;
            }

            var precioMinimo = listadoCarro.Precio * 0.85m;
            return carro.Valor >= precioMinimo;
        }

        private async Task<bool> valorMotoActualizacion(CarroDto carro)
        {
            if (carro.EsActualizacion)
            {
                var carroActual = await _context.ListadoCarros.FirstOrDefaultAsync(m => m.Modelo == carro.Modelo);

                if (carroActual != null && carro.Valor>carroActual.Precio )
                    return false;

            }
            return true;
        }

        private async Task<bool>ValidarPrecioCarroNuevo(CarroDto carro)
        {
            if (carro.Nuevo)
            {
                var listadoCarro = await _context.ListadoCarros.FirstOrDefaultAsync(l => l.Modelo == carro.Modelo);
                
                if (listadoCarro == null)
                     return true;

                return carro.Valor == listadoCarro.Precio;
            }

            return true;
        }

        private async Task<bool> ValidarCantidadDeCarros(CarroDto carroDto)
        {
            if (carroDto.EsActualizacion)
                return true;

            var cantidadDeCarros = await _context.Carros.CountAsync();
            return cantidadDeCarros < 10;
        }
    }
}