using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoConImagendto>>> ObtenerProductosDisponibles()
        {
            var productos = await _productoService.ObtenerProductosDisponibles();
            return Ok(productos);
        }
        [HttpGet("categorias")]
        public async Task<ActionResult<IEnumerable<Categoria>>> ObtenerCategorias()
        {
            var categorias = await _productoService.ObtenerCategorias();
            return Ok(categorias);
        }


        [Authorize]
        [HttpGet("mis-productos")]
        public async Task<ActionResult<IEnumerable<ProductoConImagendto>>> ObtenerProductosDisponiblesPorUsuario()
        {
            try
            {
                var userId = ObtenerUsuarioIdDesdeToken();
                if (userId == null)
                    return Unauthorized(new { mensaje = "Token inválido" });

                var productos = await _productoService.ObtenerProductosPorUsuario((int)userId);
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
                Console.WriteLine("error en productos1");
                return BadRequest(new { mensaje = ex.Message });

            }
        }
        [Authorize]

        [HttpGet("catalogo")]


        public async Task<ActionResult<IEnumerable<ProductoConImagendto>>> ObtenerProductosMenosUsuario()
        {
            try
            {
                var userId = ObtenerUsuarioIdDesdeToken();
                if (userId == null)
                    return Unauthorized(new { mensaje = "Token inválido" });
                var productos = await _productoService.ObtenerProductosMenosUsuario((int)userId);
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
        public async Task<ActionResult> AñadirProducto([FromForm] Productodto dto, [FromForm] IFormFile imagen )
        {
            try
            {
                var userId = ObtenerUsuarioIdDesdeToken();
                if (userId == null)
                    return Unauthorized(new { mensaje = "Token inválido" });

                var productoCreado = await _productoService.AñadirProducto(dto, userId.Value,imagen);
                
                return Ok(productoCreado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        [HttpPut("editar/{id}")]
        [Authorize]
        public async Task<ActionResult> EditarProducto([FromBody] ProductoEditar dto, [FromRoute]int id)
        {
            try
            {
                var userId = ObtenerUsuarioIdDesdeToken();
                if (userId == null)
                    return Unauthorized(new { mensaje = "Token inválido" });

               
                var productoEditado = await _productoService.EditarProducto(dto,id,(int)userId);
                return Ok(productoEditado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id}")]
        [Authorize]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            bool eliminado = await _productoService.EliminarProducto(id);
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
        [Authorize]
        public async Task<IActionResult> ObtenerProducto(int id)
        {
            var producto = await _productoService.ObtenerProducto(id);
            if (producto != null)
            {
                return Ok(producto);
            }
            else
            {
                return NotFound($"No se encontró el producto con id {id}.");
            }
        }


        private int? ObtenerUsuarioIdDesdeToken()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : (int?)null;
        }


    }
}