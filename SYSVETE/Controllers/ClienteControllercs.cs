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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;
        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet]
        public async Task<IActionResult> ObtenerClientes()
        {
            try
            {
                var res = await _service.ObtenerClientes();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerTipoClientePorId(int idCliente)
        {
            try
            {
                var res = await _service.ObtenerClientePorId(idCliente);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost]
        public async Task<IActionResult> AgregarCliente([FromBody] ClienteDto clientedto)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.AgregarCliente(clientedto, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("updateCliente")]
        public async Task<IActionResult> UpdateCliente([FromBody] ClienteDto cliente)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.UpdateCliente(cliente, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete]
        public async Task<IActionResult> BorrarCliente(int idCliente)
        {
            try
            {
                await _service.BorrarCliente(idCliente);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
