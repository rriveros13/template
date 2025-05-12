using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SYSVETE.Autorizacion;
using SYSVETE.Models;
using SYSVETE.Services;

namespace SYSVETE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _service;

        public UsuarioController(IUsuario service)
        {
            _service = service;
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] AuthenticateRequest request)
        {
            try
            {
                var usuarioAuth = await _service.Autenticar(request);

                if (usuarioAuth == null)
                {
                    return BadRequest(new { mensaje = "Usuario o contrasena incorrecta" });
                }

                return Ok(usuarioAuth);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            try
            {
                var res = await _service.ObtenerUsuarios();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(int idUsuario)
        {
            try
            {
                var res = await _service.ObtenerUsuarioPorID(idUsuario);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost]
        public async Task<IActionResult> InsertarUsuario([FromBody] UsuarioNuevo dto)
        {
            try
            {

                var res = await _service.InsertarUsuario(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Borrar)]
        [HttpDelete("{idUsuario}")]
        public async Task<IActionResult> BorrarUsuario(int idUsuario)
        {
            try
            {
                await _service.BorrarUsuario(idUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

    }
}
