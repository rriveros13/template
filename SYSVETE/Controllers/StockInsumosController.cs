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
    public class StockInsumosController : ControllerBase
    {
        private readonly IStockServiceService _service;
        public StockInsumosController(IStockServiceService service)
        {
            _service = service;
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet]
        public async Task<IActionResult> ObtenerStocks()
        {
            try
            {
                var res = await _service.ObtenerStocks();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerStockPorId(int idStcok)
        {
            try
            {
                var res = await _service.ObtenerStockPorId(idStcok);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obteberPorInsumo")]
        public async Task<IActionResult> ObtenerStockPorInsumo(int idInsumo)
        {
            try
            {
                var res = await _service.ObtenerStockPorInsumo(idInsumo);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete("{idStock}")]
        public async Task<IActionResult> BorrarStock(int idStock)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.BorrarStock(idStock, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("updateStock")]
        public async Task<IActionResult> UpdateStock([FromBody] StockInsumo stock)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.UpdateStock(stock, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }

        }
    }
}
