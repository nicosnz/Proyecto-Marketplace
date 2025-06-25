using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;
using marketplace_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly IProductoMongoService _productoMongoService ;

        public ProductosController(IProductoService productoService, IProductoMongoService productoMongoService)
        {
            _productoService = productoService;
            _productoMongoService = productoMongoService;
        }

        [HttpGet]
        public ActionResult<List<ProductoConImagenDto>> ObtenerProductosDisponibles()
        {
            var productos = _productoService.ObtenerProductosDisponibles();
            return Ok(productos);
        }

        [HttpGet("categorias")]
        public ActionResult<List<Categoria>> ObtenerCategorias()
        {
            var categorias = _productoService.ObtenerCategorias();
            return Ok(categorias);
        }

        [Authorize]
        [HttpGet("mis-productos")]
        public ActionResult<List<ProductoConImagenDto>> ObtenerProductosDisponiblesPorUsuario()
        {
            try
            {
                var userId = ObtenerUsuarioIdDesdeToken();
                if (userId == null)
                    return Unauthorized(new { mensaje = "Token inválido" });

                var productos = _productoService.ObtenerProductosPorUsuario((int)userId);
                return Ok(productos);
            }
            catch (ProductosNotFound ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (UsuarioNotFound ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("catalogo")]
        public ActionResult<List<ProductoConImagenDto>> ObtenerProductosMenosUsuario()
        {
            try
            {
                var userId = ObtenerUsuarioIdDesdeToken();
                if (userId == null)
                    return Unauthorized(new { mensaje = "Token inválido" });
                var productos = _productoService.ObtenerProductosMenosUsuario((int)userId);
                return Ok(productos);
            }
            catch (ProductosNotFound ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (UsuarioNotFound ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpGet("categoria/{id}")]
        [Authorize]
        public ActionResult<List<ProductoConImagenDto>> ObtenerProductosPorCategoria(int id)
        {
            try
            {
                var userId = ObtenerUsuarioIdDesdeToken();
                if (userId == null)
                    return Unauthorized(new { mensaje = "Token inválido" });
                var productos = _productoService.ObtenerProductosPorCategoria(id, (int)userId);
                foreach (var item in productos)
                {
                    Console.WriteLine(item.Nombre);
                }
                return Ok(productos);
            }
            catch (ProductosNotFound ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (UsuarioNotFound ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpPost("añadir")]
        [Authorize]
        public ActionResult AñadirProducto([FromForm] ProductoAñadirDto productoNuevo, [FromForm] IFormFile imagen)
        {
            try
            {
                var userId = ObtenerUsuarioIdDesdeToken();
                if (userId == null)
                    return Unauthorized(new { mensaje = "Token inválido" });

                var productoCreado = _productoService.AñadirProducto(productoNuevo, userId.Value, imagen);
                return Ok(productoCreado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("editar/{id}")]
        [Authorize]
        public ActionResult EditarProducto([FromBody] ProductoEditar productoAEditar, [FromRoute] int id)
        {
            try
            {
                var userId = ObtenerUsuarioIdDesdeToken();
                if (userId == null)
                    return Unauthorized(new { mensaje = "Token inválido" });

                var productoEditado = _productoService.EditarProducto(productoAEditar, id, (int)userId);
                return Ok(productoEditado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id}")]
        [Authorize]
        public IActionResult EliminarProducto(int id)
        {
            bool eliminado = _productoService.EliminarProducto(id);
            if (eliminado)
            {
                return NoContent();
            }
            else
            {
                return NotFound($"No se encontró el producto con id {id} o no pudo eliminarse.");
            }
        }

        [HttpGet("producto/{id}")]
        public IActionResult ObtenerProducto(int id)
        {
            var producto = _productoService.ObtenerProducto(id);
            if (producto != null)
            {
                return Ok(producto);
            }
            else
            {
                return NotFound($"No se encontró el producto con id {id}.");
            }
        }

        [HttpPost("producto/añadirComentario/{id}")]
        public IActionResult AñadirComentario(int id, ComentarioDto comentario)
        {
            var userId = (int)ObtenerUsuarioIdDesdeToken()!;
            _productoMongoService.AgregarComentario(id, comentario, userId);

            return Ok();
        }

        private int? ObtenerUsuarioIdDesdeToken()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : (int?)null;
        }
    }
}