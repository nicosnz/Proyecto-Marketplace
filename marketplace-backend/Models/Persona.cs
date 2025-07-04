﻿using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class Persona
{
    public int PersonaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public bool Activo { get; set; }

    public  List<Ordene> Ordenes { get; set; } = new List<Ordene>();

    public  List<Producto> Productos { get; set; } = new List<Producto>();
}
