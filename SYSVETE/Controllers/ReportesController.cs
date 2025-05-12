
using Microsoft.AspNetCore.Mvc;
using SYSVETE.Helpers;
using System.IO;

namespace SYSVETE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly FReport _service;

        public ReportesController(FReport service)
        {
            _service = service;
        }

        [HttpPost("servicios")]
        public async Task<IActionResult> InformeServicio([FromBody] ParametrosRangos dto)
        {
            try
            {
                List<ParametrosReporteDto> param = new List<ParametrosReporteDto>();
                param.Add(new ParametrosReporteDto()
                {
                    NombreParametro = "FechaInicio",
                    ValorParametro = dto.FechaInicio
                });
                
                param.Add(new ParametrosReporteDto()
                {
                    NombreParametro = "FechaFin",
                    ValorParametro = dto.FechaFin
                });

                var res = _service.GenerarReporteconParmetros("Servicios", param.ToArray());
                return File(res.ToArray(), "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("ventasNoFacturadas")]
        public async Task<IActionResult> VentasNoFacturadas()
        {
            try
            {
                var res = _service.GenerarReporte("Ventas_no_facturadas");
                return File(res.ToArray(), "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("deudaProveedor")]
        public async Task<IActionResult> DeudaProveedor()
        {
            try
            {
                var res = _service.GenerarReporte("Deuda_proveedor");
                return File(res.ToArray(), "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("ObtenerComprasPorProveedor")]
        public async Task<IActionResult> ObtenerComprasPorProveedor(int idProveedor)
        {
            try
            {
                List<ParametrosReporteDto> param = new List<ParametrosReporteDto>();
                param.Add(new ParametrosReporteDto()
                {
                    NombreParametro = "IdProveedor",
                    ValorParametro = idProveedor
                });

                var res = _service.GenerarReporteconParmetros("ObtenerComprasPorProveedor", param.ToArray());
                return File(res.ToArray(), "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }   
        
        [HttpPost("ObtenerHistorialClinicoPorPatologia")]
        public async Task<IActionResult> ObtenerHistorialClinicoPorPatologia(int idPatologia)
        {
            try
            {
                List<ParametrosReporteDto> param = new List<ParametrosReporteDto>();
                param.Add(new ParametrosReporteDto()
                {
                    NombreParametro = "IdPatologia",
                    ValorParametro = idPatologia
                });

                var res = _service.GenerarReporteconParmetros("ObtenerHistorialClinicoPorPatologia", param.ToArray());
                return File(res.ToArray(), "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

    }
}
