using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;

namespace marketplace_backend.Interfaces
{
    public interface IProductoMongoRepository
    {
        void Insertar(ProductoImagenConComentarios imagen);
        List<ProductoImagenConComentarios> ObtenerProductoPorId(int productoId);
        // ProductoImagenConComentarios ObtenerProductoPorId(string id);
        void Eliminar(string id);
        void AgregarComentario(int productoId, ComentarioDto comentario);
        
    }
}