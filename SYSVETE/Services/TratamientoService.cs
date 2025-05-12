using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface ITratamientoService
    {
        Task<List<Tratamiento>> ObtenerTratamiento();
        Task<Tratamiento> ObtenerTratamientoPorId(int idTratamiento);
        Task AgregarTratamiento(Tratamiento tratamiento, int idUsuario);
        Task UpdateTratamiento(TratamientoDto tratamiento, int idUsuario);
        Task BorrarTratamiento(int idTratamiento, int idUsuario);
    }
    public class TratamientoService : ITratamientoService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public TratamientoService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<Tratamiento>> ObtenerTratamiento()
        {
            try
            {
                var tratamientos = await _context.Tratamientos
                    .ToListAsync();
                return tratamientos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Tratamiento> ObtenerTratamientoPorId(int idTratamiento)
        {
            try
            {
                var tratamientos = await _context.Tratamientos
                    .Where(u => u.IdTratamiento == idTratamiento)
                    .FirstOrDefaultAsync();
                return tratamientos;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AgregarTratamiento(Tratamiento tratamiento, int idUsuario)
        {
            try
            {
                tratamiento.IdUsuarioInserto = idUsuario;
                _context.Tratamientos.Add(tratamiento);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateTratamiento(TratamientoDto dto, int idUsuario)
        {

            var tratamiento = await _context.Tratamientos
                .Where(mm => mm.IdTratamiento == dto.IdTratamiento)
                .FirstOrDefaultAsync();

            if (tratamiento == null)
            {
                throw new Exception($"No se puede encontrar el tratamiento {dto.IdTratamiento}");
            }
            else
            {
                tratamiento.Nombre = dto.Nombre;
                tratamiento.Costo = dto.Costo;
                tratamiento.IdUsuarioModifico = idUsuario;
                tratamiento.FechaModificado = DateTime.Now;
            }
            await _context.SaveChangesAsync();
        }
        public async Task BorrarTratamiento(int idTratamiento, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idTratamiento),
                        new SqlParameter("@tabla", "Tratamiento"));

                    var tratamiento = await _context.Tratamientos.Where(r => r.IdTratamiento == idTratamiento)
                        .SingleOrDefaultAsync();

                    if (tratamiento == null)
                    {
                        throw new Exception("No existe el tratamiento!");
                    }

                    tratamiento.Borrado = true;
                    tratamiento.IdUsuarioModifico = idUsuario;
                    tratamiento.FechaBorrado = DateTime.Now;
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
