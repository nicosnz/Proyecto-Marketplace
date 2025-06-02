using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace marketplace_backend.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MarketplaceDbContext _context;
        public UsuarioRepository(MarketplaceDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ExisteUsuarioAsync(int usuarioID)
        {
            var usuarios = await _context.Personas
                .FromSqlInterpolated($"EXEC dbo.sp_ObtenerUsuario @UsuarioID = {usuarioID}")
                .AsNoTracking()
                .ToListAsync();
            return usuarios.Any();
        }
    }
}