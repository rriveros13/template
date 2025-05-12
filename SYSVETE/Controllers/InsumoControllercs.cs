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
    public class InsumoController : ControllerBase
    {
        private readonly IInsumoService _service;
        public InsumoController(IInsumoService service)
        {
            _service = service;
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet]
        public async Task<IActionResult> ObtenerInsumos()
        {
            try
            {
                var res = await _service.ObtenerInsumos();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerInsumoPorId(int idInsumo)
        {
            try
            {
                var res = await _service.ObtenerInsumoPorId(idInsumo);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("sp")]
        public async Task<IActionResult> LlamarProcedimientoAlmacenado(int idInsumo, DateTime fecha)
        {
            try
            {
                var res = await _service.LlamarProcedimientoAlmacenado(idInsumo, fecha);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost]
        public async Task<IActionResult> AgregarInsumo([FromBody] InsumoDto dto)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.AgregarInsumo(dto, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("updateInsumo")]
        public async Task<IActionResult> UpdateInusmo(Insumo insumo)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.UpdateInusmo(insumo, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }

        }

        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete("{idInsumo}")]
        public async Task<IActionResult> BorrarInsumo(int idInsumo)
        {
            try
            {
                await _service.BorrarInsumo(idInsumo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
       }

    }
