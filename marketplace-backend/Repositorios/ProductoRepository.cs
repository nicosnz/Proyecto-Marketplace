using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<VwProductosCatalogo> ObtenerTodosProductosDisponibles()
        {
            return _context.VwProductosCatalogos.ToList();
        }

        public List<Categoria> ObtenerCategorias()
        {
            return _context.Categorias.FromSqlInterpolated($"EXEC sp_ObtenerCategorias").ToList();
        }

        public List<Producto> ObtenerProductosPorUsuario(int usuarioID)
        {
            return _context.Productos
                .FromSqlInterpolated($"EXEC sp_ObtenerProductosPorUsuario @usuarioId = {usuarioID}")
                .ToList();
        }

        public List<Producto> ObtenerProductosMenosUsuario(int usuarioID)
        {
            return _context.Productos
                .FromSqlInterpolated($"EXEC sp_ObtenerProductosMenosUsuario @usuarioId = {usuarioID}")
                .ToList();
        }

        public List<Producto> ObtenerProductosPorCategoria(int categoriaID, int usuarioID)
        {
            return _context.Productos
                .FromSqlInterpolated($"EXEC sp_ObtenerProductoPorCategoria @categoriaId = {categoriaID}, @vendedorId = {usuarioID}")
                .ToList();
        }

        public Producto EditarProducto(Producto producto)
        {
            var productoEditado = _context.Productos.FromSqlInterpolated($"EXEC EditarProducto @ProductoID = {producto.ProductoId}, @Nombre = {producto.Nombre}, @Descripcion = {producto.Descripcion}, @Precio = {producto.Precio}, @Stock = {producto.Stock}, @CategoriaId = {producto.CategoriaId}")
                .AsNoTracking()
                .ToList();
            return productoEditado.FirstOrDefault()!;
        }

        public Producto AñadirProducto(Producto producto, int usuarioID)
        {
            var productoNuevo = _context.Productos.FromSqlInterpolated($"EXEC sp_InsertarProducto @VendedorID = {usuarioID}, @CategoriaID = {producto.CategoriaId},@Nombre = {producto.Nombre}, @Descripcion = {producto.Descripcion}, @Precio = {producto.Precio}, @Stock = {producto.Stock}")
                .AsNoTracking()
                .ToList();
            return productoNuevo.FirstOrDefault()!;
        }

        public bool EliminarProducto(int productoID)
        {
            int filas = _context.Database.ExecuteSqlRaw("EXEC sp_DesactivarProducto @ProductoID = {0}", productoID);
            return filas > 0;
        }

        public Producto ObtenerProducto(int productoID)
        {
            var productoObtenido = _context.Productos.FromSqlInterpolated($"EXEC sp_ObtenerProducto @productoId = {productoID}")
                .AsNoTracking()
                .ToList();
            return productoObtenido.FirstOrDefault()!;
        }
    }
}