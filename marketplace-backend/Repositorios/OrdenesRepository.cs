using System;
using System.Collections.Generic;
using System.Linq;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace marketplace_backend.Repositorios
{
    public class OrdenesRepository : IOrdenesRepository
    {
        private readonly MarketplaceDbContext _context;

        public OrdenesRepository(MarketplaceDbContext context)
        {
            _context = context;
        }

        public int InsertarOrden(int compradorId, string direccion, string pais, string ciudad)
        {
            var ordenId = _context.Database
               .SqlQuery<OrdenIdResultado>($"EXEC sp_InsertarOrden @CompradorID={compradorId}, @Direccion={direccion}, @Ciudad={ciudad}, @Pais={pais}")
               .ToList();
            return ordenId.First().OrdenId;
        }

        public void MarcarOrdenComoPagada(int ordenId)
        {
            _context.Database.ExecuteSqlInterpolated($"EXEC sp_MarcarOrdenComoPagada @OrdenID={ordenId}");
        }

        public void AgregarProductoADetalleOrden(int ordenId, int productoId, int cantidad)
        {
            _context.Database.ExecuteSqlInterpolated($"EXEC sp_AgregarProductoADetalleOrden @OrdenID={ordenId}, @ProductoID={productoId}, @Cantidad={cantidad}");
        }

        public List<DetalleComprasUsuario> ObtenerComprasUsuario(int usuarioID)
        {
            return _context.Set<DetalleComprasUsuario>()
                .FromSqlInterpolated($"EXEC ObtenerDetallesComprasUsuario @UsuarioId = {usuarioID}")
                .ToList();
        }

        public List<ProductosVendidosUsuario> ObtenerProductosVendidosUsuario(int usuarioID)
        {
            return _context.Set<ProductosVendidosUsuario>()
                .FromSqlInterpolated($"EXEC ObtenerProductosVendidosPorUsuario @UsuarioId = {usuarioID}")
                .ToList();
        }
    }
}