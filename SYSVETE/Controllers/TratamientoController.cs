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
    public class TratamientoController : ControllerBase
    {
        private readonly ITratamientoService _service;
        public TratamientoController(ITratamientoService service)
        {
            _service = service;
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet]
        public async Task<IActionResult> ObtenerTratamiento()
        {
            try
            {
                var res = await _service.ObtenerTratamiento();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerTratamientoPorId(int idTratamiento)
        {
            try
            {
                var res = await _service.ObtenerTratamientoPorId(idTratamiento);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost]
        public async Task<IActionResult> AgregarTratamiento(Tratamiento dto)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.AgregarTratamiento(dto, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("updateTratamiento")]
        public async Task<IActionResult> UpdateTratamiento([FromBody] TratamientoDto tratamiento)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.UpdateTratamiento(tratamiento, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }

        }

        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete("{idTratamiento}")]
        public async Task<IActionResult> BorrarTratamiento(int idTratamiento)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.BorrarTratamiento(idTratamiento, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
