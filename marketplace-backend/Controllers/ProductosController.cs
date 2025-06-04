using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<VwProductosCatalogo>>> ObtenerProductosDisponibles()
        {
            var productos = await _productoService.ObtenerProductosDisponiblesAsync();
            return Ok(productos);
        }

        [Authorize]
        [HttpGet("mis-productos")]
        public async Task<ActionResult<IEnumerable<VwProductosCatalogo>>> ObtenerProductosDisponiblesPorUsuario()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); 
                var productos = await _productoService.ObtenerProductosPorUsuarioAsync(userId);
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
        [Authorize]

        [HttpGet("catalogo")]


        public async Task<ActionResult<IEnumerable<VwProductosCatalogo>>> ObtenerProductosMenosUsuario()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); 
                var productos = await _productoService.ObtenerProductosMenosUsuarioAsync(userId);
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

    }
}