using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SYSVETE.Services
{

    public interface IInsumoService
    {
        Task<List<Insumo>> ObtenerInsumos();
        Task<Insumo> ObtenerInsumoPorId(int idInsumo);
        Task AgregarInsumo(InsumoDto insumo, int idUsuario);
        Task UpdateInusmo(Insumo insumo, int idUsuario);
        Task BorrarInsumo(int insumo);
        Task<int> LlamarProcedimientoAlmacenado(int parametro1, DateTime parametro2);

    }
    public class InsumoService: IInsumoService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public InsumoService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<Insumo> ObtenerInsumoPorId(int idInsumo)
        {
            try
            {
                var insumo = await _context.Insumos.Include( t => t.IdTipoInsumoNavigation)
                    .Include(i => i.IdImpuestoNavigation)
                    .Where(u => u.IdInsumo == idInsumo)
                    .FirstOrDefaultAsync();
                return insumo;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<Insumo>> ObtenerInsumos()
        {
            try
            {
                var insumo = await _context.Insumos.Include(t => t.IdTipoInsumoNavigation)
                    .Include(i => i.IdImpuestoNavigation)
                    .ToListAsync();
                return insumo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task AgregarInsumo(InsumoDto insumo, int idUsuario)
        {
            try
            {
                Insumo nuevoInsumo = new Insumo()
                {
                    IdTipoInsumo = insumo.IdTipoInsumo,
                    IdPresentacion = insumo.idPresentacion,
                    IdImpuesto = insumo.idImpuesto,
                    codigo = insumo.codigo,
                    descripcion = insumo.descripcion,
                    Activo = insumo.Activo,
                    IdUsuarioInserto = idUsuario

                };
                _context.Insumos.Add(nuevoInsumo);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateInusmo(Insumo dto, int idUsuario)
        {

            var insumo = await _context.Insumos
                .Where(mm => mm.IdInsumo == dto.IdInsumo)
                .FirstOrDefaultAsync();

            if (insumo == null)
            {
                throw new Exception($"No se puede encontrar el modulo {dto.IdInsumo}");
            }
            else
            {
                insumo.IdTipoInsumo = dto.IdTipoInsumo;
                insumo.IdPresentacion = dto.IdPresentacion;
                insumo.IdImpuesto = dto.IdImpuesto;
                insumo.codigo = dto.codigo;
                insumo.descripcion = dto.descripcion;
                insumo.Activo = dto.Activo;
                insumo.IdUsuarioModifico = idUsuario;
                insumo.FechaModificado = DateTime.Now;
            }
           

            await _context.SaveChangesAsync();
        }
        public async Task BorrarInsumo(int idInsumo)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idInsumo),
                        new SqlParameter("@tabla", "InsumoDto"));

                    var rol = await _context.Insumos.Where(r => r.IdInsumo == idInsumo)
                        .SingleOrDefaultAsync();

                    if (rol == null)
                    {
                        throw new Exception("No existe el tipo de insumo!");
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
        public async Task<int> LlamarProcedimientoAlmacenado(int idInsumo, DateTime fecha)
        {
            try
            {
                var parametro1 = new SqlParameter("@Parametro1", idInsumo);
                var parametro2 = new SqlParameter("@Parametro2", fecha);
                var resultados = await _context.Database.ExecuteSqlRawAsync("exec dbo.ObtenerTotalInsumo @id, @fecha",
                        new SqlParameter("@id", idInsumo),
                        new SqlParameter("@fecha", fecha));

                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
