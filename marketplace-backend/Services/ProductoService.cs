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
                var vendedor = await _usuarioRepository.ObtenerInfoUsuario(prod.VendedorId);

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
                    nombreVendedor = vendedor.Nombre,
                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen.Data) : null,
                    Comentarios = imagen != null ? imagen.Comentarios : new List<Comentariodto>()                

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
                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen.Data) : null,
                    Comentarios = imagen != null ? imagen.Comentarios : new List<Comentariodto>()                

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
                var vendedor = await _usuarioRepository.ObtenerInfoUsuario(prod.VendedorId);

                lista.Add(new ProductoConImagendto
                {
                    ProductoId = prod.ProductoId,
                    Nombre = prod.Nombre,
                    Descripcion = prod.Descripcion!,
                    Precio = prod.Precio,
                    Stock = prod.Stock,
                    CategoriaId = (int)prod.CategoriaId!,
                    VendedorId = prod.VendedorId,
                    nombreVendedor = vendedor.Nombre,
                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen.Data) : null,
                    Comentarios = imagen != null ? imagen.Comentarios : new List<Comentariodto>()                

                });
            }
            return lista;


        }
        public async Task<IEnumerable<ProductoConImagendto>> ObtenerProductosPorCategoria(int categoriaID, int usuarioID)
        {
            if (!await _usuarioRepository.ExisteUsuarioAsync(usuarioID))
            {
                throw new UsuarioNotFound();
            }
            var productoPorCategoria = await _productoRepository.ObtenerProductosPorCategoria(categoriaID,usuarioID);
            if (!productoPorCategoria.Any())
            {
                throw new ProductosNotFound();
            }

            var lista = new List<ProductoConImagendto>();

            foreach (var prod in productoPorCategoria)
            {
                var vendedor = await _usuarioRepository.ObtenerInfoUsuario(prod.VendedorId);

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
                    nombreVendedor = vendedor.Nombre,
                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen.Data) : null,
                    Comentarios = imagen != null ? imagen.Comentarios : new List<Comentariodto>()                

                });
            }
            return lista;

            
        }

        public async Task<Producto> EditarProducto(ProductoEditar dto, int id, int usuarioID)
        {
            var producto = new Producto
            {
                ProductoId = id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = dto.Stock,
                VendedorId = usuarioID,
                CategoriaId = dto.CategoriaId
            };

            var productoNuevo = _productoRepository.EditarProducto(producto);
            return await productoNuevo;

        }
        public async Task<Producto> AñadirProducto(Productodto dto, int usuarioID,IFormFile imagen)
        {
            var nuevoProducto = new Producto
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    Precio = dto.Precio,
                    Stock = dto.Stock,
                    CategoriaId = dto.CategoriaId,
                    VendedorId = usuarioID
                };

            var productoNuevo = await _productoRepository.AñadirProducto(nuevoProducto, usuarioID);

            if (imagen != null && imagen.Length > 0)
            {
                using var ms = new MemoryStream();
                await imagen.CopyToAsync(ms);

                var imagenMongo = new ProductoImagen
                {
                    ProductoId = productoNuevo.ProductoId,
                    FileName = imagen.FileName,
                    ContentType = imagen.ContentType,
                    Data = ms.ToArray()
                };

                await _productoImagenRepository.InsertarAsync(imagenMongo);
            }
            return productoNuevo;
        }
        public async Task<bool> EliminarProducto(int productoID)
        {
            bool isActualizado = await _productoRepository.EliminarProducto(productoID);
            return isActualizado;

        }
        public async Task<ProductoConImagendto> ObtenerProducto(int productoID)
        {
            var productoObtenido = await _productoRepository.ObtenerProducto(productoID);

            var vendedor = await _usuarioRepository.ObtenerInfoUsuario(productoObtenido.VendedorId);

            var imagen = (await _productoImagenRepository.ObtenerPorProductoIdAsync(productoObtenido.ProductoId)).FirstOrDefault();
            var productoConImagen = new ProductoConImagendto
            {
                ProductoId = productoObtenido.ProductoId,
                Nombre = productoObtenido.Nombre,
                Descripcion = productoObtenido.Descripcion!,
                Precio = productoObtenido.Precio,
                Stock = productoObtenido.Stock,
                CategoriaId = (int)productoObtenido.CategoriaId!,
                VendedorId = productoObtenido.VendedorId,
                nombreVendedor = vendedor.Nombre,
                ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen.Data) : null,
                Comentarios = imagen != null ? imagen.Comentarios : new List<Comentariodto>()                
            };
            return productoConImagen;
        } 



        
    }
}