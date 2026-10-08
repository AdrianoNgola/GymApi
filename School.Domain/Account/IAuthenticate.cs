using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Domain.Entities;

namespace School.Domain.Account
{
    public interface IAuthenticate
    {
        string GenerateToken(string? email, int Id, string? perfil);
        Task<User?> GetUserByEmail(string? email);
        Task<bool> UserExists(string? email);

         Task<bool> UserAuthenticate(string? email, string? senha);
    }
}