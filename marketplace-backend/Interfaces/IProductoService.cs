using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.Models;

namespace marketplace_backend.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<VwProductosCatalogo>> ObtenerProductosDisponiblesAsync();
        Task<IEnumerable<Producto>> ObtenerProductosPorUsuarioAsync(int usuarioID);
        Task<IEnumerable<Producto>> ObtenerProductosMenosUsuarioAsync(int usuarioID);
        Task<Producto> EditarProducto(Producto producto);
        Task<Producto> AñadirProducto(Producto producto, int usuarioID);


    }
}