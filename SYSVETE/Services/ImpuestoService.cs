using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IImpuestoService
    {
        Task<List<Impuesto>> ObtenerImpuesto();
        Task<Impuesto> ObtenerImpuestoPorId(int idImpuesto);
        Task AgregarImpuesto(Impuesto impuesto, int idUsuario);
        Task UpdateImpuesto(ImpuestoDtocs impuesto, int idUsuario);
        Task BorrarImpuesto(int idImpuesto);
    }
    public class ImpuestoService : IImpuestoService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public ImpuestoService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<Impuesto>> ObtenerImpuesto()
        {
            try
            {
                var impuesto = await _context.Impuestos
                    .ToListAsync();
                return impuesto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Impuesto> ObtenerImpuestoPorId(int idImpuesto)
        {
            try
            {
                var impuestos = await _context.Impuestos
                    .Where(u => u.idImpuesto == idImpuesto)
                    .FirstOrDefaultAsync();
                return impuestos;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AgregarImpuesto(Impuesto impuesto, int idUsuario)
        {
            try
            {
                impuesto.IdUsuarioInserto = idUsuario;
                _context.Impuestos.Add(impuesto);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateImpuesto(ImpuestoDtocs dto, int idUsuario)
        {
            try
            {
                var impuesto = await _context.Impuestos
              .Where(mm => mm.idImpuesto == dto.idImpuesto)
              .FirstOrDefaultAsync();
                   
                if (impuesto == null)
                {
                    throw new Exception($"No se puede encontrar el impuesto {dto.idImpuesto}");
                }
                else
                {
                    impuesto.idImpuesto = dto.idImpuesto;
                    impuesto.Descripcion = dto.Descripcion;
                    impuesto.Valor = dto.Valor;
                    impuesto.FechaModificado = DateTime.Now;
                    impuesto.IdUsuarioModifico = idUsuario;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task BorrarImpuesto(int idImpuesto)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idImpuesto),
                        new SqlParameter("@tabla", "Impuesto"));

                    var rol = await _context.Impuestos.Where(r => r.idImpuesto == idImpuesto)
                        .SingleOrDefaultAsync();

                    if (rol == null)
                    {
                        throw new Exception("No existe el impuesto!");
                    }

                    rol.Borrado = true;
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
