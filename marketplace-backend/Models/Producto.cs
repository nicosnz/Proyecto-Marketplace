using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public int VendedorId { get; set; }

    public int? CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool Activo { get; set; }

    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<DetalleOrden> DetalleOrdens { get; set; } = new List<DetalleOrden>();

    public virtual Persona Vendedor { get; set; } = null!;
}
