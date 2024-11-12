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
    public class MotoController : ControllerBase
    {
        private IValidator<MotoDto> _validator;
        private ICommonVehiculosService<MotoDto> _motoService;

        public MotoController(IValidator<MotoDto> validator, [FromKeyedServices("vehiculosService")]ICommonVehiculosService<MotoDto> motoService)
        {
            _validator = validator;
            _motoService = motoService;
        }

        [HttpGet]
        public async Task<IEnumerable<MotoDto>> Get() =>
           await _motoService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<MotoDto>> GetById(int id)
        {
            var motoDto = await _motoService.GetById(id);

            return motoDto == null ? NotFound() : Ok(motoDto);
        }

        [HttpPost]
        public async Task<ActionResult<MotoDto>> Add(MotoDto motoDto)
        {
            motoDto.EsActualizacion= false;

            var validationResult = await _validator.ValidateAsync(motoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

           var addMoto= await _motoService.Add(motoDto);

            return CreatedAtAction(nameof(GetById), new { id = addMoto.MotoId }, addMoto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MotoDto>> Update(int id, MotoDto motoDto)
        {
            motoDto.EsActualizacion = true;

            var validationResult = await _validator.ValidateAsync(motoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updateMoto = await _motoService.Update(id, motoDto);

            return updateMoto == null ? NotFound() : Ok(updateMoto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MotoDto>> Delete(int id)
        {
            var deleteMoto = await _motoService.Delete(id);

            return deleteMoto==null ? NotFound():Ok(deleteMoto);
        }

        [HttpGet("sumaValoresMotos")]
        public async Task<ActionResult<string>> SumarValores()
        {
            var respuesta= await _motoService.SumarValores();

            return respuesta;
        }
    }
}
