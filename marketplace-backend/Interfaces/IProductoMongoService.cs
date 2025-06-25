using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;

namespace marketplace_backend.Interfaces
{
    public interface IProductoMongoService
    {
        void Insertar(ProductoImagenConComentarios imagen);
        List<ProductoImagenConComentarios> ObtenerProductoPorId(int productoId);
        // ProductoImagenConComentarios ObtenerPorId(string id);
        void Eliminar(string id);
        void AgregarComentario(int productoId, ComentarioDto comentario,int userId);
        
    
    }
}