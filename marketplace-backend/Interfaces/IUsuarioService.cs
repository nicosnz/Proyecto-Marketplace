using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_backend.Models;

namespace marketplace_backend.Interfaces
{
    public interface IUsuarioService
    {
        Task<Persona> RegistrarNuevoUsuario(Persona nuevaPersona);
        Task<Persona> IniciarSesionAsync(string email, string password);


        
    }
}