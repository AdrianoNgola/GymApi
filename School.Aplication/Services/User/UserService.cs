using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using School.Aplication.DTOs.User;
using School.Aplication.Services.User.Interface;
using School.Domain.Interfaces;
using School.Domain.Pagination;

namespace School.Aplication.Services.User
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
             _userRepository = userRepository;
        }
        public async Task<UserGetDTO> AddAsync(UserPostDTO userPostDTO)
        {
            try
            {
                var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userPostDTO.Senha ?? ""));
                byte[] passwordSalt = hmac.Key;

                var user = new School.Domain.Entities.User
                {
                    Nome = userPostDTO.Nome,
                    Email = userPostDTO.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Perfil = userPostDTO.Perfil
                };

                var NovoUser = await _userRepository.AddAsync(user);
                return new UserGetDTO
                {
                    Id = NovoUser.Id,
                    Nome = NovoUser.Nome,
                    Email = NovoUser.Email,
                    Perfil = NovoUser.Perfil
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao adicionar usuário: {ex.Message}");
            }
        }

        public async Task<UserGetDTO> DeleteAsync(int id)
        {
            try
            {
               var user = await _userRepository.DeleteAsync(id);
                if(user == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }
                return new UserGetDTO
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    Email = user.Email,
                    Perfil = user.Perfil
                };
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao Excluir usuário: {ex.Message}");
            }
        }

        public async Task<PageList<UserGetDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
             try
            {
                var user = await _userRepository.GetAllAsync(pageNumber, pageSize);
                var userGetDTOs = new List<UserGetDTO>();
                foreach(var users in user)
                {
                    userGetDTOs.Add(new UserGetDTO
                    {
                        Id = users.Id,
                        Nome = users.Nome,
                        Email = users.Email,
                        Perfil = users.Perfil
                    });
                }
                return new PageList<UserGetDTO>(userGetDTOs, user.TotalCount, user.CurrentPage, user.PageSize);
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao obter usuários: {ex.Message}");
            }
        }

         public async Task<List<UserGetDTO>> GetAllAsync()
        {
             try
            {
                var user = await _userRepository.GetAllAsync();
                var userGetDTOs = new List<UserGetDTO>();
                foreach(var users in user)
                {
                    userGetDTOs.Add(new UserGetDTO
                    {
                        Id = users.Id,
                        Nome = users.Nome,
                        Email = users.Email,
                        Perfil = users.Perfil,
                        Excluido = users.Excluido
                    });
                }
                return userGetDTOs;
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao obter usuários: {ex.Message}");
            }
        }
        public async Task<UserGetDTO> GetByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if(user == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }
                return new UserGetDTO
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    Email = user.Email,
                    Perfil = user.Perfil
                };
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao obter usuário: {ex.Message}");
            }
        }
        

        public async Task<UserGetDTO> UpdateAsync(UserPutDTO userPutDTO)
        {
             try
            {
                var user = await _userRepository.GetByIdAsync(userPutDTO.Id);
                if(user == null)
                {
                    throw new KeyNotFoundException($"Usuário com ID {userPutDTO.Id} não foi encontrado.");
                }

                user.Nome = userPutDTO.Nome;
                user.Email = userPutDTO.Email;

                var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userPutDTO.Senha ?? ""));
                byte[] passwordSalt = hmac.Key;

                user.PasswordHash = passwordHash;
                user.PasswordSalt =passwordSalt;
                
                await _userRepository.UpdateAsync(user);
                return new UserGetDTO
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    Email = user.Email,
                    Perfil = user.Perfil
                };
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao actualizar usuário: {ex.Message}");
            }
        }
    }
}