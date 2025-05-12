using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;

namespace SYSVETE.Services
{
    public interface IModulo
    {
        Task<List<Modulo>> ObtenerModulos();
        Task<Modulo> ObtenerModuloPorID(int idUsuario);
        Task AgregarModulo(Modulo Modulo);
        Task UpdateModulo(Modulo Modulo);
        //Task DeleteModulo(int idModulo);


    }
    public class ModuloService : IModulo
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public ModuloService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<Modulo> ObtenerModuloPorID(int idModulo)
        {
            try
            {
                var modulo = await _context.Modulos
                    .Where(u => u.IdModulo == idModulo)
                    .FirstOrDefaultAsync();
                return modulo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Modulo>> ObtenerModulos()
        {
            try
            {
                var modulos = await _context.Modulos
                    .ToListAsync();
                return modulos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task AgregarModulo(Modulo modulo)
        {
            try
            {
                _context.Modulos.Add(modulo);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                string message = e.Message;
                throw;

            }

        }
        public async Task UpdateModulo(Modulo modulo)
        {

            var existeModulo = await _context.Modulos
                .Where(mm => mm.IdModulo == modulo.IdModulo)
                .FirstOrDefaultAsync();

            if (existeModulo == null)
            {
                throw new Exception($"No se puede encontrar el modulo {modulo.IdModulo}");
            }
            var entry = _context.Entry(existeModulo);
            entry.CurrentValues.SetValues(modulo);

            await _context.SaveChangesAsync();
        }

        //public async Task DeleteModulo(int modulo)
        //{
        //    _context.Modulos.Add(modulo);
        //    await _context.SaveChangesAsync();
        //}
    }
}
