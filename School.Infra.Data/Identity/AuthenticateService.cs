using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using School.Domain.Account;
using School.Domain.Entities;
using School.InfraData.Context;

namespace School.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly AplicationDbContext _Context;
        private readonly IConfiguration _configuration;

        public AuthenticateService(AplicationDbContext context, IConfiguration configuration)
        {
            _Context = context;
            _configuration = configuration;
        }
        public string GenerateToken(string email, int Id, string perfil)
        {
            var claims = new[]
            {
                new Claim("Email", email.ToLower()),
                new Claim("Id", Id.ToString()),
                new Claim(ClaimTypes.Role, perfil),
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

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _Context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UserAuthenticate(string email, string senha)
        {
            var user = await _Context.User.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || user.Excluido != false)
                return false;
            
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            for (int i = 0; i < senhaHash.Length; i++)
            {
                if (senhaHash[i] != user.PasswordHash[i])
                    return false;
            }
            return true;

         
        }

        public Task<bool> UserExists(string email)
        {
           return _Context.User.AnyAsync(u => u.Email == email);
        }
    }
}