using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using MongoDB.Driver;

namespace marketplace_backend.Repositorios
{
    public class ProductoImagenRepository:IProductoImagenRepository
    {
        private readonly IMongoCollection<ProductoImagen> _coleccion;

        public ProductoImagenRepository(IMongoDatabase database)
        {
            _coleccion = database.GetCollection<ProductoImagen>("producto");
        }

        public async Task InsertarAsync(ProductoImagen imagen)
        {
            await _coleccion.InsertOneAsync(imagen);
        }

        public async Task<List<ProductoImagen>> ObtenerPorProductoIdAsync(int productoId)
        {
            return await _coleccion.Find(x => x.ProductoId == productoId).ToListAsync();
        }

        public async Task<ProductoImagen?> ObtenerPorIdAsync(string id)
        {
            return await _coleccion.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task EliminarAsync(string id)
        {
            await _coleccion.DeleteOneAsync(x => x.Id == id);
        }
    }
}