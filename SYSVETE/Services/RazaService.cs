using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IRazaService
    {
        Task<List<Raza>> ObtenerRazas();
        Task<Raza> ObtenerRazaPorId(int idRaza);
        Task AgregarRaza(RazaDto raza, int idUsuario);
        Task UpdateRaza(RazaDto raza, int idUsuario);
        Task BorrarRaza(int idRaza, int idUsuario);
    }
    public class RazaService : IRazaService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public RazaService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<Raza>> ObtenerRazas()
        {
            try
            {
                var raza = await _context.Razas.Include(u => u.IdEspecieNavigation)
                    .ToListAsync();
                return raza;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Raza> ObtenerRazaPorId(int idRaza)
        {
            try
            {
                var razas = await _context.Razas
                    .Where(u => u.IdRaza == idRaza)
                    .FirstOrDefaultAsync();
                return razas;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AgregarRaza(RazaDto raza, int idUsuario)
        {
            try
            {
                Raza nuevaRaza = new Raza()
                {
                    IdEspecie = raza.IdEspecie,                 
                    Nombre = raza.Nombre,
                    Activo = raza.Activo,
                    IdUsuarioInserto = idUsuario

                };
                _context.Razas.Add(nuevaRaza);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateRaza(RazaDto dto, int idUsuario)
        {

            var raza = await _context.Razas
                .Where(mm => mm.IdRaza == dto.IdRaza)
                .FirstOrDefaultAsync();

            if (raza == null)
            {
                throw new Exception($"No se puede encontrar la raza {dto.IdRaza}");
            }
            else
            {
                raza.IdEspecie = dto.IdEspecie;
                raza.Nombre = dto.Nombre;
                raza.Activo = dto.Activo;
                raza.IdUsuarioModifico = idUsuario;
                raza.FechaModificado = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }
        public async Task BorrarRaza(int idRaza, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idRaza),
                        new SqlParameter("@tabla", "Raza"));

                    var raza = await _context.Razas.Where(r => r.IdRaza == idRaza)
                        .SingleOrDefaultAsync();

                    if (raza == null)
                    {
                        throw new Exception("No existe la raza!");
                    }

                    raza.Borrado = true;
                    raza.IdUsuarioModifico = idUsuario;
                    raza.FechaBorrado = DateTime.Now;
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
