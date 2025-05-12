using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IPersona
    {
        Task<List<PersonaDto>> ObtenerPersonas();
        Task<PersonaDto> ObtenerPersonaPorId(int idUsuario);
        Task AgregarPersona(PersonaDto Persona, int idUsuario);
        Task EditarPersona(PersonaDto Persona);

    }
    public class PersonaService : IPersona
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public PersonaService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<PersonaDto> ObtenerPersonaPorId(int idPersona)
        {
            try
            {
                var persona = await _context.Personas
                    .Where(u => u.IdPersona == idPersona)
                    .Select(p => new PersonaDto()
                    {
                        IdPersona = p.IdPersona,
                        Nombre = p.Nombre,
                        Apellido = p.Apellido,
                        Cedula = p.Cedula,
                        FechaNacimiento = p.FechaNacimiento,
                    })
                    .FirstOrDefaultAsync();

                return persona;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<PersonaDto>> ObtenerPersonas()
        {
            try
            {
                var personas = await _context.Personas
                    .Select(p => new PersonaDto()
                    {
                        IdPersona = p.IdPersona,
                        Nombre = p.Nombre,
                        Apellido = p.Apellido,
                        Cedula = p.Cedula,
                        FechaNacimiento = p.FechaNacimiento,
                    })
                    .ToListAsync();
                return personas;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task AgregarPersona(PersonaDto persona, int idUsuario)
        {

            Persona data = new Persona()
            {
                IdPersona = persona.IdPersona,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Cedula = persona.Cedula,
                FechaNacimiento = persona.FechaNacimiento,
                IdUsuarioInserto = idUsuario
            };
            _context.Personas.Add(data);
            await _context.SaveChangesAsync();
        }
        public async Task EditarPersona(PersonaDto dto)
        {
            try
            {
                var persona = await _context.Personas
                   .Where(p => p.IdPersona == dto.IdPersona)
                   .SingleOrDefaultAsync();

                if (persona != null)
                {
                    persona.Nombre = dto.Nombre;
                    persona.Apellido = dto.Apellido;
                    persona.Cedula = dto.Cedula;
                    persona.FechaNacimiento = dto.FechaNacimiento;
                    persona.FechaModificado = DateTime.Now;
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
