
using System.Security.Cryptography;
using School.Aplication.DTOs.Utilizador;
using School.Aplication.Services.User.Interface;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Domain.Pagination;

namespace School.Aplication.Services.Utilizador
{
    public class UtilizadorService : IUtilizadorService
    {
        private readonly IUtilizadorRepository _utilizadorRepository;
        private readonly IPerfilRepository _Perfil;
         private readonly IGeneroRepository _Genero;

        public UtilizadorService(IUtilizadorRepository utilizadorRepository, IPerfilRepository Perfil,  IGeneroRepository Genero)
        {
            _utilizadorRepository = utilizadorRepository;
            _Perfil = Perfil;
            _Genero = Genero;
        }

        public async Task<UtilizadorGetDTO> ActivarAsync(Guid id)
        {
             try
            {
               var utilizador = await _utilizadorRepository.ActivarAsync(id);
                if(utilizador == null)
                {
                    return null;
                }
                
                var perfil = await _Perfil.GetByIdAsync(utilizador.PerfilId);
                var genero = await _Genero.GetByIdAsync(utilizador.GeneroId);
                return new UtilizadorGetDTO
                {
                    Id = utilizador.UtilizadorId,
                    Nome = utilizador.Nome,
                    Email = utilizador.Email,
                    Perfil = perfil.Nome,
                    Genero = genero.Nome,
                    DataNascimento = utilizador.DataNascimento,
                    Activo = utilizador.Activo
                };
             }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao activar o utilizador: {ex.Message}");
            }
        }

        public async Task<UtilizadorGetDTO> AddAsync(UtilizadorPostDTO utilizadorPostDTO)
        {
            try
            {
                var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(utilizadorPostDTO.PalavraPasse ?? ""));
                byte[] passwordSalt = hmac.Key;

                var utilizador = new School.Domain.Entities.Utilizador
                {
                    Nome = utilizadorPostDTO.Nome,
                    Email = utilizadorPostDTO.Email,
                    PalavraPasseHash = passwordHash,
                    PalavraPasseSalt = passwordSalt,
                    PerfilId = utilizadorPostDTO.PerfilId,
                    GeneroId = utilizadorPostDTO.GeneroId,
                    DataNascimento = utilizadorPostDTO.DataNascimento,
                    Activo = true,
                    DataInicio = DateTime.UtcNow,
                };

                var perfil = await _Perfil.GetByIdAsync(utilizador.PerfilId);
                var genero = await _Genero.GetByIdAsync(utilizador.GeneroId);
                var NovoUtilizador = await _utilizadorRepository.AddAsync(utilizador);

                return new UtilizadorGetDTO
                {
                    Id = NovoUtilizador.UtilizadorId,
                    Nome = NovoUtilizador.Nome,
                    Email = NovoUtilizador.Email,
                    Perfil = perfil.Nome,
                    Genero = genero.Nome,
                    DataNascimento = NovoUtilizador.DataNascimento,
                    Activo = NovoUtilizador.Activo

                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao adicionar utilizador: {ex.Message}");
            }
        }

        public async Task<UtilizadorGetDTO> DeleteAsync(Guid id)
        {
            try
            {
               var utilizador = await _utilizadorRepository.DeleteAsync(id);
                if(utilizador == null)
                {
                    return null;
                }
                
                var perfil = await _Perfil.GetByIdAsync(utilizador.PerfilId);
                var genero = await _Genero.GetByIdAsync(utilizador.GeneroId);
                return new UtilizadorGetDTO
                {
                    Id = utilizador.UtilizadorId,
                    Nome = utilizador.Nome,
                    Email = utilizador.Email,
                    Perfil = perfil.Nome,
                    Genero = genero.Nome,
                    DataNascimento = utilizador.DataNascimento,
                    Activo = utilizador.Activo
                };
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao Excluir utilizador: {ex.Message}");
            }
        }

        public async Task<PageList<UtilizadorGetDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var utilizador = await _utilizadorRepository.GetAllAsync(pageNumber, pageSize);
                var utilizadorGetDTOs = new List<UtilizadorGetDTO>();
                foreach(var utilizadores in utilizador)
                {
                    var perfil = await _Perfil.GetByIdAsync(utilizadores.PerfilId);
                    var genero = await _Genero.GetByIdAsync(utilizadores.GeneroId);
                    utilizadorGetDTOs.Add(new UtilizadorGetDTO
                    {
                        Id = utilizadores.UtilizadorId,
                        Nome = utilizadores.Nome,
                        Email = utilizadores.Email,
                        Perfil = perfil.Nome,
                        Genero = genero.Nome,
                        DataNascimento = utilizadores.DataNascimento,
                        Activo = utilizadores.Activo

                    });
                }
                return new PageList<UtilizadorGetDTO>(utilizadorGetDTOs, utilizador.TotalCount, utilizador.CurrentPage, utilizador.PageSize);
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao obter utilizadores: {ex.Message}");
            }
        }

        

        public async Task<UtilizadorGetDTO> GetByIdAsync(Guid id)
        {
             try
            {
                var utilizador = await _utilizadorRepository.GetByIdAsync(id);
                if(utilizador == null)
                {
                    return null;
                   
                }

                var perfil = await _Perfil.GetByIdAsync(utilizador.PerfilId);
                var genero = await _Genero.GetByIdAsync(utilizador.GeneroId);

                return new UtilizadorGetDTO
                {
                    Id = utilizador.UtilizadorId,
                    Nome = utilizador.Nome,
                    Email = utilizador.Email,
                    Perfil = perfil.Nome,
                    Genero = genero.Nome,
                    DataNascimento = utilizador.DataNascimento,
                    Activo = utilizador.Activo
                };
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao obter utilizador: {ex.Message}");
            }
        }

        public async Task<UtilizadorGetDTO> UpdateAsync(UtilizadorPutDTO utilizadorPutDTO, Guid Id)
        {
             try
            {
                var utilizador = await _utilizadorRepository.GetByIdAsync(Id);
                if(utilizador == null)
                {
                   return null;
                }
                
                var perfil = await _Perfil.GetByIdAsync(utilizador.PerfilId);
                var genero = await _Genero.GetByIdAsync(utilizador.GeneroId);
                utilizador.Nome = utilizadorPutDTO.Nome;
                utilizador.Email = utilizadorPutDTO.Email;
                utilizador.DataNascimento = utilizadorPutDTO.DataNascimento;
                utilizador.GeneroId = utilizadorPutDTO.GeneroId;
                utilizador.PerfilId = utilizadorPutDTO.PerfilId;
              
                

                var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(utilizadorPutDTO.PalavraPasse ?? ""));
                byte[] passwordSalt = hmac.Key;

                utilizador.PalavraPasseHash = passwordHash;
                utilizador.PalavraPasseSalt =passwordSalt;
                
                await _utilizadorRepository.UpdateAsync(utilizador);
                return new UtilizadorGetDTO
                {
                    Id = utilizador.UtilizadorId,
                    Nome = utilizador.Nome,
                    Email = utilizador.Email,
                    Perfil = perfil.Nome,
                    Genero = genero.Nome,
                    DataNascimento = utilizador.DataNascimento,
                    Activo = utilizador.Activo
                };
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao actualizar utilizador: {ex.Message}");
            }
        }
    }
}