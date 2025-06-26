using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.Models;

namespace marketplace_backend.Interfaces
{
    public interface IProductoRepository
    {
        List<VwProductosCatalogo> ObtenerTodosProductosDisponibles();
        List<Producto> ObtenerProductosPorUsuario(int usuarioID);
        List<Producto> ObtenerProductosMenosUsuario(int usuarioID);
        List<Producto> ObtenerProductosPorCategoria(int categoriaID,int usuarioID);
        List<Categoria> ObtenerCategorias();
        Producto EditarProducto(Producto producto);
        Producto AñadirProducto(Producto producto, int usuarioID); 
        Producto ObtenerProducto(int productoID); 
        void AñadirAFavoritos(int productoID,int usuarioID); 
        void QuitarDeFavoritos(int productoID,int usuarioID); 
        bool EliminarProducto(int productoID);

    }
}