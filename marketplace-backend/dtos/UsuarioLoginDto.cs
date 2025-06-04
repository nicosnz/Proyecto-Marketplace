using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marketplace_backend.dtos
{
    public class UsuarioLoginDto
    {
        public int PersonaId { get; set; }

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;
    }
}