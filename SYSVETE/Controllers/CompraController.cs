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
    public class CompraController : ControllerBase
    {
        private readonly ICompraService _service;

        public CompraController(ICompraService service)
        {
            _service = service;
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet]
        public async Task<IActionResult> ObtenerCompras()
        {
            try
            {
                var res = await _service.ObtenerCompras();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerComprasPorId(int idCompra)
        {
            try
            {
                var res = await _service.ObtenerComprasPorId(idCompra);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerMontos")]
        public async Task<IActionResult> ObtenerMontos(int idCompra)
        {
            try
            {
                var res = await _service.ObtenerMontos(idCompra);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("updateCompra")]
        public async Task<IActionResult> UpdateCompra([FromBody] CompraDto dto)
        {
            try
            {
                await _service.UpdateCompra(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost]
        public async Task<IActionResult> AgregarCompra([FromBody] CompraDto dto)
        {
            try
            {
                     return Ok(await _service.AgregarCompra(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("finalizar")]
        public async Task<IActionResult> FinalizarVenta([FromBody] int idCompra)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.FinalizarCompra(idCompra, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }

        }

        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete]
        public async Task<IActionResult> BorrarCompra(int idCompra)
        {
            try
            {
                await _service.BorrarCompra(idCompra);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
