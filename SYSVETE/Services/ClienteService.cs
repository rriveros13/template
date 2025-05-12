using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;

namespace SYSVETE.Services
{

    public interface IClienteService
    {
        Task<List<Cliente>> ObtenerClientes();
        Task<Cliente> ObtenerClientePorId(int idCliente);
        Task AgregarCliente(ClienteDto clienteDto, int idUsuario);
        Task UpdateCliente(ClienteDto clienteDto,int  idUsuario);
        Task BorrarCliente(int cliente);
    }
    public class ClienteService : IClienteService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public ClienteService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<Cliente> ObtenerClientePorId(int idCliente)
        {
            try
            {
                var cliente = await _context.Clientes.Include( t => t.IdPersonaNavigation )
                    .Where(u => u.IdCliente == idCliente && !u.Borrado)
                    .FirstOrDefaultAsync();
                return cliente;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<Cliente>> ObtenerClientes()
        {
            try
            {
                var cliente = await _context.Clientes.Include(t => t.IdPersonaNavigation)
                 .Where(u => !u.Borrado)
                    .ToListAsync();
                return cliente;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task AgregarCliente(ClienteDto cliente, int idUsuario)
        {
            try
            {
                Cliente nuevoCliente = new Cliente()
                {
                    IdPersona = cliente.IdPersona,
                    RUC = cliente.RUC,
                    Telefono = cliente.Telefono,
                    Email = cliente.Email,
                    Activo = cliente.Activo,
                    IdUsuarioInserto = idUsuario

                };
                _context.Clientes.Add(nuevoCliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateCliente( ClienteDto dto, int idUsuario)
        {

            var cliente = await _context.Clientes
                .Where(mm => mm.IdCliente == dto.IdCliente)
                .FirstOrDefaultAsync();

            if (cliente == null)
            {
                throw new Exception($"No se puede encontrar el dto {dto.IdCliente}");
            }
            else
            {
                cliente.IdPersona = dto.IdPersona;
                cliente.RUC = dto.RUC;
                cliente.Telefono = dto.Telefono;
                cliente.Email = dto.Email;
                cliente.FechaModificado = DateTime.Now;
                cliente.IdUsuarioModifico = idUsuario;
            }

            await _context.SaveChangesAsync();
        }

        public async Task BorrarCliente(int idCliente)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idCliente),
                        new SqlParameter("@tabla", "Cliente"));

                    var cliente = await _context.Clientes.Where(r => r.IdCliente == idCliente)
                        .SingleOrDefaultAsync();

                    if (cliente == null)
                    {
                        throw new Exception("No existe el dto!");
                    }

                    cliente.Borrado = true;
                    cliente.FechaBorrado = DateTime.Now;
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
