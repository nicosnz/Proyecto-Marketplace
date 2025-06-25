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
        List<ProductoConImagenDto> ObtenerProductosDisponibles();
        List<Categoria> ObtenerCategorias();
        List<ProductoConImagenDto> ObtenerProductosPorUsuario(int usuarioID);
        List<ProductoConImagenDto> ObtenerProductosMenosUsuario(int usuarioID);
        List<ProductoConImagenDto> ObtenerProductosPorCategoria(int categoriaID,int usuarioID);
        Producto EditarProducto(ProductoEditar productoAEditar,int id,int usuarioID);
        ProductoConImagenDto AñadirProducto(ProductoAñadirDto productoRequest, int usuarioID,IFormFile imagen);
        bool EliminarProducto(int productoID);
        ProductoConImagenDto ObtenerProducto(int productoID); 



    }
}