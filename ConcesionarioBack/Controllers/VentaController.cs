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
    public class VentaController : ControllerBase
    {
        private ICommonService<VentaDto> _ventaService;

        public VentaController([FromKeyedServices("ventaService")]ICommonService<VentaDto> ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<IEnumerable<VentaDto>> Get() =>
            await _ventaService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<VentaDto>> GetById(int id)
        {
            var ventaDto = await _ventaService.GetById(id);

            return ventaDto == null ? NotFound() : Ok(ventaDto);
        }

        [HttpPost]
        public async Task<ActionResult<VentaDto>> Add(VentaDto ventaDto)
        {
            var addVenta = await _ventaService.Add(ventaDto);

            return CreatedAtAction(nameof(GetById), new { id = addVenta.VentaId }, addVenta);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VentaDto>> Update(int id, VentaDto ventaDto)
        {

            var updateVenta = await _ventaService.Update(id, ventaDto);

            return updateVenta==null ? NotFound():Ok(updateVenta);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VentaDto>> Delete(int id)
        {
            var deleteVenta = await _ventaService.Delete(id);

            return deleteVenta == null ? NotFound() : Ok(deleteVenta);
        }
    }
}
