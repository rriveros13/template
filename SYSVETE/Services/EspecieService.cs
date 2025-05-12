using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IEspecieService
    {
        Task<List<Especie>> ObtenerEspecie();
        Task<Especie> ObtenerEspeciePorId(int idEspecie);
        Task AgregarEspecie(Especie especie, int idUsuario);
        Task UpdateEspecie(EspecieDto especie, int idUsuario);
        Task BorrarEspecie(int idEspecie);
    }
    public class EspecieService : IEspecieService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public EspecieService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }

        public async Task<List<Especie>> ObtenerEspecie()
        {
            try
            {
                var especie = await _context.Especies
                    .ToListAsync();
                return especie;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Especie> ObtenerEspeciePorId(int idEspecie)
        {
            try
            {
                var especie = await _context.Especies
                    .Where(u => u.IdEspecie == idEspecie)
                    .FirstOrDefaultAsync();
                return especie;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AgregarEspecie(Especie especie, int idUsuario)
        {
            try
            {
                especie.IdUsuarioInserto = idUsuario;
                _context.Especies.Add(especie);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateEspecie(EspecieDto dto, int idUsuario)
        {
            try
            {
                var especie = await _context.Especies
                    .Where(mm => mm.IdEspecie == dto.IdEspecie)
                    .FirstOrDefaultAsync();

                if (especie == null)
                {
                    throw new Exception($"No se puede encontrar la especie {especie.IdEspecie}");
                }
                else
                {
                    especie.IdEspecie = dto.IdEspecie;
                    especie.Nombre = dto.Nombre;
                    especie.IdUsuarioModifico = idUsuario;
                    especie.FechaModificado = DateTime.Now;
                }
                                 
                await _context.SaveChangesAsync();
            }
            catch ( Exception e)
            {
                throw e;
            }
           
        }
        public async Task BorrarEspecie(int idEspecie)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idEspecie),
                        new SqlParameter("@tabla", "Especie"));

                    var especie = await _context.Especies.Where(r => r.IdEspecie == idEspecie)
                        .SingleOrDefaultAsync();

                    if (especie == null)
                    {
                        throw new Exception("No existe la especie!");
                    }

                    especie.Borrado = true;
                    especie.FechaBorrado = DateTime.Now;
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
