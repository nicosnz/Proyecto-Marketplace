using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using marketplace_backend.Repositorios;

namespace marketplace_backend.Services
{
    public class ProductoImagenService:IProductoImagenService
    {
        private readonly IProductoImagenRepository _repo;

        public ProductoImagenService(IProductoImagenRepository repo)
        {
            _repo = repo;
        }

        public Task InsertarAsync(ProductoImagen imagen)
        {
            // Aquí podrías agregar validaciones o lógica de negocio extra si lo necesitas
            return _repo.InsertarAsync(imagen);
        }

        public Task<List<ProductoImagen>> ObtenerPorProductoIdAsync(int productoId)
        {
            return _repo.ObtenerPorProductoIdAsync(productoId);
        }

        public Task<ProductoImagen?> ObtenerPorIdAsync(string id)
        {
            return _repo.ObtenerPorIdAsync(id);
        }

        public Task EliminarAsync(string id)
        {
            return _repo.EliminarAsync(id);
        }
    }
}