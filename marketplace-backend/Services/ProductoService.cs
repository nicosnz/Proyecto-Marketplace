using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;

namespace marketplace_backend.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProductoService(IProductoRepository productoRepository, IUsuarioRepository usuarioRepository)
        {
            _productoRepository = productoRepository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<IEnumerable<VwProductosCatalogo>> ObtenerProductosDisponiblesAsync()
        {
            return await _productoRepository.ObtenerProductosDisponiblesAsync();
        }
        public async Task<IEnumerable<Producto>> ObtenerProductosPorUsuarioAsync(int usuarioID)
        {
            if (!await _usuarioRepository.ExisteUsuarioAsync(usuarioID))
            {
                throw new UsuarioNotFound();
            }
            var productosUsuario = await _productoRepository.ObtenerProductosPorUsuarioAsync(usuarioID);
            if (!productosUsuario.Any())
            {
                throw new ProductosNotFound();
            }
            return productosUsuario;
        }
        public async Task<IEnumerable<Producto>> ObtenerProductosMenosUsuarioAsync(int usuarioID)
        {
            if (!await _usuarioRepository.ExisteUsuarioAsync(usuarioID))
            {
                throw new UsuarioNotFound();
            }
            var productosNoUsuario = await _productoRepository.ObtenerProductosMenosUsuarioAsync(usuarioID);
            if (!productosNoUsuario.Any())
            {
                throw new ProductosNotFound();
            }
            return productosNoUsuario;
        }
        public async Task<Producto> EditarProducto(Producto producto)
        {
            var productoNuevo = _productoRepository.EditarProducto(producto);
            return await productoNuevo;
            
        }
        public async Task<Producto> AñadirProducto(Producto producto, int usuarioID)
        {
            var productoNuevo = _productoRepository.AñadirProducto(producto, usuarioID);
            return await productoNuevo;
        }


        
    }
}