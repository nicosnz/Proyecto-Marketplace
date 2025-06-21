using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;

namespace marketplace_backend.Services
{
    public class OrdenesService : IOrdenesService
    {
        private readonly IOrdenesRepository _ordenesRepository;
        public OrdenesService(IOrdenesRepository ordenesRepository)
        {
            _ordenesRepository = ordenesRepository;
        }
        public async Task<int> InsertarOrden(int compradorId, string direccion, string pais, string ciudad)
        {
            var ordenId = await _ordenesRepository.InsertarOrden(compradorId, direccion, pais, ciudad);
            return ordenId;
        }
        public async Task MarcarOrdenComoPagada(int ordenId)
        {
            await _ordenesRepository.MarcarOrdenComoPagada(ordenId);
        }
        public async Task AgregarProductoADetalleOrden(int ordenId, ProductoCarrito[] productoCarritos)
        {
            foreach (var item in productoCarritos)
            {
                await _ordenesRepository.AgregarProductoADetalleOrden(ordenId, item.Producto.ProductoId, item.Cantidad);
            }
        }
        public async Task<IEnumerable<DetalleComprasUsuario>> ObtenerComprasUsuario(int usuarioID)
        {
            return await _ordenesRepository.ObtenerComprasUsuario(usuarioID);
        }
        public async Task<IEnumerable<ProductosVendidosUsuario>> ObtenerProductosVendidosUsuario(int usuarioID)
        {
            return await _ordenesRepository.ObtenerProductosVendidosUsuario(usuarioID);
        }

    }
}