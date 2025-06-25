using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marketplace_backend.Services
{
    public class MarketplaceException : Exception
    {
        public MarketplaceException(string mensaje) : base(mensaje) { }
    }
    public class UsuarioNotFound : MarketplaceException
    {
        public UsuarioNotFound() : base($"El email o la contraseña son invalidos.") { }

    }
    public class UsuarioDuplicado : MarketplaceException
    {
        public UsuarioDuplicado() : base($"El email ya esta en uso.") { }

    }
    public class ProductosNotFound : MarketplaceException
    {
        public ProductosNotFound() : base($"No tienes productos") { }

    }
    public class ProductoSinStock : MarketplaceException
    {
        public ProductoSinStock(string producto) : base($"No hay suficientes unidades de {producto}."){}
    }
}