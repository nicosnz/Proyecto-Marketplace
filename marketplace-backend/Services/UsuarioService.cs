using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace marketplace_backend.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDataProtector _dataProtector;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuarioRepository usuarioRepository, IDataProtectionProvider dataProtection, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _dataProtector = dataProtection.CreateProtector("pwd");
            _configuration = configuration;
        }
        public Persona RegistrarNuevoUsuario(Persona nuevaPersona)
        {
            var persona = _usuarioRepository.ObtenerUsuarioPorEmail(nuevaPersona.Email);

            if (persona != null)
            {
                throw new UsuarioDuplicado();
            }
            var textoCifrado = _dataProtector.Protect(nuevaPersona.PasswordHash);
            nuevaPersona.PasswordHash = textoCifrado;
            return _usuarioRepository.RegistrarNuevoUsuario(nuevaPersona);
            
            
        }

        public Persona IniciarSesion(string email, string password)
        {
            var persona = _usuarioRepository.ObtenerUsuarioPorEmail(email);

            if (persona == null)
                throw new UsuarioNotFound();

            var passwordDesencriptada = _dataProtector.Unprotect(persona.PasswordHash);

            if (passwordDesencriptada != password)
                throw new UsuarioNotFound();

            return persona;
        }

        public UsuarioInfoDto ObtenerInfoUsuario(int usuarioID)
        {
            var persona = _usuarioRepository.ObtenerInfoUsuario(usuarioID);
            return persona;
        }
        public string GenerarToken(Persona usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.PersonaId.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.Nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:securityKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWTSettings:ValidIssuer"],
                audience: _configuration["JWTSettings:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}