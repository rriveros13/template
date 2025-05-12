using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SYSVETE.Models;
using SYSVETE.Services;

namespace SYSVETE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuloController : ControllerBase
    {
        private readonly IModulo _service;

        public ModuloController(IModulo service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerModulos()
        {
            try
            {
                var res = await _service.ObtenerModulos();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerModuloPorId(int idMoudlo)
        {
            try
            {
                var res = await _service.ObtenerModuloPorID(idMoudlo);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpPut("updateModulo")]
        public async Task<IActionResult> UpdateModulo(Modulo modulo)
        {
            try
            {
                await _service.UpdateModulo(modulo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AgregarModulo(Modulo modulo)
        {
            try
            {
                await _service.AgregarModulo(modulo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

    }
}
