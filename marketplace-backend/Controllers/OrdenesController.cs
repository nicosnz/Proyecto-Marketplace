using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_backend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdenesService _ordenesService;
        public OrdenesController(IOrdenesService ordenesService)
        {
            _ordenesService = ordenesService;
        }

        [HttpPost("añadir")]
        [Authorize]
        public ActionResult AñadirOrden(OrdenDto orden)
        {
            try
            {
                foreach (var item in orden.ProductoCarrito)
                {
                    Console.WriteLine(item.Producto.Nombre);
                    Console.WriteLine(item.Cantidad);
                }
                int idUsuario = (int)ObtenerUsuarioIdDesdeToken()!;
                if (idUsuario == null)
                    return Unauthorized(new { mensaje = "Token inválido" });
                var ordenId = _ordenesService.InsertarOrden(idUsuario, orden.Persona.Direccion, orden.Persona.Pais, orden.Persona.Ciudad);
                _ordenesService.AgregarProductoADetalleOrden(ordenId, orden.ProductoCarrito);
                _ordenesService.MarcarOrdenComoPagada(ordenId);
                return Ok();

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("error en ordenes");
                Console.WriteLine(ex.ToString()); // Esto mostrará el stacktrace completo
                return BadRequest(new { mensaje = ex.Message });

            }
        }

        [HttpGet("mis-compras")]
        [Authorize]
        public ActionResult<List<DetalleComprasUsuario>> ObtenerComprasUsuario()
        {
            try
            {
                int idUsuario = (int)ObtenerUsuarioIdDesdeToken()!;
                if (idUsuario == null)
                    return Unauthorized(new { mensaje = "Token inválido" });
                var comprasUsuario = _ordenesService.ObtenerComprasUsuario(idUsuario);
                return Ok(comprasUsuario);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("error en ordenes");
                Console.WriteLine(ex.ToString()); // Esto mostrará el stacktrace completo
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("mis-ventas")]
        [Authorize]
        public ActionResult<List<ProductosVendidosUsuario>> ObtenerProductosVendidosUsuario()
        {
            try
            {
                int idUsuario = (int)ObtenerUsuarioIdDesdeToken()!;
                if (idUsuario == null)
                    return Unauthorized(new { mensaje = "Token inválido" });
                var ventasUsuario = _ordenesService.ObtenerProductosVendidosUsuario(idUsuario);
                return Ok(ventasUsuario);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("error en ordenes");
                Console.WriteLine(ex.ToString()); // Esto mostrará el stacktrace completo
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        private int? ObtenerUsuarioIdDesdeToken()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : (int?)null;
        }
    }
}