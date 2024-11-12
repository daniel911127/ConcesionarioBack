using ConcesionarioBack.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcesionarioBack.Domain.DTOs;
using ConcesionarioBack.Domain.Interfaces;

namespace ConcesionarioBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListadoCarroController : ControllerBase
    {
        private ICommonService<ListadoDto > _listadoService;

        public ListadoCarroController([FromKeyedServices("listadoCarro")]ICommonService<ListadoDto> listadoService)
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

            return CreatedAtAction(nameof(GetById), new { id = addListado.Id }, addListado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ListadoDto>> Update(int id, ListadoDto listadoDto)
        {
            var updateListado = await _listadoService.Update(id, listadoDto);

            return updateListado==null ? NotFound():Ok(updateListado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ListadoDto>> Delete(int id)
        {
            var deleteListado = await _listadoService.Delete(id);

            return deleteListado == null ? NotFound() : Ok(deleteListado);
        }
    }
}
