﻿using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class DetalleOrden
{
    public int DetalleId { get; set; }

    public int OrdenId { get; set; }

    public int ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public Ordene Orden { get; set; } = null!;

    public Producto Producto { get; set; } = null!;
}
