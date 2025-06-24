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
        List<ProductoConImagendto> ObtenerProductosDisponibles();
        List<Categoria> ObtenerCategorias();
        List<ProductoConImagendto> ObtenerProductosPorUsuario(int usuarioID);
        List<ProductoConImagendto> ObtenerProductosMenosUsuario(int usuarioID);
        List<ProductoConImagendto> ObtenerProductosPorCategoria(int categoriaID,int usuarioID);
        Producto EditarProducto(ProductoEditar dto,int id,int usuarioID);
        Producto AñadirProducto(Productodto dto, int usuarioID,IFormFile imagen);
        bool EliminarProducto(int productoID);
        ProductoConImagendto ObtenerProducto(int productoID); 



    }
}