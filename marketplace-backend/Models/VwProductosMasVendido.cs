using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class VwProductosMasVendido
{
    public int ProductoId { get; set; }

    public string ProductoNombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int CategoriaId { get; set; }

    public string CategoriaNombre { get; set; } = null!;

    public int VendedorId { get; set; }

    public string VendedorNombre { get; set; } = null!;

    public int? TotalCantidadVendida { get; set; }

    public decimal? TotalVentas { get; set; }
}
