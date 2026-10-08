using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using School.Aplication.Services;
using School.Aplication.Services.Curso;
using School.Aplication.Services.Matricula;
using School.Aplication.Services.Matricula.Interface;
using School.Aplication.Services.Nota;
using School.Aplication.Services.Nota.Interface;
using School.Aplication.Services.Turma;
using School.Aplication.Services.Turma.Interface;
using School.Aplication.Services.User;
using School.Aplication.Services.User.Interface;
using School.Domain.Account;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Infra.Data.Identity;
using School.InfraData.Context;
using School.InfraData.Repositries;

namespace School.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(AplicationDbContext).Assembly.FullName)));

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                        ClockSkew = TimeSpan.Zero // Elimina a tolerância de tempo para expiração do token
                    };
                });

                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<ICursoRepository, CursoRepository>();
                services.AddScoped<IMatriculaRepository, MatriculaRepository>();
                services.AddScoped<INotaRepository, NotaRepository>();
                services.AddScoped<ITurmaRepository, TurmaRepository>();



                services.AddScoped<IUserService, UserService>();
                services.AddScoped<ICursoService, CursoService>();
                services.AddScoped<ITurmaService, TurmaService>();
                services.AddScoped<INotaService, NotaService>();
                services.AddScoped<IMatriculaService, MatriculaService>();
                services.AddScoped<IAuthenticate, AuthenticateService>();



                
            return services;
        }
    }
}