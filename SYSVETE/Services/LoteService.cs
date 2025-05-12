using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface ILoteService
    {
        Task<List<Lote>> ObtenerLote();
        Task<Lote> ObtenerLotePorId(int idLote);
        Task AgregarLote(LoteDto loteDto, int idUsuario);
        Task UpdateLote(LoteDto lote, int idUsuario);
        Task BorrarLote(int idLote);
    }
    public class LoteService : ILoteService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public LoteService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<Lote>> ObtenerLote()
        {
            try
            {
                var lotes = await _context.Lotes
                    .ToListAsync();
                return lotes;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Lote> ObtenerLotePorId(int idLote)
        {
            try
            {
                var lote = await _context.Lotes
                    .Where(u => u.IdLote == idLote)
                    .FirstOrDefaultAsync();
                return lote;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AgregarLote(LoteDto loteDto, int idUsuario)
        {
            try
            {

                Lote nuevoLote = new Lote()
                {
                    CodigoLote = loteDto.CodigoLote,
                    FechaVencimiento = loteDto.FechaVencimiento,
                    FechaFabricacion = loteDto.FechaFabricacion,
                    IdUsuarioInserto = idUsuario
                };
                _context.Lotes.Add(nuevoLote);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateLote(LoteDto dto, int idUsuario)
        {

            var lote = await _context.Lotes
                .Where(mm => mm.IdLote == dto.IdLote)
                .FirstOrDefaultAsync();

            if (lote == null)
            {
                throw new Exception($"No se puede encontrar la dto {dto.IdLote}");
            }
            else
            {
                lote.CodigoLote = dto.CodigoLote;
                lote.FechaVencimiento = dto.FechaVencimiento;
                lote.FechaFabricacion = dto.FechaFabricacion;
                lote.IdUsuarioModifico = idUsuario;
                lote.FechaModificado = DateTime.Now;
            }
           
            await _context.SaveChangesAsync();
        }
        public async Task BorrarLote(int idLote)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idLote),
                        new SqlParameter("@tabla", "Lote"));

                    var lote = await _context.Lotes.Where(r => r.IdLote == idLote)
                        .SingleOrDefaultAsync();

                    if (lote == null)
                    {
                        throw new Exception("No existe el dto!");
                    }

                    lote.Borrado = true;
                    lote.FechaBorrado = DateTime.Now;

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
