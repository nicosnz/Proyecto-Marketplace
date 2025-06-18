using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        public async Task<ActionResult> AñadirOrden(OrdenDto orden)
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
                var ordenId = await _ordenesService.InsertarOrden(idUsuario, orden.Persona.Direccion, orden.Persona.Pais,orden.Persona.Ciudad);
                await _ordenesService.AgregarProductoADetalleOrden(ordenId, orden.ProductoCarrito);
                await _ordenesService.MarcarOrdenComoPagada(ordenId);
                return Ok();

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("error en ordenes");
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