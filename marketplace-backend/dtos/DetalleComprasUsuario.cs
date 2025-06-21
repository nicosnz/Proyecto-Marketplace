using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace marketplace_backend.dtos
{
    [Keyless]
    public class DetalleComprasUsuario
    {
        public int OrdenId { get; set; }
        public DateTime? FechaOrden { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public int productoId { get; set; }
        public string? nombreProducto { get; set; }
        public string? descripcion { get; set; }
        public decimal precioProducto { get; set; }

    }
}