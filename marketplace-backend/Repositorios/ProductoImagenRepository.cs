using System;
using System.Collections.Generic;
using System.Linq;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using MongoDB.Driver;

namespace marketplace_backend.Repositorios
{
    public class ProductoImagenRepository : IProductoImagenRepository
    {
        private readonly IMongoCollection<ProductoImagen> _coleccion;

        public ProductoImagenRepository(IMongoDatabase database)
        {
            _coleccion = database.GetCollection<ProductoImagen>("producto");
        }

        public void Insertar(ProductoImagen imagen)
        {
            _coleccion.InsertOne(imagen);
        }

        public List<ProductoImagen> ObtenerPorProductoId(int productoId)
        {
            return _coleccion.Find(x => x.ProductoId == productoId).ToList();
        }

        public ProductoImagen ObtenerPorId(string id)
        {
            return _coleccion.Find(x => x.Id == id).FirstOrDefault();
        }

        public void Eliminar(string id)
        {
            _coleccion.DeleteOne(x => x.Id == id);
        }

        public void AgregarComentario(int productoId, Comentariodto comentario)
        {
            var filtro = Builders<ProductoImagen>.Filter.Eq(x => x.ProductoId, productoId);
            var actualizacion = Builders<ProductoImagen>.Update.Push(x => x.Comentarios, comentario);

            var resultado = _coleccion.UpdateOne(filtro, actualizacion);
            
        }
    }
}