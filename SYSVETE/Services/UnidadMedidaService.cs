using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
namespace SYSVETE.Services
{
    public interface IUnidadMedidaService
    {
        Task<List<UnidadMedida>> ObtenerUnidadMedida();
        Task<UnidadMedida> ObtenerUnidadMedidaPorId(int idTipoInsumo);
        Task AgregarUnidadMedida(UnidadMedida tipoInsumo);
        Task UpdateUnidadMedida(UnidadMedida tipoInsumo);
        Task BorrarUnidadMedida(int idTipoInsumo);
    }

    public class UnidadMedidaService : IUnidadMedidaService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public UnidadMedidaService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }

        public async Task<List<UnidadMedida>> ObtenerUnidadMedida()
        {
            try
            {
                var unidadMedida = await _context.UnidadMedidas
                    .ToListAsync();
                return unidadMedida;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UnidadMedida> ObtenerUnidadMedidaPorId(int idUnidadMedida)
        {
            try
            {
                var unidadMedida = await _context.UnidadMedidas
                    .Where(u => u.IdUnidad == idUnidadMedida)
                    .FirstOrDefaultAsync();
                return unidadMedida;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AgregarUnidadMedida(UnidadMedida unidadMedida)
        {
            try
            {
                _context.UnidadMedidas.Add(unidadMedida);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateUnidadMedida(UnidadMedida unidadMedida)
        {

            var existeUnidadMedida = await _context.UnidadMedidas
                .Where(mm => mm.IdUnidad == unidadMedida.IdUnidad)
                .FirstOrDefaultAsync();

            if (existeUnidadMedida == null)
            {
                throw new Exception($"No se puede encontrar el modulo {unidadMedida.IdUnidad}");
            }
            var entry = _context.Entry(existeUnidadMedida);
            entry.CurrentValues.SetValues(unidadMedida);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// R E V I S AR !!!!!!!!!
        /// </summary>
        /// <param name="idTipoInsumo"></param>
        /// <returns></returns>
        public async Task BorrarUnidadMedida(int idTipoInsumo)
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