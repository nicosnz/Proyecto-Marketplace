using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace marketplace_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration;

        public UsuarioController(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioRegistraseDto usuarioDto)
        {
            try
            {
                var nuevoUsuario = new Persona
                {
                    Nombre = usuarioDto.Nombre,
                    Email = usuarioDto.Email,
                    PasswordHash = usuarioDto.PasswordHash
                };

                var resultado = await _usuarioService.RegistrarNuevoUsuario(nuevoUsuario);

                var token = GenerarToken(resultado); 

                return Ok(new
                {
                    mensaje = "Usuario registrado exitosamente",
                    token,
                    usuario = new
                    {
                        id = resultado.PersonaId,
                        nombre = resultado.Nombre,
                        email = resultado.Email
                    }
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto loginDto)
        {
            try
            {
                var usuario = await _usuarioService.IniciarSesionAsync(loginDto.Email, loginDto.PasswordHash);
                var token = GenerarToken(usuario); 
                return Ok(new { token });
            }
            catch (ApplicationException ex)
            {
                return Unauthorized(new { mensaje = ex.Message });
            }
        }
        private string GenerarToken(Persona usuario)
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