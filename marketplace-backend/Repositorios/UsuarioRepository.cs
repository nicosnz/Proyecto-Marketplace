using System;
using System.Collections.Generic;
using System.Linq;
using marketplace_backend.dtos;
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

        public bool ExisteUsuario(int usuarioID)
        {
            var usuarioExiste = _context.Personas
                .FromSqlInterpolated($"EXEC dbo.sp_ObtenerUsuario @UsuarioID = {usuarioID}")
                .AsNoTracking()
                .ToList();
            return usuarioExiste.Any();
        }

        public Persona RegistrarNuevoUsuario(Persona nuevaPersona)
        {
            var persona = _context.Personas.FromSqlInterpolated($"EXEC sp_InsertarPersona @Nombre = {nuevaPersona.Nombre}, @Email = {nuevaPersona.Email}, @PasswordHash = {nuevaPersona.PasswordHash}")
                .AsNoTracking()
                .ToList();
            return persona.FirstOrDefault()!;
        }

        public Persona ObtenerUsuarioPorEmail(string email)
        {
            var resultado = _context.Personas
                .FromSqlInterpolated($"EXEC dbo.sp_IniciarSesion @Email = {email}")
                .AsNoTracking()
                .ToList();

            return resultado.FirstOrDefault()!;
        }

        public UsuarioInfoDto ObtenerInfoUsuario(int usuarioID)
        {
            var resultado = _context.Set<UsuarioInfoDto>()
                .FromSqlInterpolated($"EXEC dbo.sp_ObtenerInfoUsuario @usuarioId = {usuarioID}")
                .AsNoTracking()
                .ToList();

            return resultado.FirstOrDefault()!;
        }
    }
}