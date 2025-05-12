using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IVacunaService
    {
        Task<List<Vacuna>> ObtenerVacuna();
        Task<Vacuna> ObtenerVacunaPorId(int idVacuna);
        Task AgregarVacuna(VacunaDto vacunaDto, int idUsuario);
        Task UpdateVacuna(Vacuna vacuna);
        Task BorrarVacuna(int idVacuna);
    }
    public class VacunaService : IVacunaService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public VacunaService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<Vacuna>> ObtenerVacuna()
        {
            try
            {
                var patologias = await _context.Vacunas
                    .ToListAsync();
                return patologias;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Vacuna> ObtenerVacunaPorId(int idVacuna)
        {
            try
            {
                var tratamientos = await _context.Vacunas
                    .Where(u => u.IdVacuna == idVacuna)
                    .FirstOrDefaultAsync();
                return tratamientos;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AgregarVacuna(VacunaDto vacunaDto, int idUsuario)
        {
            try
            {

                Vacuna nuevaVacuna = new Vacuna()
                {
                    Nombre = vacunaDto.Nombre,
                    IdUsuarioInserto = idUsuario
                };
                _context.Vacunas.Add(nuevaVacuna);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateVacuna(Vacuna vacuna)
        {

            var existeTratamiento = await _context.Vacunas
                .Where(mm => mm.IdVacuna == vacuna.IdVacuna)
                .FirstOrDefaultAsync();

            if (existeTratamiento == null)
            {
                throw new Exception($"No se puede encontrar la vacuna {vacuna.IdVacuna}");
            }
            //var entry = _context.Entry(existeTratamiento);
            //entry.CurrentValues.SetValues(vacuna);

            existeTratamiento.Costo = vacuna.Costo;
            existeTratamiento.Nombre = vacuna.Nombre;
            existeTratamiento.FechaModificado = DateTime.Now;

            await _context.SaveChangesAsync();
        }
        public async Task BorrarVacuna(int idVacuna)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idVacuna),
                        new SqlParameter("@tabla", "Vacuna"));

                    var patologia = await _context.Vacunas.Where(r => r.IdVacuna == idVacuna)
                        .SingleOrDefaultAsync();

                    if (patologia == null)
                    {
                        throw new Exception("No existe la vacuna!");
                    }

                    patologia.Borrado = true;

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
