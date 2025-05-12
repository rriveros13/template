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
    public class EspecieController : ControllerBase
    {
        private readonly IEspecieService _service;
        public EspecieController(IEspecieService service)
        {
            _service = service;
        }
        [HttpGet]
        [Autorizar(AccionesDefinidas.Consultar)]
        public async Task<IActionResult> ObtenerEspecie()
        {
            try
            {
                var res = await _service.ObtenerEspecie();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpGet("obtenerPorId")]
        [Autorizar(AccionesDefinidas.Consultar)]
        public async Task<IActionResult> ObtenerEspeciePorId(int idEspecie)
        {
            try
            {
                var res = await _service.ObtenerEspeciePorId(idEspecie);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpPost]
        [Autorizar(AccionesDefinidas.Agregar)]
        public async Task<IActionResult> AgregarEspecie(Especie especie)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.AgregarEspecie(especie, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpPut("updateEspecie")]
        [Autorizar(AccionesDefinidas.Editar)]
        public async Task<IActionResult> UpdateEspecie(EspecieDto especie)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.UpdateEspecie(especie, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }

        }
        [HttpDelete("{IdEspecie}")]
        [Autorizar(AccionesDefinidas.Borrar)]
        public async Task<IActionResult> BorrarEspecie(int idEspecie)
        {
            try
            {
                await _service.BorrarEspecie(idEspecie);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

    }
}
