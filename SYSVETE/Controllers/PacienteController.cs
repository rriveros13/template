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
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _service;
        public PacienteController(IPacienteService service)
        {
            _service = service;
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet]
        public async Task<IActionResult> ObtenerPacientes()
        {
            try
            {
                var res = await _service.ObtenerPacientes();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerPacientePorId(int idPaciente)
        {
            try
            {
                var res = await _service.ObtenerPacientePorId(idPaciente);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost]
        public async Task<IActionResult> AgregarPaciente(Paciente paciente)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.AgregarPaciente(paciente, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [Autorizar(AccionesDefinidas.Editar)]
        [HttpPut("updatePaciente")]
        public async Task<IActionResult> UpdatePaciente([FromBody] PacienteDto pacienteDto)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.UpdatePaciente(pacienteDto, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete()]
        public async Task<IActionResult> BorrarPAciente(int idPaciente)
        {
            try
            {
                await _service.BorrarPAciente(idPaciente);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
