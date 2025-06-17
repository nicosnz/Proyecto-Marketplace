using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;

namespace marketplace_backend.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProductoImagenRepository _productoImagenRepository;

        public ProductoService(IProductoRepository productoRepository, IProductoImagenRepository productoImagenRepository, IUsuarioRepository usuarioRepository)
        {
            _productoRepository = productoRepository;
            _usuarioRepository = usuarioRepository;
            _productoImagenRepository = productoImagenRepository;
        }
        public async Task<IEnumerable<ProductoConImagendto>> ObtenerProductosDisponibles()
        {
            var productos = await _productoRepository.ObtenerTodosProductosDisponibles();
            var lista = new List<ProductoConImagendto>();
            foreach (var prod in productos)
            {
                var imagen = (await _productoImagenRepository.ObtenerPorProductoIdAsync(prod.ProductoId)).FirstOrDefault();

                lista.Add(new ProductoConImagendto
                {
                    ProductoId = prod.ProductoId,
                    Nombre = prod.ProductoNombre,
                    Descripcion = prod.Descripcion!,
                    Precio = prod.Precio,
                    Stock = prod.Stock,
                    CategoriaId = prod.CategoriaId,
                    VendedorId = prod.VendedorId,
                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen.Data) : null
                });
            }
            return lista;

        }
        public async Task<IEnumerable<Categoria>> ObtenerCategorias()
        {
            return await _productoRepository.ObtenerCategorias();
        }

        public async Task<IEnumerable<ProductoConImagendto>> ObtenerProductosPorUsuario(int usuarioID)
        {
            if (!await _usuarioRepository.ExisteUsuarioAsync(usuarioID))
            {
                throw new UsuarioNotFound();
            }
            var productosUsuario = await _productoRepository.ObtenerProductosPorUsuario(usuarioID);
            if (!productosUsuario.Any())
            {
                throw new ProductosNotFound();

            }
            var lista = new List<ProductoConImagendto>();

            foreach (var prod in productosUsuario)
            {
                var imagen = (await _productoImagenRepository.ObtenerPorProductoIdAsync(prod.ProductoId)).FirstOrDefault();

                lista.Add(new ProductoConImagendto
                {
                    ProductoId = prod.ProductoId,
                    Nombre = prod.Nombre,
                    Descripcion = prod.Descripcion!,
                    Precio = prod.Precio,
                    Stock = prod.Stock,
                    CategoriaId = (int)prod.CategoriaId!,
                    VendedorId = prod.VendedorId,
                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen.Data) : null
                });
            }
            return lista;
        }
        public async Task<IEnumerable<ProductoConImagendto>> ObtenerProductosMenosUsuario(int usuarioID)
        {
            if (!await _usuarioRepository.ExisteUsuarioAsync(usuarioID))
            {
                throw new UsuarioNotFound();
            }
            var productosNoUsuario = await _productoRepository.ObtenerProductosMenosUsuario(usuarioID);
            if (!productosNoUsuario.Any())
            {
                throw new ProductosNotFound();
            }

            var lista = new List<ProductoConImagendto>();

            foreach (var prod in productosNoUsuario)
            {
                var imagen = (await _productoImagenRepository.ObtenerPorProductoIdAsync(prod.ProductoId)).FirstOrDefault();

                lista.Add(new ProductoConImagendto
                {
                    ProductoId = prod.ProductoId,
                    Nombre = prod.Nombre,
                    Descripcion = prod.Descripcion!,
                    Precio = prod.Precio,
                    Stock = prod.Stock,
                    CategoriaId = (int)prod.CategoriaId!,
                    VendedorId = prod.VendedorId,
                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen.Data) : null
                });
            }
            return lista;


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
        public async Task<bool> EliminarProducto(int productoID)
        {
            bool isActualizado = await _productoRepository.EliminarProducto(productoID);
            return isActualizado;

        }
        public async Task<Producto> ObtenerProducto(int productoID)
        {
            var productoObtenido = await _productoRepository.ObtenerProducto(productoID);
            return productoObtenido;
        } 



        
    }
}