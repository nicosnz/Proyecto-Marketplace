using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class VwOrdenesDetalleBasico
{
    public int OrdenId { get; set; }

    public DateTime? FechaOrden { get; set; }

    public decimal Total { get; set; }

    public string? Estado { get; set; }

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? EstadoEnvio { get; set; }

    public string? Pais { get; set; }

    public int CompradorId { get; set; }

    public string CompradorNombre { get; set; } = null!;

    public string CompradorEmail { get; set; } = null!;
}
