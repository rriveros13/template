using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IProveedorService
    {
        Task<List<ProveedorDto>> ObtenerProveedores();
        Task<ProveedorDto> ObtenerProveedorPorId(int idProveedor);
        Task AgregarProveedor(ProveedorDto model, int idUsuario);
        Task UpdateProveedor(ProveedorDto model, int idUsuario);
        Task BorrarProveedor(int idProveedor, int idUsuario);

    }
    public class ProveedorService : IProveedorService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public ProveedorService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }

        public async Task AgregarProveedor(ProveedorDto model, int idUsuario)
        {
            try
            {
                Proveedor data = new()
                {
                    IdProveedor = model.IdProveedor,
                    IdPersona = model.IdPersona,
                    Ruc = model.Ruc,
                    Telefono = string.IsNullOrEmpty(model.Telefono) ? null : model.Telefono,
                    Email = string.IsNullOrEmpty(model.Email) ? null : model.Email,
                    IdUsuarioInserto = idUsuario
                };

                _context.Add(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task BorrarProveedor(int idProveedor, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idProveedor),
                        new SqlParameter("@tabla", "Proveedor"));

                    var data = await _context.Proveedores.Where(r => r.IdProveedor == idProveedor)
                        .SingleOrDefaultAsync();

                    if (data == null)
                    {
                        throw new Exception("No existen datos!");
                    }

                    data.Borrado = true;
                    data.FechaBorrado = DateTime.Now;
                    data.IdUsuarioModifico =idUsuario;

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

        public async Task<List<ProveedorDto>> ObtenerProveedores()
        {
            try
            {
                var data = await _context.Proveedores
                    .Include(p => p.IdPersonaNavigation)
                    .Select(p => new ProveedorDto()
                    {
                        IdProveedor = p.IdProveedor,
                        IdPersona = p.IdPersona,
                        Ruc = p.Ruc,
                        Telefono = p.Telefono,
                        Email = p.Email,
                        Persona = new PersonaDto() 
                        { 
                            Nombre = p.IdPersonaNavigation.Nombre
                        }
                    })
                    .ToListAsync();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProveedorDto> ObtenerProveedorPorId(int idProveedor)
        {
            try
            {
                var data = await _context.Proveedores.Where(p => p.IdProveedor == idProveedor)
                    .Include(p => p.IdPersonaNavigation)
                    .Select(p => new ProveedorDto()
                    {
                        IdProveedor = p.IdProveedor,
                        IdPersona = p.IdPersona,
                        Ruc = p.Ruc,
                        Telefono = p.Telefono,
                        Email = p.Email,
                    })
                    .SingleOrDefaultAsync();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateProveedor(ProveedorDto model, int idUsuario)
        {
            try
            {
                var proveedor = await _context.Proveedores
                    .Where(p => p.IdProveedor == model.IdProveedor)
                    .FirstOrDefaultAsync();

                if (proveedor != null)
                {
                    proveedor.Ruc = model.Ruc;
                    proveedor.Telefono = model.Telefono;
                    proveedor.Email = model.Email;
                    proveedor.FechaModificado = DateTime.Now;
                    proveedor.IdUsuarioModifico = idUsuario;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
