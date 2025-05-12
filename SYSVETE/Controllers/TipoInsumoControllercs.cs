using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SYSVETE.Autorizacion;
using SYSVETE.Models;
using SYSVETE.Services;

namespace SYSVETE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoInsumoController : ControllerBase
    {
        private readonly ITipoInsumoService _service;
        public TipoInsumoController(ITipoInsumoService service)
        {
            _service = service;
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet]
        public async Task<IActionResult> ObtenerTipoInsumos()
        {
            try
            {
                var res = await _service.ObtenerTipoInsumos();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerTipoInsumoPorId(int idTipoInsumo)
        {
            try
            {
                var res = await _service.ObtenerTipoInsumoPorId(idTipoInsumo);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost]
        public async Task<IActionResult> AgregarTipoInsumo(TipoInsumo tipoInsumo)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.AgregarTipoInsumo(tipoInsumo, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("updateRol")]
        public async Task<IActionResult> UpdateTipoInusmo(TipoInsumo tipoInsumo)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.UpdateTipoInusmo(tipoInsumo, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete("{idTipoInsumo}")]
        public async Task<IActionResult> BorrarTipoInsumo(int idTipoInsumo)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.BorrarTipoInsumo(idTipoInsumo, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
