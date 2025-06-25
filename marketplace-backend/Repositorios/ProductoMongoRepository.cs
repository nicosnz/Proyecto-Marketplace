using System;
using System.Collections.Generic;
using System.Linq;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using MongoDB.Driver;

namespace marketplace_backend.Repositorios
{
    public class ProductoMongoRepository : IProductoMongoRepository
    {
        private readonly IMongoCollection<ProductoImagenConComentarios> _coleccion;

        public ProductoMongoRepository(IMongoDatabase database)
        {
            _coleccion = database.GetCollection<ProductoImagenConComentarios>("producto");
        }

        public void Insertar(ProductoImagenConComentarios imagen)
        {
            _coleccion.InsertOne(imagen);
        }

        public List<ProductoImagenConComentarios> ObtenerProductoPorId(int productoId)
        {
            return _coleccion.Find(x => x.ProductoId == productoId).ToList();
        }

        // public ProductoImagenConComentarios ObtenerProductoPorId(string id)
        // {
        //     return _coleccion.Find(x => x.Id == id).FirstOrDefault();
        // }

        public void Eliminar(string id)
        {
            _coleccion.DeleteOne(x => x.Id == id);
        }

        public void AgregarComentario(int productoId, ComentarioDto comentario)
        {
            var filtro = Builders<ProductoImagenConComentarios>.Filter.Eq(x => x.ProductoId, productoId);
            var actualizacion = Builders<ProductoImagenConComentarios>.Update.Push(x => x.Comentarios, comentario);

            var resultado = _coleccion.UpdateOne(filtro, actualizacion);
            
        }
    }
}