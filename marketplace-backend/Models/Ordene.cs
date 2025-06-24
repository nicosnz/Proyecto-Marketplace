using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class Ordene
{
    public int OrdenId { get; set; }

    public int CompradorId { get; set; }

    public DateTime? FechaOrden { get; set; }

    public decimal Total { get; set; }

    public string? Estado { get; set; }

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Pais { get; set; }

    public Persona Comprador { get; set; } = null!;

    public List<DetalleOrden> DetalleOrdens { get; set; } = new List<DetalleOrden>();
}
