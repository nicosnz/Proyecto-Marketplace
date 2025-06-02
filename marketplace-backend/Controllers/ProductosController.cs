using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;
using marketplace_backend.Services;
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
        [HttpGet("{usuarioID}")]
        public async Task<ActionResult<IEnumerable<VwProductosCatalogo>>> ObtenerProductosDisponiblesPorUsuario(int usuarioID)
        {
            try
            {

                var productos = await _productoService.ObtenerProductosPorUsuarioAsync(usuarioID);
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

        [HttpGet("noUsuario/{usuarioID}")]


        public async Task<ActionResult<IEnumerable<VwProductosCatalogo>>> ObtenerProductosMenosUsuario(int usuarioID)
        {
            try
            {

                var productos = await _productoService.ObtenerProductosMenosUsuarioAsync(usuarioID);
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