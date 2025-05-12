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
    public class ImpuestoController : ControllerBase
    {
        private readonly IImpuestoService _service;
        public ImpuestoController(IImpuestoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Autorizar(AccionesDefinidas.Consultar)]
        public async Task<IActionResult> ObtenerImpuesto()
        {
            try
            {
                var res = await _service.ObtenerImpuesto();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpGet("obtenerPorId")]
        [Autorizar(AccionesDefinidas.Consultar)]
        public async Task<IActionResult> ObtenerImpuestoPorId(int idImpuesto)
        {
            try
            {
                var res = await _service.ObtenerImpuestoPorId(idImpuesto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost]
        public async Task<IActionResult> AgregarImpuesto(Impuesto impuesto)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.AgregarImpuesto(impuesto, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("updateImpuesto")]
        public async Task<IActionResult> UpdateImpuesto([FromBody] ImpuestoDtocs impuesto)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.UpdateImpuesto(impuesto, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }

        }

        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete("{IdImpuesto}")]
        public async Task<IActionResult> BorrarImpuesto(int idImpuesto)
        {
            try
            {
                await _service.BorrarImpuesto(idImpuesto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
