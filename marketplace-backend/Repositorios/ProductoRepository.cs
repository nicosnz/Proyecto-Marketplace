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
        public async Task<Producto> EditarProducto(Producto producto)
        {
            var productoEditado = await _context.Productos.FromSqlInterpolated($"EXEC EditarProducto @ProductoID = {producto.ProductoId}, @Nombre = {producto.Nombre}, @Descripcion = {producto.Descripcion}, @Precio = {producto.Precio}, @Stock = {producto.Stock}")
                .AsNoTracking()
                .ToListAsync();
            return productoEditado.FirstOrDefault()!;
        }
        public async Task<Producto> AñadirProducto(Producto producto, int usuarioID)
        {
            var productoNuevo = await _context.Productos.FromSqlInterpolated($"EXEC sp_InsertarProducto @VendedorID = {usuarioID}, @CategoriaID = {producto.CategoriaId},@Nombre = {producto.Nombre}, @Descripcion = {producto.Descripcion}, @Precio = {producto.Precio}, @Stock = {producto.Stock}")
                .AsNoTracking()
                .ToListAsync();
            return productoNuevo.FirstOrDefault()!;
        }




    }
}