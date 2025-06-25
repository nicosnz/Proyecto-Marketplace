using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace marketplace_backend.dtos
{
    [Keyless]
    public class UsuarioInfoDto
    {

        public int PersonaId { get; set; }

        public string Nombre { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime? FechaRegistro { get; set; }


    }
}