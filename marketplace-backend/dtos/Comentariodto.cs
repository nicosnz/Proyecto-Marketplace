using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marketplace_backend.dtos
{
    public class Comentariodto
    {
        public int usuarioId { get; set; }

        public string nombreUsuario { get; set; } = null!;

        public string texto { get; set; } = null!;
    }
}