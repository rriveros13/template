using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SYSVETE.Autorizacion;
using SYSVETE.Models;
using SYSVETE.Models.DTOs;
using SYSVETE.Services;
namespace SYSVETE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadMedidaController : ControllerBase
    {

        private readonly IUnidadMedidaService _service;
        public UnidadMedidaController(IUnidadMedidaService service)
        {
            _service = service;
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet]
        public async Task<IActionResult> ObtenerUnidadMedida()
        {
            try
            {
                var res = await _service.ObtenerUnidadMedida();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerUnidadMedidaPorId(int idUnidad)
        {
            try
            {
                var res = await _service.ObtenerUnidadMedidaPorId(idUnidad);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost]
        public async Task<IActionResult> AgregarUnidadMedida(UnidadMedida unidaMedida)
        {
            try
            {
                await _service.AgregarUnidadMedida(unidaMedida);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("updateUnidad")]
        public async Task<IActionResult> UpdateUnidadMedida(UnidadMedida unidadMedida)
        {
            try
            {
                await _service.UpdateUnidadMedida(unidadMedida);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }

        }

        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete("{idUnidad}")]
        public async Task<IActionResult> BorrarUnidadMedida(int idUnidad)
        {
            try
            {
                await _service.BorrarUnidadMedida(idUnidad);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
