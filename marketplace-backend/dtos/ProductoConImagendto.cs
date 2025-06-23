using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marketplace_backend.dtos
{
    public class ProductoConImagendto
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
        public int VendedorId { get; set; }

        public string? nombreVendedor { get; set; }
        public string? ImagenBase64 { get; set; }
        public List<Comentariodto> Comentarios { get; set; } = new List<Comentariodto>();

    }
}