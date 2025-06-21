using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;

namespace marketplace_backend.Interfaces
{
    public interface IOrdenesService
    {
        Task<int> InsertarOrden(int compradorId, string direccion, string pais, string ciudad);
        Task MarcarOrdenComoPagada(int ordenId);

        Task AgregarProductoADetalleOrden(int ordenId, ProductoCarrito[] productoCarritos);
        Task<IEnumerable<DetalleComprasUsuario>> ObtenerComprasUsuario(int usuarioID);
        Task<IEnumerable<ProductosVendidosUsuario>> ObtenerProductosVendidosUsuario(int usuarioID);

    }
}