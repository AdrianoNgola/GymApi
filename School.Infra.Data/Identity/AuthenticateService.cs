using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using School.Domain.Account;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.InfraData.Context;

namespace School.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AplicationDbContext _Context;
        private readonly IConfiguration _configuration;
         private readonly IPerfilRepository _Perfil;


         private Guid? UtilizadorId
        {
            get
            {
                var userClaim = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "Id");
                return Guid.TryParse(userClaim?.Value, out var userId) ? userId : null;
            }
        }

        public AuthenticateService(AplicationDbContext context, IConfiguration configuration, IPerfilRepository Perfil, IHttpContextAccessor httpContextAccessor)
        {
            _Context = context;
            _configuration = configuration;
            _Perfil = Perfil;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GenerateToken(string? email, string utlizadorId, Guid perfil)
        {

            var Role = await _Perfil.GetByIdAsync(perfil);

            var claims = new[]
            {
                new Claim("Email", email.ToLower()),
                new Claim("Id", utlizadorId),
                new Claim(ClaimTypes.Role, Role.Nome),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var Credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: Credencial
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Utilizador?> GetUserByEmail(string email)
        {
            return await _Context.Utilizador.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UserAuthenticate(string email, string senha)
        {
            var utilizar = await _Context.Utilizador.FirstOrDefaultAsync(u => u.Email == email);
            if (utilizar == null || utilizar.Activo != true)
                return false;
            
            using var hmac = new HMACSHA512(utilizar.PalavraPasseSalt);
            var senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            for (int i = 0; i < senhaHash.Length; i++)
            {
                if (senhaHash[i] != utilizar.PalavraPasseHash[i])
                    return false;
            }
            return true;

         
        }

        public async Task<bool> UserExists(string email)
        {
           return await _Context.Utilizador.AnyAsync(u => u.Email == email);
        }

         public Guid GetUtilizadorId()
        {
            if (UtilizadorId.HasValue)
                return UtilizadorId.Value;

            throw new AuthenticationException("Usuário não identificado.");
        }

    }
}