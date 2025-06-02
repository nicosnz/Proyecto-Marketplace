using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class VwResumenVentasVendedor
{
    public int VendedorId { get; set; }

    public string VendedorNombre { get; set; } = null!;

    public int? CantidadOrdenes { get; set; }

    public int? TotalProductosVendidos { get; set; }

    public decimal? TotalVentas { get; set; }
}
