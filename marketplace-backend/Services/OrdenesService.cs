using System;
using System.Collections.Generic;
using System.Linq;
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
        public int InsertarOrden(int compradorId, string direccion, string pais, string ciudad)
        {
            var ordenId = _ordenesRepository.InsertarOrden(compradorId, direccion, pais, ciudad);
            return ordenId;
        }
        public void MarcarOrdenComoPagada(int ordenId)
        {
            _ordenesRepository.MarcarOrdenComoPagada(ordenId);
        }
        public void AgregarProductoADetalleOrden(int ordenId, ProductoCarrito[] productoCarritos)
        {
            foreach (var item in productoCarritos)
            {
                _ordenesRepository.AgregarProductoADetalleOrden(ordenId, item.Producto.ProductoId, item.Cantidad);
            }
        }
        public List<DetalleComprasUsuario> ObtenerComprasUsuario(int usuarioID)
        {
            return _ordenesRepository.ObtenerComprasUsuario(usuarioID);
        }
        public List<ProductosVendidosUsuario> ObtenerProductosVendidosUsuario(int usuarioID)
        {
            return _ordenesRepository.ObtenerProductosVendidosUsuario(usuarioID);
        }
    }
}