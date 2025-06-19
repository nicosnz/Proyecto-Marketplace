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
        public async Task<IEnumerable<VwProductosCatalogo>> ObtenerTodosProductosDisponibles()
        {
            return await _context.VwProductosCatalogos.ToListAsync();
        }
        public async Task<IEnumerable<Categoria>> ObtenerCategorias()
        {
            return await _context.Categorias.FromSqlInterpolated($"EXEC sp_ObtenerCategorias").ToListAsync();
        }

        public async Task<IEnumerable<Producto>> ObtenerProductosPorUsuario(int usuarioID)
        {
            return await _context.Productos
                .FromSqlInterpolated($"EXEC sp_ObtenerProductosPorUsuario @usuarioId = {usuarioID}")
                .ToListAsync();
        }
        public async Task<IEnumerable<Producto>> ObtenerProductosMenosUsuario(int usuarioID)
        {
            return await _context.Productos
                .FromSqlInterpolated($"EXEC sp_ObtenerProductosMenosUsuario @usuarioId = {usuarioID}")
                .ToListAsync();

        }
        public async Task<IEnumerable<Producto>> ObtenerProductosPorCategoria(int categoriaID,int usuarioID)
        {
            return await _context.Productos
                .FromSqlInterpolated($"EXEC sp_ObtenerProductoPorCategoria @categoriaId = {categoriaID}, @vendedorId = {usuarioID}")
                .ToListAsync();
        }

        public async Task<Producto> EditarProducto(Producto producto)
        {
            var productoEditado = await _context.Productos.FromSqlInterpolated($"EXEC EditarProducto @ProductoID = {producto.ProductoId}, @Nombre = {producto.Nombre}, @Descripcion = {producto.Descripcion}, @Precio = {producto.Precio}, @Stock = {producto.Stock}, @CategoriaId = {producto.CategoriaId}")
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

        public async Task<bool> EliminarProducto(int productoID)
        {
            int filas = await _context.Database.ExecuteSqlRawAsync("EXEC sp_DesactivarProducto @ProductoID = {0}", productoID);
            return filas > 0;

        }
        public async Task<Producto> ObtenerProducto(int productoID){
            var productoObtenido= await _context.Productos.FromSqlInterpolated($"EXEC sp_ObtenerProducto @productoId = {productoID}")
                .AsNoTracking()
                .ToListAsync();
            return productoObtenido.FirstOrDefault()!;
        } 






    }
}