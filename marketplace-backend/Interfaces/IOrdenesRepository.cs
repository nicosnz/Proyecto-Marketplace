using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;

namespace marketplace_backend.Interfaces
{
    public interface IOrdenesRepository
    {
        int InsertarOrden(int compradorId, string direccion, string pais, string ciudad);
        void MarcarOrdenComoPagada(int ordenId);
        void AgregarProductoADetalleOrden(int ordenId, int productoId, int cantidad);
        List<DetalleComprasUsuario> ObtenerComprasUsuario(int usuarioID);
        List<ProductosVendidosUsuario> ObtenerProductosVendidosUsuario(int usuarioID);


    }
}