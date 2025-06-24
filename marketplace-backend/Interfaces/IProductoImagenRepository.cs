using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;

namespace marketplace_backend.Interfaces
{
    public interface IProductoImagenRepository
    {
        void Insertar(ProductoImagen imagen);


        List<ProductoImagen> ObtenerPorProductoId(int productoId);
        ProductoImagen ObtenerPorId(string id);


        void Eliminar(string id);
        void AgregarComentario(int productoId, Comentariodto comentario);
        
    }
}