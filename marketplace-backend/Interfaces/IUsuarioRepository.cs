using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marketplace_backend.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> ExisteUsuarioAsync(int usuarioID);
    }
}