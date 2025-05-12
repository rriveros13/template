using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface ICompraService
    {
        Task<List<CompraDto>> ObtenerCompras();
        Task<CompraDto?> ObtenerComprasPorId(int idCompra);
        Task<CompraDto?> ObtenerMontos(int idCompra);
        Task FinalizarCompra(int idVenta, int idUsuario);

        Task<int> AgregarCompra(CompraDto model);
        Task UpdateCompra(CompraDto model);
        Task BorrarCompra(int idCompra);
    }
    public class CompraService : ICompraService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public CompraService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }

        public async Task<int> AgregarCompra(CompraDto model)
        {
            try
            {
                Compra compra = new()
                {
                    IdCompra = model.IdCompra,
                    NroBoleta = model.NroBoleta,
                    IdProveedor = model.IdProveedor,
                    FechaCompra = model.FechaCompra,
                    TipoCompra = model.TipoCompra
                };

                _context.Compras.Add(compra);
                await _context.SaveChangesAsync();

                return compra.IdCompra;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task BorrarCompra(int idCompra)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                    //    new SqlParameter("@id", idCompra),
                    //    new SqlParameter("@tabla", "Compra"));

                    var data = await _context.Compras.Where(r => r.IdCompra == idCompra)
                        .SingleOrDefaultAsync();

                    if (data == null)
                    {
                        throw new Exception("No existen datos!");
                    }

                    data.Borrado = true;
                    data.FechaBorrado = DateTime.Now;

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

        public async Task<List<CompraDto>> ObtenerCompras()
        {
            try
            {

                var compraDetalless = await _context.Compras
                .Where(v => !_context.CompraDetalles.Any(d => d.IdCompra == v.IdCompra))
                .ToListAsync();
                foreach (var updt in compraDetalless)
                {
                    updt.Borrado = true;
                    await _context.SaveChangesAsync();

                }

                return await _context.Compras
                    .Include(p => p.Proveedor)
                    .ThenInclude(p => p.IdPersonaNavigation)
                    .Select(c => new CompraDto()
                    {
                        IdCompra = c.IdCompra,
                        NroBoleta = c.NroBoleta,
                        IdProveedor = c.IdProveedor,
                        Finalizado = c.Finalizado,
                        FechaCompra = c.FechaCompra,
                        TipoCompra = c.TipoCompra,
                        Facturado = c.Facturado,
                        proveedorDto = new ProveedorDto()
                        {
                            IdProveedor = c.IdProveedor,
                            Ruc = c.Proveedor.Ruc,
                            Persona = new PersonaDto()
                            {
                                Nombre = c.Proveedor.IdPersonaNavigation.Nombre,
                                Apellido = c.Proveedor.IdPersonaNavigation.Apellido,
                            }
                        }
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CompraDto?> ObtenerComprasPorId(int idCompra)
        {
            try
            {
                return await _context.Compras
                    .Where(c => c.IdCompra == idCompra)
                    .Select(c => new CompraDto()
                    {
                        IdCompra = c.IdCompra,
                        NroBoleta = c.NroBoleta,
                        Finalizado = c.Finalizado,
                        IdProveedor = c.IdProveedor,
                        FechaCompra = c.FechaCompra,
                        TipoCompra = c.TipoCompra,
                        Facturado = c.Facturado,

                    })
                    .SingleOrDefaultAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<CompraDto?> ObtenerMontos(int idCompra)
        {
            CompraDto montos = new CompraDto();

            try
            {
                if (idCompra == 0)
                {
                    throw new Exception("La compra no es valida!");
                }

                var MontoTotal = await _context.CompraDetalles
                    .Include(cd => cd.Insumo)
                    .Where(cd => cd.IdCompra == idCompra)
                    .Select(cd => new CompraDetalleDto()
                    {
                        IdCompra = cd.IdCompra,
                        IdCompraDetalle = cd.IdCompraDetalle,
                        Precio = cd.Precio,
                        Cantidad = cd.Cantidad,
                        Descripcion = cd.Descripcion,
                        IdInsumo = cd.IdInsumo,
                        Insumo = new InsumoDto()
                        {
                            idImpuesto = cd.IdInsumo,
                            descripcion = cd.Insumo.descripcion
                        }

                    })
                    .ToListAsync();
                if (MontoTotal.Any())
                {
                    foreach (var monto in MontoTotal)
                    {
                        montos.MontoTotal += (monto.Cantidad * monto.Precio);
                    }
                }


                var deuda = await _context.DeudaProveedores.Include(x => x.IdCompraNavigation)
                                    .Where(u => u.IdCompra == idCompra)
                                    .ToListAsync();
                if (deuda != null)
                {
                    foreach (var d in deuda)
                    {
                        montos.MontoAbonado += d.MontoPagado;
                    }
                }

                montos.SaldoPendiente = montos.MontoTotal - montos.MontoAbonado;
                if (montos.SaldoPendiente <= 0 && montos.MontoTotal > 0)
                {
                    var compraUpdate = await _context.Compras
                    .Where(mm => mm.IdCompra == idCompra)
                    .FirstOrDefaultAsync();
                    if (compraUpdate != null && !compraUpdate.Facturado)
                    {
                        CompraDto dto = new CompraDto()
                        {
                            IdCompra = compraUpdate.IdCompra,
                            NroBoleta = compraUpdate.NroBoleta,
                            IdProveedor =compraUpdate.IdProveedor,
                            TipoCompra = compraUpdate.TipoCompra,
                            Facturado = true,
                            FechaCompra = compraUpdate.FechaCompra                        
                        };
                        await UpdateCompra(dto);
                    }
                }

                return montos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task FinalizarCompra(int idCompra, int idUsuario)
        {

            var venta = await _context.Compras
                .Where(mm => mm.IdCompra == idCompra)
                .FirstOrDefaultAsync();

            if (venta == null)
            {
                throw new Exception($"No se puede encontrar el venta {idCompra}");
            }
            else
            {
                venta.Finalizado = true;
                venta.IdUsuarioModifico = idUsuario;
                venta.FechaModificado = DateTime.Now;
            }
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCompra(CompraDto model)
        {
            try
            {
                var compra = await _context.Compras
                    .Where(c => c.IdCompra == model.IdCompra)
                    .SingleOrDefaultAsync();

                if (compra == null)
                {
                    throw new Exception("No se encontro ninguna compra");
                }

                compra.IdCompra = model.IdCompra;
                compra.NroBoleta = model.NroBoleta;
                compra.IdProveedor = model.IdProveedor;
                compra.FechaCompra = model.FechaCompra;
                compra.TipoCompra = model.TipoCompra;
                compra.Facturado = model.Facturado;
                compra.FechaModificado = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
