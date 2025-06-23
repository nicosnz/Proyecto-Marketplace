using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;

namespace marketplace_backend.Interfaces
{
    public interface IProductoImagenRepository
    {
        Task InsertarAsync(ProductoImagen imagen);


        Task<List<ProductoImagen>> ObtenerPorProductoIdAsync(int productoId);
        Task<ProductoImagen?> ObtenerPorIdAsync(string id);


        Task EliminarAsync(string id);
        Task AgregarComentarioAsync(int productoId, Comentariodto comentario);
        
    }
}