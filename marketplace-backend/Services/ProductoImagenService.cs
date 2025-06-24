using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Insertar(ProductoImagen imagen)
        {
            _repo.Insertar(imagen);
        }

        public List<ProductoImagen> ObtenerPorProductoId(int productoId)
        {
            return _repo.ObtenerPorProductoId(productoId);
        }

        public ProductoImagen ObtenerPorId(string id)
        {
            return _repo.ObtenerPorId(id);
        }

        public void Eliminar(string id)
        {
            _repo.Eliminar(id);
        }

        public void AgregarComentario(int productoId, Comentariodto comentario, int userId)
        {
            var persona = _usuarioRepository.ObtenerInfoUsuario(userId);
            var comentarioNuevo = new Comentariodto
            {
                usuarioId = userId,
                nombreUsuario = persona.Nombre,
                texto = comentario.texto
            };
            _repo.AgregarComentario(productoId, comentarioNuevo);
        }
    }
}