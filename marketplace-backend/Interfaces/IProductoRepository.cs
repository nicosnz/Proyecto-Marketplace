using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.Models;

namespace marketplace_backend.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<VwProductosCatalogo>> ObtenerTodosProductosDisponibles();
        Task<IEnumerable<Producto>> ObtenerProductosPorUsuario(int usuarioID);
        Task<IEnumerable<Producto>> ObtenerProductosMenosUsuario(int usuarioID);
        Task<IEnumerable<Producto>> ObtenerProductosPorCategoria(int categoriaID,int usuarioID);
        Task<IEnumerable<Categoria>> ObtenerCategorias();
        Task<Producto> EditarProducto(Producto producto);
        Task<Producto> AñadirProducto(Producto producto, int usuarioID); 
        Task<Producto> ObtenerProducto(int productoID); 
        Task<bool> EliminarProducto(int productoID);

    }
}