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
    public class DeudaProveedorController : ControllerBase
    {
        private readonly IDeudaProveedorService _service;
        public DeudaProveedorController(IDeudaProveedorService service)
        {
            _service = service;
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet]
        public async Task<IActionResult> ObtenerDeudaProveedor()
        {
            try
            {
                var res = await _service.ObtenerDeudaProveedor();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerDeudaProveedorPorId(int idDeuda)
        {
            try
            {
                var res = await _service.ObtenerDeudaProveedorPorId(idDeuda);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorIdCompra")]
        public async Task<IActionResult> ObtenerDeudaProveedorPorCompra(int idCompra)
        {
            try
            {
                var res = await _service.ObtenerDeudaProveedorPorCompra(idCompra);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost]
        public async Task<IActionResult> AgregarDeudaProveedor(DeudaProveedor dto)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.AgregarDeudaProveedor(dto, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("updateDeudaProveedor")]
        public async Task<IActionResult> UpdateDeudaProveedor([FromBody] DeudaProveedorDto tratamiento)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.UpdateDeudaProveedor(tratamiento, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }

        }

        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete("{idDeuda}")]
        public async Task<IActionResult> BorrarDeudaProveedor(int idDeuda)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.BorrarDeudaProveedor(idDeuda, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
