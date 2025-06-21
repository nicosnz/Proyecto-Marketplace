using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace marketplace_backend.dtos
{
    [Keyless]
    public class ProductosVendidosUsuario
    {
        public int ProductoID { get; set; }
        public string? nombreProducto { get; set; }
        public string? descripcion { get; set; }
        public int totalVendido { get; set; }
        public decimal totalIngresos { get; set; }
        public int cantidadOrdenes { get; set; }

    }
}