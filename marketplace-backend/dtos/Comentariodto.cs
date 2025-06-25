using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marketplace_backend.dtos
{
    public class ComentarioDto
    {
        public int UsuarioId { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string Texto { get; set; } = null!;
    }
}