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
    public class ProcedimientoController : ControllerBase
    {
        private readonly IProcedimientoService _service;
        public ProcedimientoController(IProcedimientoService service)
        {
            _service = service;
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet]
        public async Task<IActionResult> ObtenerProcedimiento()
        {
            try
            {
                var res = await _service.ObtenerProcedimiento();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerProcedimientoPorId(int idProcedimiento)
        {
            try
            {
                var res = await _service.ObtenerProcedimientoPorId(idProcedimiento);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost]
        public async Task<IActionResult> AgregarProcedimiento([FromBody] ProcedimientoDto procedimientoDto)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.AgregarProcedimiento(procedimientoDto, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("updateProcedimiento")]
        public async Task<IActionResult> UpdateProcedimiento([FromBody] ProcedimientoDto procedimiento)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.UpdateProcedimiento(procedimiento, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete("{idProcedimiento}")]
        public async Task<IActionResult> BorrarProcedimiento(int idProcedimiento)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.BorrarProcedimiento(idProcedimiento, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
