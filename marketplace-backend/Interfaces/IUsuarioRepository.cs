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
        bool ExisteUsuario(int usuarioID);
        Persona RegistrarNuevoUsuario(Persona nuevaPersona); 
        Persona ObtenerUsuarioPorEmail(string email);
        UsuarioInfoDto ObtenerInfoUsuario(int usuarioID);

    }
}