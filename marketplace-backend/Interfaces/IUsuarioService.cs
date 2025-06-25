using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.dtos;
using marketplace_backend.Models;

namespace marketplace_backend.Interfaces
{
    public interface IUsuarioService
    {
        Persona RegistrarNuevoUsuario(Persona nuevaPersona);
        Persona IniciarSesion(string email, string password);
        UsuarioInfoDto ObtenerInfoUsuario(int usuarioID);
        string GenerarToken(Persona usuario);


        
    }
}