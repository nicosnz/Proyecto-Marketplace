using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marketplace_backend.dtos
{
    public class OrdenDto
    {
        public ProductoCarrito[] ProductoCarrito { get; set; }
        public UsuarioCompradto Persona { get; set; }
    }
}