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
    public class PermisoController : ControllerBase
    {
        private readonly IPermiso _service;

        public PermisoController(IPermiso service)
        {
            _service = service;
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet]
        public async Task<IActionResult> ObtenerPermisos()
        {
            try
            {
                var res = await _service.ObtenerPermisos();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerPermisoPorID(int idPermiso)
        {
            try
            {
                var res = await _service.ObtenerPermisoPorID(idPermiso);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("updatePermiso")]
        public async Task<IActionResult> UpdatePermiso([FromBody] PermisoDto permiso)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.UpdatePermiso(permiso, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost("addPermiso")]
        public async Task<IActionResult> AgregarPermiso([FromBody] PermisoDto dto)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.AgregarPermiso(dto, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete("{idPermiso}")]
        public async Task<IActionResult> BorrarRol(int idPermiso)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.BorrarPermiso(idPermiso, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
