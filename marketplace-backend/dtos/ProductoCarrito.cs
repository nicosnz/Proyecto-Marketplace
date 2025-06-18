using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.Models;

namespace marketplace_backend.dtos
{
    public class ProductoCarrito
    {
        public ProductoConImagendto Producto { get; set; }


        public int Cantidad { get; set; }

        public int total { get; set; }

    }
}