using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IDeudaProveedorService
    {
        Task<List<DeudaProveedor>> ObtenerDeudaProveedor();
        Task<DeudaProveedor> ObtenerDeudaProveedorPorId(int idDeuda);
        Task<List<DeudaProveedor>> ObtenerDeudaProveedorPorCompra(int idCompra);

        Task AgregarDeudaProveedor(DeudaProveedor deuda, int idUsuario);
        Task UpdateDeudaProveedor(DeudaProveedorDto deuda, int idUsuario);
        Task BorrarDeudaProveedor(int idDeuda, int idUsuario);
    }
    public class DeudaProveedorService : IDeudaProveedorService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public DeudaProveedorService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<DeudaProveedor>> ObtenerDeudaProveedor()
        {
            try
            {
                var deuda = await _context.DeudaProveedores
                    .ToListAsync();
                return deuda;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DeudaProveedor> ObtenerDeudaProveedorPorId(int idDeuda)
        {
            try
            {
                var deuda = await _context.DeudaProveedores.Include( x => x.IdCompraNavigation)
                    .Where(u => u.IdDeuda == idDeuda)
                    .FirstOrDefaultAsync();
                return deuda;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<DeudaProveedor>> ObtenerDeudaProveedorPorCompra(int idCompra)
        {
            try
            {
                var deuda = await _context.DeudaProveedores.Include(x => x.IdCompraNavigation)
                    .Where(u => u.IdCompra == idCompra)
                    .ToListAsync();
                return deuda;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AgregarDeudaProveedor(DeudaProveedor deuda, int idUsuario)
        {
            try
            {
                deuda.IdUsuarioInserto = idUsuario;
                _context.DeudaProveedores.Add(deuda);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateDeudaProveedor(DeudaProveedorDto dto, int idUsuario)
        {

            var deuda = await _context.DeudaProveedores
                .Where(mm => mm.IdDeuda == dto.IdDeuda)
                .FirstOrDefaultAsync();

            if (deuda == null)
            {
                throw new Exception($"No se puede encontrar la deuda {dto.IdDeuda}");
            }
            else
            {
                deuda.IdCompra = dto.IdCompra;
                deuda.FechaPago = dto.FechaPago;
                deuda.MontoPagado = dto.MontoPagado;
                deuda.MetodoPago = dto.MetodoPago;
                deuda.IdUsuarioModifico = idUsuario;
                deuda.FechaModificado = DateTime.Now;
            }
            await _context.SaveChangesAsync();
        }
        public async Task BorrarDeudaProveedor(int idDeuda, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idDeuda),
                        new SqlParameter("@tabla", "DeudaProveedor"));

                    var deuda = await _context.DeudaProveedores.Where(r => r.IdDeuda == idDeuda)
                        .SingleOrDefaultAsync();

                    if (deuda == null)
                    {
                        throw new Exception("No existe el tratamiento!");
                    }

                    deuda.Borrado = true;
                    deuda.IdUsuarioModifico = idUsuario;
                    deuda.FechaBorrado = DateTime.Now;
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
