using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IVentaService
    {
        Task<List<Venta>> ObtenerVenta();
        Task<Venta> ObtenerVentaPorId(int idVenta);
        Task<VentaDto> ObtenerMontos(int idVenta, int idUsuario);
        Task<int> AgregarVenta(Venta venta, int idUsuario);
        Task UpdateVenta(Venta venta, int idUsuario);
        Task FinalizarVenta(int idVenta, int idUsuario);

        Task BorrarVenta(int idVenta, int idUsuario);
    }
    public class VentaService : IVentaService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public VentaService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<Venta>> ObtenerVenta()
        {
            try
            {

                var ventasSinDetalle2 = await _context.Ventas
                .Where(v => !_context.VentaDetalles.Any(d => d.IdVenta == v.IdVenta))
                .ToListAsync();
                foreach (var updt in ventasSinDetalle2)
                {
                    updt.Borrado = true;
                    await _context.SaveChangesAsync();

                }

                var venta = await _context.Ventas.Include( x => x.IdClienteNavigation)
                    .ThenInclude( m => m.IdPersonaNavigation)
                    .ToListAsync();
                return venta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Venta> ObtenerVentaPorId(int idVenta)
        {
            try
            {
                var venta = await _context.Ventas.Include(x => x.IdClienteNavigation)
                    .ThenInclude(m => m.IdPersonaNavigation)
                    .Where(u => u.IdVenta == idVenta)
                    .FirstOrDefaultAsync();
                return venta;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> AgregarVenta(Venta venta, int idUsuario)
        {
            //int maxNroBoleta = 0;
             var maxNroBoleta = await _context.Ventas
                    .MaxAsync(v => v.NroBoleta);
            venta.NroBoleta = maxNroBoleta + 1;
            try
            {
                venta.IdUsuarioInserto = idUsuario;
                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();
                return venta.IdVenta;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<VentaDto?> ObtenerMontos(int idVenta, int idUsuario)
        {
            VentaDto montos = new VentaDto();

            try
            {
                if (idVenta == 0)
                {
                    throw new Exception("La compra no es valida!");
                }

                var MontoTotal = await _context.VentaDetalles
                    .Where(cd => cd.IdVenta == idVenta)
                    .ToListAsync();
                if (MontoTotal.Any())
                {
                    foreach (var monto in MontoTotal)
                    {
                        montos.MontoTotal += (monto.Cantidad * monto.Precio);
                    }
                }


                var deuda = await _context.PagoVentas.Include(x => x.IdVentaNavigation)
                                    .Where(u => u.IdVenta == idVenta)
                                    .ToListAsync();
                if (deuda != null)
                {
                    foreach (var d in deuda)
                    {
                        montos.MontoAbonado += d.MontoPagado;
                    }
                }

                montos.SaldoPendiente = montos.MontoTotal - montos.MontoAbonado;
                if (montos.SaldoPendiente <= 0 && montos.MontoTotal > 0)
                {
                    var compraUpdate = await _context.Ventas
                    .Where(mm => mm.IdVenta == idVenta)
                    .FirstOrDefaultAsync();
                    if (compraUpdate != null && compraUpdate.Facturado == false)
                    {
                        Venta dto = new Venta()
                        {
                            IdVenta = compraUpdate.IdVenta,
                            NroBoleta = compraUpdate.NroBoleta,
                            IdCliente = compraUpdate.IdCliente,
                            Facturado = true,
                            FechaVenta = compraUpdate.FechaVenta,
                        };

                        await UpdateVenta(dto, idUsuario);
                    }
                }

                return montos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateVenta(Venta dto, int idUsuario)
        {

            var venta = await _context.Ventas
                .Where(mm => mm.IdVenta == dto.IdVenta)
                .FirstOrDefaultAsync();

            if (venta == null)
            {
                throw new Exception($"No se puede encontrar el venta {dto.IdVenta}");
            }
            else
            {
                venta.NroBoleta = dto.NroBoleta;
                venta.FechaVenta = dto.FechaVenta;
                venta.IdCliente = dto.IdCliente;
                venta.Facturado = dto.Facturado;
                venta.IdUsuarioModifico = idUsuario;
                venta.FechaModificado = DateTime.Now;
            }
            await _context.SaveChangesAsync();
        }

        public async Task FinalizarVenta(int IdVenta, int idUsuario)
        {

            var venta = await _context.Ventas
                .Where(mm => mm.IdVenta == IdVenta)
                .FirstOrDefaultAsync();

            if (venta == null)
            {
                throw new Exception($"No se puede encontrar el venta {IdVenta}");
            }
            else
            {
                venta.Finalizado = true;
                venta.IdUsuarioModifico = idUsuario;
                venta.FechaModificado = DateTime.Now;
            }
            await _context.SaveChangesAsync();
        }

        public async Task BorrarVenta(int idVenta, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idVenta),
                        new SqlParameter("@tabla", "Tratamiento"));

                    var venta = await _context.Ventas.Where(r => r.IdVenta == idVenta)
                        .SingleOrDefaultAsync();

                    if (venta == null)
                    {
                        throw new Exception("No existe el venta!");
                    }

                    venta.Borrado = true;
                    venta.IdUsuarioModifico = idUsuario;
                    venta.FechaBorrado = DateTime.Now;
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
    }
}
