using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IProcedimientoService
    {
        Task<List<Procedimiento>> ObtenerProcedimiento();
        Task<Procedimiento> ObtenerProcedimientoPorId(int idProcedimiento);
        Task AgregarProcedimiento(ProcedimientoDto procedimientoDto, int idUsuario);
        Task UpdateProcedimiento(ProcedimientoDto procedimientoDto, int idUsuario);
        Task BorrarProcedimiento(int idPatologia, int idUsuario);
    }
    public class ProcedimientoService : IProcedimientoService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public ProcedimientoService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<Procedimiento>> ObtenerProcedimiento()
        {
            try
            {
                var procedimientos = await _context.Procedimientos
                    .ToListAsync();
                return procedimientos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Procedimiento> ObtenerProcedimientoPorId(int idProcedimiento)
        {
            try
            {
                var procedimiento = await _context.Procedimientos
                    .Where(u => u.IdProcedimiento == idProcedimiento)
                    .FirstOrDefaultAsync();
                return procedimiento;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AgregarProcedimiento(ProcedimientoDto procedimientoDto, int idUsuario)
        {
            try
            {

                Procedimiento nuevoProcedimiento = new Procedimiento()
                {
                    Descripcion = procedimientoDto.Descripcion,
                    Costo = procedimientoDto.Costo,
                   IdUsuarioInserto = idUsuario
                };
                _context.Procedimientos.Add(nuevoProcedimiento);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateProcedimiento(ProcedimientoDto dto, int idUsuario)
        {

            var procedimiento = await _context.Procedimientos
                .Where(mm => mm.IdProcedimiento == dto.IdProcedimiento)
                .FirstOrDefaultAsync();

            if (procedimiento == null)
            {
                throw new Exception($"No se puede encontrar el procedimiento {dto.IdProcedimiento}");
            }
            else
            {
                procedimiento.Descripcion = dto.Descripcion;
                procedimiento.IdUsuarioInserto = idUsuario;
                procedimiento.FechaModificado = DateTime.Now;
                procedimiento.Costo = dto.Costo;
            }
          

            await _context.SaveChangesAsync();
        }
        public async Task BorrarProcedimiento(int idProcedimiento, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idProcedimiento),
                        new SqlParameter("@tabla", "Procedimiento"));

                    var procedimiento = await _context.Procedimientos.Where(r => r.IdProcedimiento == idProcedimiento)
                        .SingleOrDefaultAsync();

                    if (procedimiento == null)
                    {
                        throw new Exception("No existe el procedimiento!");
                    }

                    procedimiento.Borrado = true;
                    procedimiento.IdUsuarioModifico = idUsuario;
                    procedimiento.FechaBorrado = DateTime.Now;
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
