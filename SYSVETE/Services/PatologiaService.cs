using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IPatologiaService
    {
        Task<List<Patologia>> ObtenerPatologia();
        Task<Patologia> ObtenerPatologiaPorId(int idPatologia);
        Task AgregarPatologia(PatologiaDto patologiaDto, int idUsuario);
        Task UpdatePatologia(PatologiaDto patologia, int idUsuario);
        Task BorrarPatologia(int idPatologia);
    }
    public class PatologiaService : IPatologiaService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public PatologiaService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<Patologia>> ObtenerPatologia()
        {
            try
            {
                var patologias = await _context.Patologias
                    .ToListAsync();
                return patologias;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Patologia> ObtenerPatologiaPorId(int idPatologia)
        {
            try
            {
                var tratamientos = await _context.Patologias
                    .Where(u => u.IdPatologia == idPatologia)
                    .FirstOrDefaultAsync();
                return tratamientos;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AgregarPatologia(PatologiaDto patologiaDto, int idUsuario)
        {
            try
            {

                Patologia nuevaPatologia = new Patologia()
                {
                   Nombre = patologiaDto.Nombre,
                   IdUsuarioInserto = idUsuario
                };
                _context.Patologias.Add(nuevaPatologia);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdatePatologia(PatologiaDto dto, int idUsuario)
        {

            var patologia = await _context.Patologias
                .Where(mm => mm.IdPatologia == dto.IdPatologia)
                .FirstOrDefaultAsync();

            if (patologia == null)
            {
                throw new Exception($"No se puede encontrar la Patologia {dto.IdPatologia}");
            }
            else
            {
                patologia.Nombre = dto.Nombre;
                patologia.IdUsuarioModifico = idUsuario;
                patologia.FechaModificado = DateTime.Now;
            }
           
            await _context.SaveChangesAsync();
        }
        public async Task BorrarPatologia(int idPatologia)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idPatologia),
                        new SqlParameter("@tabla", "Patologia"));

                    var patologia = await _context.Patologias.Where(r => r.IdPatologia == idPatologia)
                        .SingleOrDefaultAsync();

                    if (patologia == null)
                    {
                        throw new Exception("No existe la patologia!");
                    }

                    patologia.Borrado = true;
                    patologia.FechaBorrado = DateTime.Now;

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
