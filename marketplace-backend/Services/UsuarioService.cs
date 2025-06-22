using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;
using marketplace_backend.Interfaces;
using marketplace_backend.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;

namespace marketplace_backend.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDataProtector _dataProtector;
        public UsuarioService(IUsuarioRepository usuarioRepository, IDataProtectionProvider dataProtection)
        {
            _usuarioRepository = usuarioRepository;
            _dataProtector = dataProtection.CreateProtector("pwd");
        }
        public async Task<Persona> RegistrarNuevoUsuario(Persona nuevaPersona)
        {
            try
            {
                var textoCifrado = _dataProtector.Protect(nuevaPersona.PasswordHash);
                nuevaPersona.PasswordHash = textoCifrado;
                return await _usuarioRepository.RegistrarNuevoUsuario(nuevaPersona);

            }
            catch (SqlException ex) when (ex.Message.Contains("El email ya existe"))
            {
                throw new ApplicationException("Ya existe un usuario con ese correo.");
            }

        }
        public async Task<Persona> IniciarSesionAsync(string email, string password)
        {
            var persona = await _usuarioRepository.ObtenerUsuarioPorEmailAsync(email);

            if (persona == null)
                throw new ApplicationException("El usuario no existe o está inactivo.");

            var passwordDesencriptada = _dataProtector.Unprotect(persona.PasswordHash);

            if (passwordDesencriptada != password)
                throw new ApplicationException("Credenciales inválidas.");

            return persona;
        }
        public async Task<UsuarioInfodto> ObtenerInfoUsuario(int usuarioID)
        {
            var persona = await _usuarioRepository.ObtenerInfoUsuario(usuarioID);
            return persona;
        }

    }
}