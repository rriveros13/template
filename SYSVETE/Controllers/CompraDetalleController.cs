using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SYSVETE.Models;
using SYSVETE.Models.DTOs;
using SYSVETE.Services;
using SYSVETE.Autorizacion;

namespace SYSVETE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraDetalleController : ControllerBase
    {
        private readonly ICompraDetalleService _service;

        public CompraDetalleController(ICompraDetalleService service)
        {
            _service = service;
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("{idCompra}")]
        public async Task<IActionResult> ObtenerDetallesDeCompra(int idCompra)
        {
            try
            {
                var res = await _service.ObtenerDetallesDeCompra(idCompra);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerDetallePorId(int idDetalle)
        {
            try
            {
                var res = await _service.ObtenerDetallePorId(idDetalle);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("updateCompraDetalle")]
        public async Task<IActionResult> UpdateCompraDetalle([FromBody] CompraDetalleDto dto)
        {
            try
            {
                await _service.UpdateCompraDetalle(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost("addCompraDetalle")]
        public async Task<IActionResult> AgregarCompraDetalle([FromBody] CompraDetalleDto dto)
        {
            try
            {

                await _service.AgregarCompraDetalle(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete]
        public async Task<IActionResult> BorrarCompraDetalle(int idDetalle)
        {
            try
            {
                await _service.BorrarCompraDetalle(idDetalle);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
