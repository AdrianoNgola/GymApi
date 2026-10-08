

using School.Aplication.DTOs.Matricula;
using School.Aplication.DTOs.Turma;
using School.Aplication.DTOs.User;
using School.Aplication.Services.Matricula.Interface;
using School.Domain.Interfaces;

namespace School.Aplication.Services.Matricula
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository;

        public MatriculaService(IMatriculaRepository matriculaRepository)
        {
            _matriculaRepository = matriculaRepository;
        }
        public async Task<MatriculaGetDTO> AddAsync(MatriculaPostDTO matriculaPostDTO)
        {
            try
            {
                // Criação da entidade
                var matricula = new Domain.Entities.Matricula
                {
                    UserId = matriculaPostDTO.UserId,
                    TurmaId = matriculaPostDTO.TurmaId,
                    DataMatricula = DateTime.UtcNow,
                    Excluido = false,
                    Ativo = true,
                    DataExp = matriculaPostDTO.DataExp
                    
                   
                };

                // Persistência no repositório
                var criarMatricula = await _matriculaRepository.AddAsync(matricula);

                // Retorno do DTO
                return new MatriculaGetDTO
                {
                    Id = criarMatricula.Id,
                    UserId = criarMatricula.UserId,
                    TurmaId = criarMatricula.TurmaId,
                    DataMatricula = criarMatricula.DataMatricula,
                    Ativo = criarMatricula.Excluido,
                    DataExp = criarMatricula.DataExp,
                    

                };
            }
            catch (ArgumentException ex)
            {
                // Erros de validação de entrada
                throw new InvalidOperationException($"Erro de validação: {ex.Message}");
            }
            catch (NullReferenceException ex)
            {
                 // Caso turmaPostDTO ou repositório estejam nulos
                throw new InvalidOperationException("Objecto ou dependência não inicializada.", ex);
            }
            catch (Exception ex)
            {
                 // Qualquer outro erro inesperado
                throw new ApplicationException("Erro ao criar matricula.", ex);
            }
        }

        public async Task<MatriculaGetDTO?> DeleteAsync(int id)
        {
            try
            {
                var matricula = await _matriculaRepository.DeleteAsync(id);
                if (matricula == null)
                {
                    return null;
                }

                return new MatriculaGetDTO
                {
                    Id = matricula.Id,
                    UserId = matricula.UserId,
                    TurmaId = matricula.TurmaId,
                    DataMatricula = matricula.DataMatricula,
                    Ativo = matricula.Excluido,
                    DataExp = matricula.DataExp,
                };
            }
            catch (Exception ex)
            {
                 // Qualquer outro erro inesperado
                throw new ApplicationException("Erro ao deletar matricula.", ex);
            }
        }

        public async Task<List<DetalheMatriculaDTO?>> GetAllAsync()
        {
             try
            {
                var matricula = await _matriculaRepository.GetAllAsync();
                if(matricula == null)
                {
                    return null!;
                }
                var matriculaGet =new  List<DetalheMatriculaDTO>();
                foreach(var matriculas in matricula)
                {
                
                    matriculaGet.Add(new DetalheMatriculaDTO
                    {
                        Id = matriculas.Id,
                        DataMatricula = matriculas.DataMatricula,
                        Ativo = matriculas.Ativo,
                        DataExp = matriculas.DataExp,
                        
                        User = new UserGetDTO
                        {
                            Id = matriculas.User!.Id,
                            Nome = matriculas.User.Nome,                     
                            Email = matriculas.User.Email
                        },
                        Turma = new TurmaGetDTO
                        {
                            Id = matriculas.Turma!.Id,
                            Nome = matriculas.Turma.Nome,
                            Descricao = matriculas.Turma.Descricao
                        }

                    });
                }
                return matriculaGet!;
            }
            catch (Exception ex)
            {
                 // Qualquer outro erro inesperado
                throw new ApplicationException("Erro ao obter lista de matriculas.", ex);
            }
        }

        public async Task<DetalheMatriculaDTO?> GetByIdAsync(int id)
        {
           try
           {
                var matricula = await _matriculaRepository.GetByIdAsync(id);
                if(matricula == null)
                {
                    return null;
                }

                return new DetalheMatriculaDTO
                {
                        Id = matricula.Id,
                        DataMatricula = matricula.DataMatricula,
                        Ativo = matricula.Excluido,
                        DataExp = matricula.DataExp,
                        
                        User = new UserGetDTO
                        {
                            Id = matricula.User!.Id,
                            Nome = matricula.User.Nome,                     
                            Email = matricula.User.Email
                        },
                        Turma = new TurmaGetDTO
                        {
                            Id = matricula.Turma!.Id,
                            Nome = matricula.Turma.Nome,
                            Descricao = matricula.Turma.Descricao
                        }

                };

           }
           catch (Exception ex)
           {
            
            throw new ApplicationException("Erro ao atualizar matricula.", ex);
           }
        }

        public async Task<MatriculaGetDTO> UpdateAsync(MatriculaPutDTO matriculaPutDTO)
        {
            try
            {
                var matricula = await _matriculaRepository.GetByIdAsync(matriculaPutDTO.Id);
                if (matricula == null)
                {
                    throw new KeyNotFoundException($"Turma com ID {matriculaPutDTO.Id} não encontrada.");
                }

                // Atualiza os campos do curso
                matricula.UserId = matriculaPutDTO.UserId;
                matricula.TurmaId = matriculaPutDTO.TurmaId;
                matricula.DataExp = matriculaPutDTO.DataExp;
                

                var atualizarMatricula = await _matriculaRepository.UpdateAsync(matricula);

                return new MatriculaGetDTO
                {
                     Id = atualizarMatricula.Id,
                    UserId = atualizarMatricula.UserId,
                    TurmaId = atualizarMatricula.TurmaId,
                    DataMatricula = atualizarMatricula.DataMatricula,
                    Ativo = atualizarMatricula.Excluido,
                    DataExp = atualizarMatricula.DataExp,
                };

            }
            catch(Exception ex)
            {
                throw new ApplicationException("Erro ao atualizar turma.", ex);
            }
        }
    }
}