using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace marketplace_backend.Repositorios
{

    public class ProductoRepository : IProductoRepository
    {
        private readonly MarketplaceDbContext _context;
        public ProductoRepository(MarketplaceDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<VwProductosCatalogo>> ObtenerProductosDisponiblesAsync()
        {
            return await _context.VwProductosCatalogos.ToListAsync();
        }
        public async Task<IEnumerable<Producto>> ObtenerProductosPorUsuarioAsync(int usuarioID)
        {
            return await _context.Productos
                .FromSqlInterpolated($"EXEC sp_ObtenerProductosPorUsuario @usuarioId = {usuarioID}")
                .ToListAsync();
        }
        public async Task<IEnumerable<Producto>> ObtenerProductosMenosUsuarioAsync(int usuarioID)
        {
            return await _context.Productos
                .FromSqlInterpolated($"EXEC sp_ObtenerProductosMenosUsuario @usuarioId = {usuarioID}")
                .ToListAsync();
            
        }



    }
}