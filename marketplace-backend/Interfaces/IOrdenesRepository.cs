using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;

namespace marketplace_backend.Interfaces
{
    public interface IOrdenesRepository
    {
        Task<int> InsertarOrden(int compradorId, string direccion, string pais, string ciudad);
        Task MarcarOrdenComoPagada(int ordenId);
        Task AgregarProductoADetalleOrden(int ordenId, int productoId, int cantidad);
        Task<IEnumerable<DetalleComprasUsuario>> ObtenerComprasUsuario(int usuarioID);
        Task<IEnumerable<ProductosVendidosUsuario>> ObtenerProductosVendidosUsuario(int usuarioID);


    }
}