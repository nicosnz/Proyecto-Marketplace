using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class VwOrdenDetalleProducto
{
    public int OrdenId { get; set; }

    public int DetalleId { get; set; }

    public int ProductoId { get; set; }

    public string ProductoNombre { get; set; } = null!;

    public string? ProductoDescripcion { get; set; }

    public int VendedorId { get; set; }

    public string VendedorNombre { get; set; } = null!;

    public int? Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }
}
