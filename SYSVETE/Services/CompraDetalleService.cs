using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface ICompraDetalleService
    {
        Task<List<CompraDetalleDto>> ObtenerDetallesDeCompra(int idCompra);
        Task<CompraDetalleDto?> ObtenerDetallePorId(int idDetalle);

        Task<int> AgregarCompraDetalle(CompraDetalleDto model);
        Task UpdateCompraDetalle(CompraDetalleDto model);
        Task BorrarCompraDetalle(int idDetalle);
    }
    public class CompraDetalleService : ICompraDetalleService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public CompraDetalleService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }

        public async Task<int> AgregarCompraDetalle(CompraDetalleDto model)
        {
            try
            {
                CompraDetalle compraDetalle = new()
                {
                    IdCompra = model.IdCompra,
                    IdCompraDetalle = model.IdCompraDetalle,
                    Precio = model.Precio,
                    Cantidad = model.Cantidad,
                    Descripcion = model.Descripcion,
                    IdInsumo = model.IdInsumo
                };

                _context.CompraDetalles.Add(compraDetalle);
                await _context.SaveChangesAsync();
               await AgregarInsumoStock(compraDetalle);
                return model.IdCompraDetalle;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task BorrarCompraDetalle(int idDetalle)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idDetalle),
                        new SqlParameter("@tabla", "CompraDetalle"));

                    var data = await _context.CompraDetalles.Where(r => r.IdCompraDetalle == idDetalle)
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

        public async Task<CompraDetalleDto?> ObtenerDetallePorId(int idDetalle)
        {
            try
            {
                return await _context.CompraDetalles
                    .Where(cd => cd.IdCompraDetalle == idDetalle)
                    .Select(cd => new CompraDetalleDto()
                    {
                        IdCompra = cd.IdCompra,
                        IdCompraDetalle = cd.IdCompraDetalle,
                        Precio = cd.Precio,
                        Cantidad = cd.Cantidad,
                        Descripcion = cd.Descripcion,
                        IdInsumo = cd.IdInsumo
                    })
                    .SingleOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CompraDetalleDto>> ObtenerDetallesDeCompra(int idCompra)
        {
            try
            {
                if (idCompra == 0)
                {
                    throw new Exception("La compra no es valida!");
                }

                return await _context.CompraDetalles
                    .Include(cd => cd.Insumo)
                        .ThenInclude(i => i.IdImpuestoNavigation)
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
                            descripcion = cd.Insumo.descripcion,
                            impuesto = new ImpuestoDtocs()
                            {
                                Descripcion = cd.Insumo.IdImpuestoNavigation.Descripcion,
                                Valor = cd.Insumo.IdImpuestoNavigation.Valor
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

        public async Task UpdateCompraDetalle(CompraDetalleDto model)
        {
            try
            {
                var existe = await _context.CompraDetalles
                    .Where(cd => cd.IdCompraDetalle == model.IdCompraDetalle && !cd.Borrado)
                    .FirstOrDefaultAsync();
                if (existe == null)
                {
                    throw new Exception("Detalle no encontrado!");
                }

                existe.Descripcion = model.Descripcion;
                existe.IdInsumo = model.IdInsumo;
                existe.Cantidad = model.Cantidad;
                existe.Precio = model.Precio;
                existe.FechaModificado = DateTime.Now;

                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AgregarInsumoStock(CompraDetalle dto)
        {
            try
            {
                var stockActual = await _context.StockInsumos
                    .Where(cd => cd.IdInsumo == dto.IdInsumo)
                    .SingleOrDefaultAsync();

                if (stockActual != null)
                {
                    stockActual.IdInsumo = stockActual.IdInsumo;
                    stockActual.CantidadActual = stockActual.CantidadActual + dto.Cantidad;
                    stockActual.FechaModificado = DateTime.Now;

                    await _context.SaveChangesAsync();
                }
                else
                {
                   
                    StockInsumo entrada = new()
                    {
                        IdInsumo = dto.IdInsumo,
                        CantidadActual = dto.Cantidad,
                    };

                    _context.StockInsumos.Add(entrada);
                    await _context.SaveChangesAsync();
                }
            }

            catch (Exception e)
            {

                throw;
            }
        }
    }
}
