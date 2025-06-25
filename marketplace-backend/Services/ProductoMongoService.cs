using System;
using System.Collections.Generic;
using System.Linq;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using marketplace_backend.Repositorios;

namespace marketplace_backend.Services
{
    public class ProductoMongoService : IProductoMongoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProductoMongoRepository _repo;

        public ProductoMongoService(IProductoMongoRepository repo, IUsuarioRepository usuarioRepository)
        {
            _repo = repo;
            _usuarioRepository = usuarioRepository;
        }

        public void Insertar(ProductoImagenConComentarios imagen)
        {
            _repo.Insertar(imagen);
        }

        public List<ProductoImagenConComentarios> ObtenerProductoPorId(int productoId)
        {
            return _repo.ObtenerProductoPorId(productoId);
        }

        // public ProductoImagenConComentarios ObtenerPorId(string id)
        // {
        //     return _repo.ObtenerPorId(id);
        // }

        public void Eliminar(string id)
        {
            _repo.Eliminar(id);
        }

        public void AgregarComentario(int productoId, ComentarioDto comentario, int userId)
        {
            var persona = _usuarioRepository.ObtenerInfoUsuario(userId);
            var comentarioNuevo = new ComentarioDto
            {
                UsuarioId = userId,
                NombreUsuario = persona.Nombre,
                Texto = comentario.Texto
            };
            _repo.AgregarComentario(productoId, comentarioNuevo);
        }
    }
}