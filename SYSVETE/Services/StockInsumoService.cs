using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IStockServiceService
    {
        Task<List<StockInsumo>> ObtenerStocks();
        Task<StockInsumo> ObtenerStockPorId(int idStock);
        Task<StockInsumo> ObtenerStockPorInsumo(int idInsumo);
        Task UpdateStock(StockInsumo stock, int idUsuario);

        Task BorrarStock(int idStock, int idUsuario);
    }
    public class StockServiceService : IStockServiceService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public StockServiceService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<StockInsumo>> ObtenerStocks()
        {
            try
            {
                var stock = await _context.StockInsumos
                    .Include(x => x.IdInsumoNavigation).ThenInclude(r => r.IdTipoInsumoNavigation)
                    .Include(x => x.IdInsumoNavigation).ThenInclude(r => r.IdImpuestoNavigation)
                    .ToListAsync();
                return stock;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<StockInsumo> ObtenerStockPorId(int idStock)
        {
            try
            {
                var stock = await _context.StockInsumos
                    .Where(u => u.IdStock == idStock).Include(x => x.IdInsumoNavigation).ThenInclude(r => r.IdTipoInsumoNavigation)
                    .Include(x => x.IdInsumoNavigation).ThenInclude(r => r.IdImpuestoNavigation)
                    .FirstOrDefaultAsync();
                return stock;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<StockInsumo> ObtenerStockPorInsumo(int idInsumo)
        {
            try
            {
                var stock = await _context.StockInsumos
                    .Where(u => u.IdInsumo == idInsumo)
                    .Include(x => x.IdInsumoNavigation).ThenInclude( r => r.IdTipoInsumoNavigation)
                    .Include(x => x.IdInsumoNavigation).ThenInclude(r => r.IdImpuestoNavigation)
                    .FirstOrDefaultAsync();
                return stock;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateStock(StockInsumo stock, int idUsuario)
        {

            var existeStock = await _context.StockInsumos
                .Where(mm => mm.IdStock == stock.IdStock)
                .FirstOrDefaultAsync();
          
            if (existeStock == null)
            {
                throw new Exception($"No se puede stock {stock.IdStock}");
            }
            else
            {
                var diferencia = stock.CantidadActual - existeStock.CantidadActual;


                existeStock.CantidadActual = stock.CantidadActual;
                existeStock.IdUsuarioModifico = idUsuario;
                existeStock.FechaModificado = DateTime.Now;
                await _context.SaveChangesAsync();

                //Insertamos historial con el cambio


            }


        }

        public async Task BorrarStock(int idStock, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idStock),
                        new SqlParameter("@tabla", "StockInsumos"));

                    var stockInsumo = await _context.StockInsumos.Where(r => r.IdStock == idStock)
                        .SingleOrDefaultAsync();

                    if (stockInsumo == null)
                    {
                        throw new Exception("No existe el stock!");
                    }

                    stockInsumo.Borrado = true;
                    stockInsumo.IdUsuarioModifico = idUsuario;
                    stockInsumo.FechaBorrado = DateTime.Now;
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
