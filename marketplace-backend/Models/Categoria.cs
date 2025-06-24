using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    public List<Producto> Productos { get; set; } = new List<Producto>();
}
