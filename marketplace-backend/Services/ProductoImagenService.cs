using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using marketplace_backend.Repositorios;

namespace marketplace_backend.Services
{
    public class ProductoImagenService : IProductoImagenService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProductoImagenRepository _repo;

        public ProductoImagenService(IProductoImagenRepository repo, IUsuarioRepository usuarioRepository)
        {
            _repo = repo;
            _usuarioRepository = usuarioRepository;
        }

        public Task InsertarAsync(ProductoImagen imagen)
        {
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
        public async Task AgregarComentarioAsync(int productoId, Comentariodto comentario,int userId)
        {
            var persona = await _usuarioRepository.ObtenerInfoUsuario(userId);
            var comentarioNuevo = new Comentariodto
            {
                usuarioId = userId,
                nombreUsuario = persona.Nombre,
                texto = comentario.texto
            };
            await _repo.AgregarComentarioAsync(productoId, comentarioNuevo);
        }

    }
}