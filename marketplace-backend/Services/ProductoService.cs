using System;
using System.Collections.Generic;
using System.Linq;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;

namespace marketplace_backend.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProductoMongoRepository _productoMongoRepository;

        public ProductoService(IProductoRepository productoRepository, IProductoMongoRepository productoImagenRepository, IUsuarioRepository usuarioRepository)
        {
            _productoRepository = productoRepository;
            _usuarioRepository = usuarioRepository;
            _productoMongoRepository = productoImagenRepository;
        }

        public List<ProductoConImagenDto> ObtenerProductosDisponibles()
        {
            var productos = _productoRepository.ObtenerTodosProductosDisponibles();
            var lista = new List<ProductoConImagenDto>();
            foreach (var prod in productos)
            {
                var vendedor = _usuarioRepository.ObtenerInfoUsuario(prod.VendedorId);
                var imagen = _productoMongoRepository.ObtenerProductoPorId(prod.ProductoId).FirstOrDefault();

                lista.Add(new ProductoConImagenDto
                {
                    ProductoId = prod.ProductoId,
                    Nombre = prod.ProductoNombre,
                    Descripcion = prod.Descripcion!,
                    Precio = prod.Precio,
                    Stock = prod.Stock,
                    CategoriaId = prod.CategoriaId,
                    VendedorId = prod.VendedorId,
                    nombreVendedor = vendedor.Nombre,
                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen.Imagen) : null,
                    Comentarios = imagen != null ? imagen.Comentarios : new List<ComentarioDto>()
                });
            }
            return lista;
        }

        public List<Categoria> ObtenerCategorias()
        {
            return _productoRepository.ObtenerCategorias();
        }

        public List<ProductoConImagenDto> ObtenerProductosPorUsuario(int usuarioID)
        {
            if (!_usuarioRepository.ExisteUsuario(usuarioID))
            {
                throw new UsuarioNotFound();
            }
            var productosUsuario = _productoRepository.ObtenerProductosPorUsuario(usuarioID);
            if (!productosUsuario.Any())
            {
                throw new ProductosNotFound();
            }
            var lista = new List<ProductoConImagenDto>();

            foreach (var prod in productosUsuario)
            {
                var imagen = _productoMongoRepository.ObtenerProductoPorId(prod.ProductoId).FirstOrDefault();

                lista.Add(new ProductoConImagenDto
                {
                    ProductoId = prod.ProductoId,
                    Nombre = prod.Nombre,
                    Descripcion = prod.Descripcion!,
                    Precio = prod.Precio,
                    Stock = prod.Stock,
                    CategoriaId = (int)prod.CategoriaId!,
                    VendedorId = prod.VendedorId,
                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen.Imagen) : null,
                    Comentarios = imagen != null ? imagen.Comentarios : new List<ComentarioDto>()
                });
            }
            return lista;
        }

        public List<ProductoConImagenDto> ObtenerProductosMenosUsuario(int usuarioID)
        {
            if (!_usuarioRepository.ExisteUsuario(usuarioID))
            {
                throw new UsuarioNotFound();
            }
            var productosNoUsuario = _productoRepository.ObtenerProductosMenosUsuario(usuarioID);
            if (!productosNoUsuario.Any())
            {
                throw new ProductosNotFound();
            }

            var lista = new List<ProductoConImagenDto>();

            foreach (var prod in productosNoUsuario)
            {
                var imagen = _productoMongoRepository.ObtenerProductoPorId(prod.ProductoId).FirstOrDefault();
                var vendedor = _usuarioRepository.ObtenerInfoUsuario(prod.VendedorId);

                lista.Add(new ProductoConImagenDto
                {
                    ProductoId = prod.ProductoId,
                    Nombre = prod.Nombre,
                    Descripcion = prod.Descripcion!,
                    Precio = prod.Precio,
                    Stock = prod.Stock,
                    CategoriaId = (int)prod.CategoriaId!,
                    VendedorId = prod.VendedorId,
                    nombreVendedor = vendedor.Nombre,
                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen.Imagen) : null,
                    Comentarios = imagen != null ? imagen.Comentarios : new List<ComentarioDto>()
                });
            }
            return lista;
        }

        public List<ProductoConImagenDto> ObtenerProductosPorCategoria(int categoriaID, int usuarioID)
        {
            if (!_usuarioRepository.ExisteUsuario(usuarioID))
            {
                throw new UsuarioNotFound();
            }
            var productoPorCategoria = _productoRepository.ObtenerProductosPorCategoria(categoriaID, usuarioID);
            if (!productoPorCategoria.Any())
            {
                throw new ProductosNotFound();
            }

            var lista = new List<ProductoConImagenDto>();

            foreach (var prod in productoPorCategoria)
            {
                var vendedor = _usuarioRepository.ObtenerInfoUsuario(prod.VendedorId);
                var imagen = _productoMongoRepository.ObtenerProductoPorId(prod.ProductoId).FirstOrDefault();

                lista.Add(new ProductoConImagenDto
                {
                    ProductoId = prod.ProductoId,
                    Nombre = prod.Nombre,
                    Descripcion = prod.Descripcion!,
                    Precio = prod.Precio,
                    Stock = prod.Stock,
                    CategoriaId = (int)prod.CategoriaId!,
                    VendedorId = prod.VendedorId,
                    nombreVendedor = vendedor.Nombre,
                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen.Imagen) : null,
                    Comentarios = imagen != null ? imagen.Comentarios : new List<ComentarioDto>()
                });
            }
            return lista;
        }

        public Producto EditarProducto(ProductoEditar productoAEditar, int productoId, int usuarioID)
        {
            var producto = new Producto
            {
                ProductoId = productoId,
                Nombre = productoAEditar.Nombre,
                Descripcion = productoAEditar.Descripcion,
                Precio = productoAEditar.Precio,
                Stock = productoAEditar.Stock,
                VendedorId = usuarioID,
                CategoriaId = productoAEditar.CategoriaId
            };

            var productoNuevo = _productoRepository.EditarProducto(producto);
            return productoNuevo;
        }

        public ProductoConImagenDto AñadirProducto(ProductoAñadirDto productoRequest, int usuarioID, IFormFile imagen)
        {
            var nuevoProducto = new Producto
            {
                Nombre = productoRequest.Nombre,
                Descripcion = productoRequest.Descripcion,
                Precio = productoRequest.Precio,
                Stock = productoRequest.Stock,
                CategoriaId = productoRequest.CategoriaId,
                VendedorId = usuarioID
            };

            var productoNuevo = _productoRepository.AñadirProducto(nuevoProducto, usuarioID);

            if (imagen != null && imagen.Length > 0)
            {
                using var ms = new MemoryStream();
                imagen.CopyTo(ms);

                var imagenMongo = new ProductoImagenConComentarios
                {
                    ProductoId = productoNuevo.ProductoId,
                    NombreArchivo = imagen.FileName,
                    TipoContenido = imagen.ContentType,
                    Imagen = ms.ToArray()
                };

                _productoMongoRepository.Insertar(imagenMongo);
            }
            var vendedor = _usuarioRepository.ObtenerInfoUsuario(productoNuevo.VendedorId);
            var imagenProducto = _productoMongoRepository.ObtenerProductoPorId(productoNuevo.ProductoId).FirstOrDefault();


            var productoConImagen = new ProductoConImagenDto
            {
                ProductoId = productoNuevo.ProductoId,
                Nombre = productoNuevo.Nombre,
                Descripcion = productoNuevo.Descripcion!,
                Precio = productoNuevo.Precio,
                Stock = productoNuevo.Stock,
                CategoriaId = (int)productoNuevo.CategoriaId!,
                VendedorId = productoNuevo.VendedorId,
                nombreVendedor = vendedor.Nombre,
                ImagenBase64 = imagenProducto != null ? Convert.ToBase64String(imagenProducto.Imagen) : null,
                Comentarios = imagenProducto != null ? imagenProducto.Comentarios : new List<ComentarioDto>()
            };
            return productoConImagen;
        }

        public bool EliminarProducto(int productoID)
        {
            bool isActualizado = _productoRepository.EliminarProducto(productoID);
            return isActualizado;
        }

        public ProductoConImagenDto ObtenerProducto(int productoID)
        {
            var productoObtenido = _productoRepository.ObtenerProducto(productoID);

            var vendedor = _usuarioRepository.ObtenerInfoUsuario(productoObtenido.VendedorId);

            var imagen = _productoMongoRepository.ObtenerProductoPorId(productoObtenido.ProductoId).FirstOrDefault();
            var productoConImagen = new ProductoConImagenDto
            {
                ProductoId = productoObtenido.ProductoId,
                Nombre = productoObtenido.Nombre,
                Descripcion = productoObtenido.Descripcion!,
                Precio = productoObtenido.Precio,
                Stock = productoObtenido.Stock,
                CategoriaId = (int)productoObtenido.CategoriaId!,
                VendedorId = productoObtenido.VendedorId,
                nombreVendedor = vendedor.Nombre,
                ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen.Imagen) : null,
                Comentarios = imagen != null ? imagen.Comentarios : new List<ComentarioDto>()
            };
            return productoConImagen;
        }
         public void AñadirAFavoritos(int productoId,int usuarioId)
        {
            
            _productoRepository.AñadirAFavoritos(productoId,usuarioId );
        }

        public void QuitarDeFavoritos(int productoId,int usuarioId)
        {
            
            _productoRepository.QuitarDeFavoritos(productoId,usuarioId );
        }

    }
}