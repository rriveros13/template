using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IVentaDetalleService
    {
        Task<List<VentaDetalle>> ObtenerVentaDetalle();
        Task<List<VentaDetalle>> ObtenerVentaDetallePorCabecera(int idVenta);

        Task<VentaDetalle> ObtenerVentaDetallePorId(int idVentaDetalle);
        Task AgregarVentaDetalle(VentaDetalle ventaDetalle, int idUsuario);
        Task UpdateVentaDetalle(VentaDetalle ventaDetalle, int idUsuario);
        Task BorrarVentaDetalle(int idVentaDetalle, int idUsuario);
    }
    public class VentaDetalleService : IVentaDetalleService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public VentaDetalleService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<VentaDetalle>> ObtenerVentaDetalle()
        {
            try
            {
                var RES = ObtenerCabeceraSinDetalle();

                var ventaDetalle = await _context.VentaDetalles.Include( x => x.IdHistorialNavigation)
                    .ThenInclude( p => p.IdProcedimientoNavigation)
                    .Include(pr => pr.IdHistorialNavigation).ThenInclude( m => m.IdTratamientoNavigation)
                    .Include(u => u.IdHistorialNavigation).ThenInclude( s => s.IdPacienteNavigation)
                    .Include(r => r.IdHistorialNavigation).ThenInclude(q => q.IdVacunaNavigation)
                     .Include(e => e.IdInsumoNavigation).ThenInclude( i => i.IdImpuestoNavigation)
                    .ToListAsync();
                return ventaDetalle;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<VentaDetalle> ObtenerVentaDetallePorId(int idVentaDetalle)
        {
            try
            {
                var ventaDetalle = await _context.VentaDetalles.Include(x => x.IdHistorialNavigation)
                    .ThenInclude(p => p.IdProcedimientoNavigation)
                    .Include(pr => pr.IdHistorialNavigation).ThenInclude(m => m.IdTratamientoNavigation)
                    .Include(u => u.IdHistorialNavigation).ThenInclude(s => s.IdPacienteNavigation)
                    .Include(r => r.IdHistorialNavigation).ThenInclude(q => q.IdVacunaNavigation)
                     .Include(e => e.IdInsumoNavigation).ThenInclude(i => i.IdImpuestoNavigation)
                    .Where(o => o.IdVentaDetalle == idVentaDetalle)
                    .FirstOrDefaultAsync();
                return ventaDetalle;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Venta>> ObtenerCabeceraSinDetalle()
        {
            try
            {

                var ventasSinDetalle2 = await _context.Ventas
                .Where(v => !_context.VentaDetalles.Any(d => d.IdVenta == v.IdVenta))
                .ToListAsync();
                foreach (var venta in ventasSinDetalle2)
                {
                    venta.Borrado = true;
                    await _context.SaveChangesAsync();

                }
                return ventasSinDetalle2;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<VentaDetalle>> ObtenerVentaDetallePorCabecera(int idVenta)
        {
            try
            {

                


                    var ventaDetalle = await _context.VentaDetalles.Include(x => x.IdHistorialNavigation)
                    .ThenInclude(p => p.IdProcedimientoNavigation)
                    .Include(pr => pr.IdHistorialNavigation).ThenInclude(m => m.IdTratamientoNavigation)
                    .Include(u => u.IdHistorialNavigation).ThenInclude(s => s.IdPacienteNavigation)
                    .Include(r => r.IdHistorialNavigation).ThenInclude(q => q.IdVacunaNavigation)
                     .Include(e => e.IdInsumoNavigation).ThenInclude(i => i.IdImpuestoNavigation)
                    .Where(o => o.IdVenta == idVenta)
                    .ToListAsync();
                return ventaDetalle;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AgregarVentaDetalle(VentaDetalle ventaDetalle, int idUsuario)
        {
            try
            {
                ventaDetalle.IdUsuarioInserto = idUsuario;
                _context.VentaDetalles.Add(ventaDetalle);
                await _context.SaveChangesAsync();
                if (ventaDetalle.IdInsumo != null)
                await RestarInsumoStock(ventaDetalle);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateVentaDetalle(VentaDetalle dto, int idUsuario)
        {

            var ventaDetalle = await _context.VentaDetalles
                .Where(mm => mm.IdVentaDetalle == dto.IdVentaDetalle)
                .FirstOrDefaultAsync();

            if (ventaDetalle == null)
            {
                throw new Exception($"No se puede encontrar el venta {dto.IdVentaDetalle}");
            }
            else
            {
                ventaDetalle.IdVenta = dto.IdVenta;
                ventaDetalle.IdInsumo = dto.IdInsumo;
                ventaDetalle.Cantidad = dto.Cantidad;
                ventaDetalle.Precio = dto.Precio;
                ventaDetalle.Descripcion = dto.Descripcion;
                ventaDetalle.IdUsuarioModifico = idUsuario;
                ventaDetalle.FechaModificado = DateTime.Now;
            }
            await _context.SaveChangesAsync();
        }
        public async Task BorrarVentaDetalle(int idVentaDetalle, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idVentaDetalle),
                        new SqlParameter("@tabla", "VentaDetalle"));

                    var ventaDetalle = await _context.VentaDetalles.Where(r => r.IdVentaDetalle == idVentaDetalle)
                        .SingleOrDefaultAsync();

                    if (ventaDetalle == null)
                    {
                        throw new Exception("No existe el venta!");
                    }
                     var servicioNoFinalizado = await _context.VentaDetalles
                     .Include(e => e.IdVentaNavigation)
                    .Where(o => o.IdVentaDetalle == idVentaDetalle)
                    .FirstOrDefaultAsync();

                    if (servicioNoFinalizado.IdVentaNavigation.Facturado != true && servicioNoFinalizado.IdVentaNavigation.Facturado == false)
                    {
                        var historialesNoFacturados = await _context.HistorialClinicoS.Include(t => t.IdVacunaNavigation)
                         .Where(u => u.IdHistorial == servicioNoFinalizado.IdHistorial)
                            .FirstOrDefaultAsync();
                        historialesNoFacturados.Facturado = false;
                        await _context.SaveChangesAsync();
                    }

                    ventaDetalle.Borrado = true;
                    ventaDetalle.IdUsuarioModifico = idUsuario;
                    ventaDetalle.FechaBorrado = DateTime.Now;
                    await _context.SaveChangesAsync();
                    await scope.CommitAsync();

                }
                catch (Exception)
                {
                    await scope.RollbackAsync();
                    throw;
                }

            }
        }

        public async Task RestarInsumoStock(VentaDetalle dto)
        {
            try
            {
                var stockActual = await _context.StockInsumos
                    .Where(cd => cd.IdInsumo == dto.IdInsumo)
                    .SingleOrDefaultAsync();

                if (stockActual != null)
                {
                    if ((stockActual.CantidadActual - dto.Cantidad) >= 0)
                    {
                        stockActual.IdInsumo = stockActual.IdInsumo;
                        stockActual.CantidadActual = stockActual.CantidadActual - dto.Cantidad;
                        stockActual.FechaModificado = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }

                    else
                    {
                        throw new Exception($"No hay suficiente stock disponible");

                    }
                }

            }

            catch (Exception e)
            {

                throw;
            }
        }
    }
}
