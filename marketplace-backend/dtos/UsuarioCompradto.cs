using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marketplace_backend.dtos
{
    public class UsuarioCompradto
    {
        public int PersonaId { get; set; }

        public string Pais { get; set; } = null!;

        public string Ciudad { get; set; } = null!;
        public string Direccion { get; set; } = null!;
    }
    
}