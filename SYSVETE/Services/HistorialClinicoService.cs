using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;

namespace SYSVETE.Services
{

    public interface IHistorialClinicoService
    {
        Task<List<HistorialClinico>> ObtenerHistorialClinico();
        Task<HistorialClinico> ObtenerHistorialClinicoPorId(int idHistorialClinico);
        Task<List<HistorialClinico>> ObtenerHistorialClinicoPorPaciente(int idPaciente);

        Task AgregarHistorialClinico(HistorialClinico historialClinico, int idUsuario);
        Task FacturarServicios(int idCliente,int idVenta, int idUsuario);

        Task UpdateHistorilaClinico(HistorialClinico historialClinico, int idUsuario);
        Task BorrarHistorialClinico(int idHistorialClinico);
    }
    public class HistorialClinicoService : IHistorialClinicoService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public HistorialClinicoService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }



        public async Task FacturarServicios(int idCliente, int idVenta, int idUsuario)
        {
            var pacientes = await _context.Pacientes.Include(t => t.IdClienteNavigation)
                     .Include(e => e.IdRazaNavigation)
                     .Where(u => u.IdCliente == idCliente)
                     .ToListAsync();

            foreach (Paciente paciente in pacientes)
            {
                var historialesNoFacturados = await _context.HistorialClinicoS.Include(t => t.IdVacunaNavigation)
                   .Include(e => e.IdProcedimientoNavigation)
                  .Include(x => x.IdTratamientoNavigation)
                  .Include(j => j.IdPatologiaNavigation)
                  .Include(z => z.IdPacienteNavigation)
                   .Where(u => u.IdPaciente == paciente.IdPaciente && !u.Facturado)
                    .ToListAsync();
                foreach (HistorialClinico hs in historialesNoFacturados)
                {
                    bool bandera = false;

                    //// insertar un detalle por cada item
                    if (hs.IdProcedimiento != null)
                    {
                        VentaDetalle nuevDetalle = new VentaDetalle()
                        {
                            IdHistorial = hs.IdHistorial,
                            IdVenta = idVenta,
                            Descripcion = "Procedimiento - " + hs.IdProcedimientoNavigation.Descripcion,
                            Cantidad = 1,
                            Precio = hs.IdProcedimientoNavigation.Costo,
                            IdUsuarioInserto = idUsuario,
                            IdInsumo = null,
                            FechaInsertado = DateTime.Now
                        };
                        _context.VentaDetalles.Add(nuevDetalle);
                        await _context.SaveChangesAsync();

                        // actualizamos el estado de facturado en historialclinico
                        var updt = await _context.HistorialClinicoS.Include(t => t.IdVacunaNavigation)
                           .Include(e => e.IdProcedimientoNavigation)
                          .Include(x => x.IdTratamientoNavigation)
                          .Include(j => j.IdPatologiaNavigation)
                          .Include(z => z.IdPacienteNavigation)
                           .Where(u => u.IdHistorial == hs.IdHistorial && !u.Borrado)
                            .FirstOrDefaultAsync();

                        updt.Facturado = true;
                        await _context.SaveChangesAsync();

                    }
                    if (hs.IdVacuna != null)
                    {
                        VentaDetalle nuevDetalle = new VentaDetalle()
                        {
                            IdHistorial = hs.IdHistorial,
                            IdVenta = idVenta,
                            Descripcion = "Vacuna - " + hs.IdVacunaNavigation.Nombre,
                            Cantidad = 1,
                            Precio = hs.IdVacunaNavigation.Costo,
                            IdUsuarioInserto = idUsuario,
                            IdInsumo = null,
                            FechaInsertado = DateTime.Now
                        };
                        _context.VentaDetalles.Add(nuevDetalle);
                        await _context.SaveChangesAsync();
                        // actualizamos el estado de facturado en historialclinico
                        var updt = await _context.HistorialClinicoS.Include(t => t.IdVacunaNavigation)
                           .Include(e => e.IdProcedimientoNavigation)
                          .Include(x => x.IdTratamientoNavigation)
                          .Include(j => j.IdPatologiaNavigation)
                          .Include(z => z.IdPacienteNavigation)
                           .Where(u => u.IdHistorial == hs.IdHistorial && !u.Borrado)
                            .FirstOrDefaultAsync();

                        updt.Facturado = true;
                        await _context.SaveChangesAsync();
                    }
                    if (hs.IdTratamiento != null)
                    {
                        VentaDetalle nuevDetalle = new VentaDetalle()
                        {
                            IdHistorial = hs.IdHistorial,
                            IdVenta = idVenta,
                            Descripcion = "Tratamiento - " + hs.IdTratamientoNavigation.Nombre,
                            Cantidad = 1,
                            Precio = hs.IdTratamientoNavigation.Costo,
                            IdUsuarioInserto = idUsuario,
                            IdInsumo = null,
                            FechaInsertado = DateTime.Now
                        };
                        _context.VentaDetalles.Add(nuevDetalle);
                        await _context.SaveChangesAsync();
                        // actualizamos el estado de facturado en historialclinico
                        var updt = await _context.HistorialClinicoS.Include(t => t.IdVacunaNavigation)
                           .Include(e => e.IdProcedimientoNavigation)
                          .Include(x => x.IdTratamientoNavigation)
                          .Include(j => j.IdPatologiaNavigation)
                          .Include(z => z.IdPacienteNavigation)
                           .Where(u => u.IdHistorial == hs.IdHistorial && !u.Borrado)
                            .FirstOrDefaultAsync();

                        updt.Facturado = true;
                        await _context.SaveChangesAsync();
                    }

                }
                if (historialesNoFacturados == null)
                {
                    throw new Exception("No se encontraron servicios");

                }
            }
        }
        public async Task<HistorialClinico> ObtenerHistorialClinicoPorId(int idHistorial)
        {
            try
            {
                var paciente = await _context.HistorialClinicoS.Include(t => t.IdVacunaNavigation)
                   .Include(e => e.IdProcedimientoNavigation)
                  .Include(x => x.IdTratamientoNavigation)
                  .Include(j => j.IdPatologiaNavigation)
                  .Include(z => z.IdPacienteNavigation).ThenInclude(r => r.IdClienteNavigation).ThenInclude(u => u.IdPersonaNavigation)
                   .Where(u => u.IdHistorial == idHistorial && !u.Borrado)
                    .FirstOrDefaultAsync();
                return paciente;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<HistorialClinico>> ObtenerHistorialClinicoPorPaciente(int idPaciente)
        {
            try
            {
                var paciente = await _context.HistorialClinicoS.Include(t => t.IdVacunaNavigation)
                   .Include(e => e.IdProcedimientoNavigation)
                  .Include(x => x.IdTratamientoNavigation)
                  .Include(j => j.IdPatologiaNavigation)
                  .Include(z => z.IdPacienteNavigation).ThenInclude(r => r.IdClienteNavigation).ThenInclude(u => u.IdPersonaNavigation)
                   .Where(u => u.IdPaciente == idPaciente && !u.Borrado)
                    .ToListAsync();
                return paciente;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<HistorialClinico>> ObtenerHistorialClinico()
        {
            try
            {
                var hc = await _context.HistorialClinicoS.Include(t => t.IdVacunaNavigation)
                  .Include(e => e.IdProcedimientoNavigation)
                  .Include(x => x.IdTratamientoNavigation)
                  .Include(j => j.IdPatologiaNavigation)
                  .Include(z => z.IdPacienteNavigation).ThenInclude( r => r.IdClienteNavigation).ThenInclude( u => u.IdPersonaNavigation)
                 .Where(u => !u.Borrado)
                    .ToListAsync();
                return hc;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task AgregarHistorialClinico(HistorialClinico historial,int idUsuario)
        {
            // insert a ventas y detalles
            try
            {
                historial.IdUsuarioInserto = idUsuario;
                historial.Facturado = false;
                _context.HistorialClinicoS.Add(historial);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            #region deprecado
            //try
            //{
            //    var HS = await _context.HistorialClinicoS.Include(t => t.IdVacunaNavigation)
            //       .Include(e => e.IdProcedimientoNavigation)
            //      .Include(x => x.IdTratamientoNavigation)
            //      .Include(j => j.IdPatologiaNavigation)
            //      .Include(z => z.IdPacienteNavigation)
            //       .Where(u => u.IdHistorial == historial.IdHistorial && !u.Borrado)
            //        .FirstOrDefaultAsync();
            //    var  paciente = await _context.Pacientes
            //     .Where(u => u.IdPaciente == historial.IdPaciente).FirstOrDefaultAsync();

            //    var venta = await _context.Ventas
            //     .Where(u => !u.Facturado && u.IdCliente == paciente.IdCliente ).FirstOrDefaultAsync();

            //    if (venta == null)
            //    {
            //        int maxNroBoleta = await _context.Ventas
            //        .MaxAsync(v => v.NroBoleta);

            //        Venta nuevaVenta = new Venta()
            //        {
            //            NroBoleta = maxNroBoleta + 1,
            //            Facturado = false,
            //            FechaVenta = DateTime.Now,
            //            IdCliente = (int)paciente.IdCliente,
            //            IdUsuarioInserto = idUsuario,
            //            FechaInsertado = DateTime.Now
            //        };
            //        _context.Ventas.Add(nuevaVenta);
            //        await _context.SaveChangesAsync();



            //        //// insertar un detalle por cada item
            //        if (historial.IdProcedimiento != null)
            //        {
            //            VentaDetalle nuevDetalle = new VentaDetalle()
            //            {
            //                IdHistorial = historial.IdHistorial,
            //                IdVenta = nuevaVenta.IdVenta,
            //                Descripcion = "Procedimiento - " + HS.IdProcedimientoNavigation.Descripcion,
            //                Cantidad = 1,
            //                Precio = HS.IdProcedimientoNavigation.Costo,
            //                IdUsuarioInserto = idUsuario,
            //                IdInsumo = null,
            //                FechaInsertado = DateTime.Now
            //            };
            //            _context.VentaDetalles.Add(nuevDetalle);
            //            await _context.SaveChangesAsync();
            //        }
            //        if (historial.IdVacuna != null)
            //        {
            //            VentaDetalle nuevDetalle = new VentaDetalle()
            //            {
            //                IdHistorial = historial.IdHistorial,
            //                IdVenta = nuevaVenta.IdVenta,
            //                Descripcion = "Vacuna - " + HS.IdVacunaNavigation.Nombre,
            //                Cantidad = 1,
            //                Precio = HS.IdVacunaNavigation.Costo,
            //                IdUsuarioInserto = idUsuario,
            //                IdInsumo = null,
            //                FechaInsertado = DateTime.Now
            //            };
            //            _context.VentaDetalles.Add(nuevDetalle);
            //            await _context.SaveChangesAsync();
            //        }
            //        if (historial.IdTratamiento != null)
            //        {
            //            VentaDetalle nuevDetalle = new VentaDetalle()
            //            {
            //                IdHistorial = historial.IdHistorial,
            //                IdVenta = nuevaVenta.IdVenta,
            //                Descripcion = "Tratamiento - " + HS.IdTratamientoNavigation.Nombre,
            //                Cantidad = 1,
            //                Precio = HS.IdTratamientoNavigation.Costo,
            //                IdUsuarioInserto = idUsuario,
            //                IdInsumo = null,
            //                FechaInsertado = DateTime.Now
            //            };
            //            _context.VentaDetalles.Add(nuevDetalle);
            //            await _context.SaveChangesAsync();
            //        }


            //    }
            //    else
            //    {
            //        //// insertar un detalle por cada item
            //        if (historial.IdProcedimiento != null)
            //        {
            //            VentaDetalle nuevDetalle = new VentaDetalle()
            //            {
            //                IdHistorial = historial.IdHistorial,
            //                IdVenta = venta.IdVenta,
            //                Descripcion = "Procedimiento - " +  HS.IdProcedimientoNavigation.Descripcion,
            //                Cantidad = 1,
            //                Precio = HS.IdProcedimientoNavigation.Costo,
            //                IdUsuarioInserto = idUsuario,
            //                IdInsumo = null,
            //                FechaInsertado = DateTime.Now
            //            };
            //            _context.VentaDetalles.Add(nuevDetalle);
            //            await _context.SaveChangesAsync();
            //        }
            //        if (historial.IdVacuna != null)
            //        {
            //            VentaDetalle nuevDetalle = new VentaDetalle()
            //            {
            //                IdHistorial = historial.IdHistorial,
            //                IdVenta = venta.IdVenta,
            //                Descripcion = "Vacuna - " + HS.IdVacunaNavigation.Nombre,
            //                Cantidad = 1,
            //                Precio = HS.IdVacunaNavigation.Costo,
            //                IdUsuarioInserto = idUsuario,
            //                IdInsumo = null,
            //                FechaInsertado = DateTime.Now
            //            };
            //            _context.VentaDetalles.Add(nuevDetalle);
            //            await _context.SaveChangesAsync();
            //        }
            //        if (historial.IdTratamiento != null)
            //        {
            //            VentaDetalle nuevDetalle = new VentaDetalle()
            //            {
            //                IdHistorial = historial.IdHistorial,
            //                IdVenta = venta.IdVenta,
            //                Descripcion = "Tratamiento - " + HS.IdTratamientoNavigation.Nombre,
            //                Cantidad = 1,
            //                Precio = HS.IdTratamientoNavigation.Costo,
            //                IdUsuarioInserto = idUsuario,
            //                IdInsumo = null,
            //                FechaInsertado = DateTime.Now
            //            };
            //            _context.VentaDetalles.Add(nuevDetalle);
            //            await _context.SaveChangesAsync();
            //        }
            //    }                          
            //}
            //catch (Exception e)
            //{

            //    throw e;
            //}
            #endregion
        }
        public async Task UpdateHistorilaClinico(HistorialClinico dto, int idUsuario)
        {
            try
            {
                var historialClinico = await _context.HistorialClinicoS
                                .Where(mm => mm.IdPaciente == dto.IdPaciente)
                                .FirstOrDefaultAsync();

                if (historialClinico == null)
                {
                    throw new Exception($"No se puede encontrar el paciente {dto.IdHistorial}");
                }
                else
                {
                    historialClinico.IdTratamiento = dto.IdTratamiento;
                    historialClinico.IdPaciente = dto.IdPaciente;
                    historialClinico.IdPatologia = dto.IdPatologia;
                    historialClinico.IdVacuna = dto.IdVacuna;
                    historialClinico.Descripcion = dto.Descripcion.Trim();
                    historialClinico.IdProcedimiento = dto.IdProcedimiento;
                    historialClinico.IdUsuarioModifico = idUsuario;
                    historialClinico.FechaModificado = DateTime.Now;
                    await _context.SaveChangesAsync();


                }
            }
            catch (Exception e)
            {
                throw e;
            }

            
        }
        public async Task BorrarHistorialClinico(int idHistorial)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idHistorial),
                        new SqlParameter("@tabla", "Paciente"));

                    var paciente = await _context.HistorialClinicoS.Where(r => r.IdHistorial == idHistorial)
                        .SingleOrDefaultAsync();

                    if (paciente == null)
                    {
                        throw new Exception("No existe el historial!");
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
