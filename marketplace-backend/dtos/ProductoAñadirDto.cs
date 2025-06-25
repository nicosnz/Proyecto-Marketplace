using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marketplace_backend.dtos
{
    public class ProductoAñadirDto
    {
        public int ProductoId { get; set; }
        public int? CategoriaId { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public bool Activo { get; set; }

    
    }
}