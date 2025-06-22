using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;
using marketplace_backend.Models;

namespace marketplace_backend.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> ExisteUsuarioAsync(int usuarioID);
        Task<Persona> RegistrarNuevoUsuario(Persona nuevaPersona); 
        Task<Persona?> ObtenerUsuarioPorEmailAsync(string email);
        Task<UsuarioInfodto> ObtenerInfoUsuario(int usuarioID);

    }
}