using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;
using marketplace_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace marketplace_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registrar")]
        public IActionResult RegistrarUsuario([FromBody] UsuarioRegistrarseDto usuarioDto)
        {
            try
            {
               
                var resultado = _usuarioService.RegistrarNuevoUsuario(usuarioDto);

                var token = _usuarioService.GenerarToken(resultado);

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
            catch (UsuarioDuplicado ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginDto loginDto)
        {
            try
            {
                var usuario = _usuarioService.IniciarSesion(loginDto);
                var token = _usuarioService.GenerarToken(usuario);
                return Ok(new { mensaje = "Ingreso exitoso", token });
            }
            
            catch (UsuarioNotFound ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("infoUsuario")]
        public IActionResult ObtenerInfoUsuario()
        {
            try
            {
                int usuarioID = (int)ObtenerUsuarioIdDesdeToken()!;
                var usuario = _usuarioService.ObtenerInfoUsuario(usuarioID);
                return Ok(usuario);
            }
            catch (ApplicationException ex)
            {
                return Unauthorized(new { mensaje = ex.Message });
            }
        }

        

        private int? ObtenerUsuarioIdDesdeToken()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : (int?)null;
        }
    }
}