using ConcesionarioBack.Common.Models;
using ConcesionarioBack.Domain.DTOs;
using ConcesionarioBack.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcesionarioBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListadoMotoController : ControllerBase
    {
        private ICommonService<ListadoDto> _listadoService;

        public ListadoMotoController([FromKeyedServices("listadoMoto")] ICommonService<ListadoDto> listadoService)
        {
            _listadoService = listadoService;
        }

        [HttpGet]
        public async Task<IEnumerable<ListadoDto>> Get() =>
            await _listadoService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<ListadoDto>> GetById(int id)
        {
            var listado = await _listadoService.GetById(id);

            return listado == null ? NotFound() : Ok(listado);
        }

        [HttpPost]
        public async Task<ActionResult<ListadoDto>> Add(ListadoDto listadoDto)
        {
            var addListado = await _listadoService.Add(listadoDto);

            return CreatedAtAction(nameof(Get), new { id = addListado.Id }, addListado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ListadoDto>> Update(int id, ListadoDto listadoDto)
        {
            var listadoActualizado = await _listadoService.Update(id, listadoDto);

            return listadoActualizado==null ? NotFound() : Ok(listadoActualizado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ListadoDto>> Delete(int id)
        {
            var listadoEliminado = await _listadoService.Delete(id);

            return listadoEliminado == null ? NotFound() : Ok(listadoEliminado);
        }
    }
}
