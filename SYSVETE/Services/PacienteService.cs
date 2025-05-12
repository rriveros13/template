using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;

namespace SYSVETE.Services
{

    public interface IPacienteService
    {
        Task<List<Paciente>> ObtenerPacientes();
        Task<Paciente> ObtenerPacientePorId(int idPaciente);
        Task AgregarPaciente(Paciente paciente, int idUsuario);
        Task UpdatePaciente(PacienteDto pacienteDto, int idUsuario);
        Task BorrarPAciente(int idPaciente);
    }
    public class PacienteService : IPacienteService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public PacienteService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<Paciente> ObtenerPacientePorId(int idPaciente)
        {
            try
            {
                var paciente = await _context.Pacientes.Include(t => t.IdClienteNavigation)
                    .Include(e => e.IdRazaNavigation)
                     .Include(r => r.IdClienteNavigation).ThenInclude(o => o.IdPersonaNavigation)
                    .Where(u => u.IdPaciente == idPaciente && !u.Borrado)
                    .FirstOrDefaultAsync();
                return paciente;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<Paciente>> ObtenerPacientes()    
        {
            try
            {
                var paciente = await _context.Pacientes.Include(t => t.IdClienteNavigation)
                  .Include(e => e.IdRazaNavigation)
                  .Include( r => r.IdClienteNavigation).ThenInclude( o => o.IdPersonaNavigation)
                 .Where(u => !u.Borrado)
                    .ToListAsync();
                return paciente;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task AgregarPaciente(Paciente paciente,int idUsuario)
        {
            try
            {
                paciente.IdUsuarioInserto = idUsuario;
                _context.Pacientes.Add(paciente);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdatePaciente(PacienteDto dto, int idUsuario)
        {
            try
            {
                var paciente = await _context.Pacientes
                                .Where(mm => mm.IdPaciente == dto.IdPaciente)
                                .FirstOrDefaultAsync();

                if (paciente == null)
                {
                    throw new Exception($"No se puede encontrar el paciente {dto.IdPaciente}");
                }
                else
                {
                    paciente.IdCliente = dto.IdCliente;
                    paciente.IdRaza = dto.IdRaza;
                    paciente.Nombre = dto.Nombre;
                    paciente.Edad = dto.Edad;
                    paciente.Sexo = dto.Sexo.Trim();
                    paciente.Peso = dto.Peso;
                    paciente.Activo = dto.Activo;
                    paciente.IdUsuarioModifico = idUsuario;
                    paciente.FechaModificado = DateTime.Now;
                    await _context.SaveChangesAsync();


                }
            }
            catch (Exception e)
            {
                throw e;
            }

            
        }
        public async Task BorrarPAciente(int idPaciente)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idPaciente),
                        new SqlParameter("@tabla", "Paciente"));

                    var paciente = await _context.Pacientes.Where(r => r.IdPaciente == idPaciente)
                        .SingleOrDefaultAsync();

                    if (paciente == null)
                    {
                        throw new Exception("No existe el paciente!");
                    }

                    paciente.Borrado = true;
                    paciente.FechaBorrado = DateTime.Now;
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
