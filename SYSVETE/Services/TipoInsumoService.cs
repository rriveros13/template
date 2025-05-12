using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
namespace SYSVETE.Services
{
    public interface ITipoInsumoService
    {
        Task<List<TipoInsumo>> ObtenerTipoInsumos();
        Task<TipoInsumo> ObtenerTipoInsumoPorId(int idTipoInsumo);
        Task AgregarTipoInsumo(TipoInsumo tipoInsumo, int idUsuario);
        Task UpdateTipoInusmo(TipoInsumo tipoInsumo, int idUsuario);
        Task BorrarTipoInsumo(int idTipoInsumo, int idUsuario);
    }
    public class TipoInsumoService: ITipoInsumoService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public TipoInsumoService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<TipoInsumo> ObtenerTipoInsumoPorId(int idTipoInsumo)
        {
            try
            {
                var tipoInsumo = await _context.TipoInsumos
                    .Where(u => u.IdTipoInsumo == idTipoInsumo)
                    .FirstOrDefaultAsync();
                return tipoInsumo;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<TipoInsumo>> ObtenerTipoInsumos()
        {
            try
            {
                var tipoInsumos = await _context.TipoInsumos
                    .ToListAsync();
                return tipoInsumos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task AgregarTipoInsumo(TipoInsumo tipoInsumo, int idUsuario)
        {
            try
            {
                tipoInsumo.IdUsuarioInserto = idUsuario;
                _context.TipoInsumos.Add(tipoInsumo);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateTipoInusmo(TipoInsumo tipoInsumo, int idUsuario)
        {

            var existeTipoInsumo = await _context.TipoInsumos
                .Where(mm => mm.IdTipoInsumo == tipoInsumo.IdTipoInsumo)
                .FirstOrDefaultAsync();

            if (existeTipoInsumo == null)
            {
                throw new Exception($"No se puede encontrar el modulo {tipoInsumo.IdTipoInsumo}");
            }
            else
            {
                existeTipoInsumo.Descripcion = tipoInsumo.Descripcion;
                existeTipoInsumo.IdUsuarioModifico = idUsuario;
                existeTipoInsumo.FechaModificado = DateTime.Now;
            }
            

            await _context.SaveChangesAsync();
        }

        public async Task BorrarTipoInsumo(int idTipoInsumo, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idTipoInsumo),
                        new SqlParameter("@tabla", "TipoInsumo"));

                    var rol = await _context.TipoInsumos.Where(r => r.IdTipoInsumo == idTipoInsumo)
                        .SingleOrDefaultAsync();

                    if (rol == null)
                    {
                        throw new Exception("No existe el tipo de insumo!");
                    }

                    rol.Borrado = true;
                    rol.IdUsuarioModifico = idUsuario;
                    rol.FechaBorrado = DateTime.Now;
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
