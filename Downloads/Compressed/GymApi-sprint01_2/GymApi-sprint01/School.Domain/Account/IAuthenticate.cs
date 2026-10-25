using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Domain.Entities;


namespace School.Domain.Account
{
    public interface IAuthenticate
    {
        Task<string> GenerateToken(string? email, string utilizadorId, Guid perfilId);
        Task<Utilizador?> GetUserByEmail(string? email);
        Task<bool> UserExists(string? email);
         Guid GetUtilizadorId();

         Task<bool> UserAuthenticate(string? email, string? senha);
    }
}