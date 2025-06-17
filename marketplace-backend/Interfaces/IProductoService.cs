using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;
using marketplace_backend.Models;

namespace marketplace_backend.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoConImagendto>> ObtenerProductosDisponibles();
        Task<IEnumerable<Categoria>> ObtenerCategorias();

        Task<IEnumerable<ProductoConImagendto>> ObtenerProductosPorUsuario(int usuarioID);
        Task<IEnumerable<ProductoConImagendto>> ObtenerProductosMenosUsuario(int usuarioID);
        Task<Producto> EditarProducto(Producto producto);
        Task<Producto> AñadirProducto(Producto producto, int usuarioID);
        Task<bool> EliminarProducto(int productoID);
        Task<Producto> ObtenerProducto(int productoID); 



    }
}