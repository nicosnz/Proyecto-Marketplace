using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace marketplace_backend.Repositorios
{
    public class OrdenesRepository : IOrdenesRepository
    {
        private readonly MarketplaceDbContext _context;

        public OrdenesRepository(MarketplaceDbContext context)
        {

            _context = context;
        }
        public async Task<int> InsertarOrden(int compradorId, string direccion, string pais, string ciudad)
        {
            var ordenId = await _context.Database
               .SqlQuery<OrdenIdResultado>($"EXEC sp_InsertarOrden @CompradorID={compradorId}, @Direccion={direccion}, @Ciudad={ciudad}, @Pais={pais}")
               .ToListAsync();
            return ordenId.First().OrdenId;

        }
        public async Task MarcarOrdenComoPagada(int ordenId)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC sp_MarcarOrdenComoPagada @OrdenID={ordenId}");
        }
        public async Task AgregarProductoADetalleOrden(int ordenId, int productoId, int cantidad)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC sp_AgregarProductoADetalleOrden @OrdenID={ordenId}, @ProductoID={productoId}, @Cantidad={cantidad}");
        }
        public async Task<IEnumerable<DetalleComprasUsuario>> ObtenerComprasUsuario(int usuarioID)
        {
            return await _context.Set<DetalleComprasUsuario>()
                .FromSqlInterpolated($"EXEC ObtenerDetallesComprasUsuario @UsuarioId = {usuarioID}")
                .ToListAsync();
        }
        public async Task<IEnumerable<ProductosVendidosUsuario>> ObtenerProductosVendidosUsuario(int usuarioID)
        {
            return await _context.Set<ProductosVendidosUsuario>()
                .FromSqlInterpolated($"EXEC ObtenerProductosVendidosPorUsuario @UsuarioId = {usuarioID}")
                .ToListAsync();
        }




    }
}