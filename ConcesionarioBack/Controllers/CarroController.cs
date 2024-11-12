using ConcesionarioBack.Common.Models;
using ConcesionarioBack.Domain.DTOs;
using ConcesionarioBack.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ConcesionarioBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroController : ControllerBase
    {
        private IValidator<CarroDto> _validator;
        private ICommonVehiculosService<CarroDto> _carroService;

        public CarroController(IValidator<CarroDto> validator, [FromKeyedServices("vehiculosService")]ICommonVehiculosService <CarroDto> carroService)
        {
            _validator = validator;
            _carroService = carroService;
        }

        [HttpGet]
        public async Task<IEnumerable<CarroDto>> Get() =>
            await _carroService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<CarroDto>> GetById(int id)
        {
            var carroDto = await _carroService.GetById(id);

            return carroDto==null ? NotFound() : Ok(carroDto);
        }

        [HttpPost]
        public async Task<ActionResult<CarroDto>> Add(CarroDto carroDto)
        {
            carroDto.EsActualizacion= false;

            var validationResult =await _validator.ValidateAsync(carroDto);

            if (!validationResult.IsValid) 
            {
                return BadRequest(validationResult.Errors);
            }
            
            var addCarro = await _carroService.Add(carroDto);

            return CreatedAtAction(nameof(GetById), new { id = addCarro.CarroId }, addCarro);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarroDto>> Update(int id, CarroDto carroDto)
        {
            carroDto.EsActualizacion = true;

            var validationResult = await _validator.ValidateAsync(carroDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

           var updateCarro = await _carroService.Update(id, carroDto);

            return updateCarro==null ? NotFound():Ok(updateCarro);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CarroDto>> Delete(int id)
        {
            var deleteCarro = await _carroService.Delete(id);

            return deleteCarro==null ? NotFound():Ok(deleteCarro);

        }

        [HttpGet("sumaValoresCarros")]
        public async Task<ActionResult<string>> SumarValores()
        {
            var respuesta= await _carroService.SumarValores();

            return respuesta;
        }
    }
}
