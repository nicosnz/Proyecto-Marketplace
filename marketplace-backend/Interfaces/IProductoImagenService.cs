using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;

namespace marketplace_backend.Interfaces
{
    public interface IProductoImagenService
    {
        Task InsertarAsync(ProductoImagen imagen);


        Task<List<ProductoImagen>> ObtenerPorProductoIdAsync(int productoId);
        Task<ProductoImagen?> ObtenerPorIdAsync(string id);


        Task EliminarAsync(string id);
        
    
    }
}