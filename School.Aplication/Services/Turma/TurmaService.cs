using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Aplication.DTOs;
using School.Aplication.DTOs.Turma;
using School.Aplication.Services.Turma.Interface;
using School.Domain.Interfaces;

namespace School.Aplication.Services.Turma
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
       
        public TurmaService(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        

        }
        public async Task<TurmaGetDTO> AddAsync(TurmaPostDTO turmaPostDTO)
        {
            try
            {
                // Criação da entidade
                var turma = new Domain.Entities.Turma
                {
                    Nome = turmaPostDTO.Nome,
                    Descricao = turmaPostDTO.Descricao,
                    CursoId = turmaPostDTO.CursoId
                   
                };

                // Persistência no repositório
                var criarTurma = await _turmaRepository.AddAsync(turma);

                // Retorno do DTO
                return new TurmaGetDTO
                {
                    Id = criarTurma!.Id,
                    Nome = criarTurma.Nome,
                    Descricao = criarTurma.Descricao,
                    CursoId = criarTurma.CursoId
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
                throw new ApplicationException("Erro ao criar turma.", ex);
            }
        }

               
        public async Task<TurmaGetDTO?> DeleteAsync(int id)
        {
            try
            {
                var turma = await _turmaRepository.DeleteAsync(id);
                if (turma == null)
                {
                    return null;
                }

                return new TurmaGetDTO
                {
                    Id = turma.Id,
                    Nome = turma.Nome,
                    Descricao = turma.Descricao,
                    CursoId = turma.CursoId
                };
            }
            catch (Exception ex)
            {
                 // Qualquer outro erro inesperado
                throw new ApplicationException("Erro ao deletar turma.", ex);
            }
        }

        public async Task<bool> ExistsAsync(string nome)
        {
           try
           {
              var turma = await _turmaRepository.ExistsAsync(nome);
            if(!turma)
             return true;
            
            return false;
           }
           catch (Exception ex)
           {
            
            throw new ApplicationException("Erro ao verificar existência da turma.", ex);
           }
        }

        public async Task<List<DetalheTurmaDTO?>> GetAllAsync()
        {
             try
            {
                var turma = await _turmaRepository.GetAllAsync();
                if(turma == null)
                {
                    return null!;
                }
                var TurmaGet =new  List<DetalheTurmaDTO>();
                foreach(var turmas in turma)
                {
                
                    TurmaGet.Add(new DetalheTurmaDTO
                    {
                        Id = turmas.Id,
                        Nome = turmas.Nome,
                        Descricao = turmas.Descricao,
                        
                        Curso = new CursoGetDTO
                        {
                            Id = turmas.Curso!.Id,
                            Nome = turmas.Curso.Nome,
                            Descricao = turmas.Curso.Descricao,
                            CargaHoraria = turmas.Curso.CargaHoraria,
                            Preco = turmas.Curso.Preco
                        }
                    });
                }
                return TurmaGet!;
            }
            catch (Exception ex)
            {
                 // Qualquer outro erro inesperado
                throw new ApplicationException("Erro ao obter lista de turmas.", ex);
            }
        }

        public async Task<DetalheTurmaDTO?> GetByIdAsync(int id)
        {
             try
            {
                var turma = await _turmaRepository.GetByIdAsync(id);
                if(turma == null)
                {
                    return null;
                }

                return new DetalheTurmaDTO
                {
                    Id = turma.Id,
                    Nome = turma.Nome,
                    Descricao = turma.Descricao,
                    
                    Curso = new CursoGetDTO
                    {
                        Id = turma.Curso!.Id,
                        Nome = turma.Curso.Nome,
                        Descricao = turma.Curso.Descricao
                    }
                   
                };
            }
            catch (Exception ex)
            {
                 // Qualquer outro erro inesperado
                throw new ApplicationException("Erro ao obter turma.", ex);
            }
        }

        public async Task<TurmaGetDTO> UpdateAsync(TurmaPutDTO turmaPutDTO)
        {
            try
            {
                var turma = await _turmaRepository.GetByIdAsync(turmaPutDTO.Id);
                if (turma == null)
                {
                    throw new KeyNotFoundException($"Turma com ID {turmaPutDTO.Id} não encontrada.");
                }

                // Atualiza os campos do curso
                turma.Nome = turmaPutDTO.Nome;
                turma.Descricao = turmaPutDTO.Descricao;
                

                var atualizarTurma = await _turmaRepository.UpdateAsync(turma);

                return new TurmaGetDTO
                {
                    Id = atualizarTurma!.Id,
                    Nome = atualizarTurma.Nome,
                    Descricao = atualizarTurma.Descricao,
                    CursoId = atualizarTurma.CursoId
                };

            }
            catch(Exception ex)
            {
                throw new ApplicationException("Erro ao atualizar turma.", ex);
            }
        }
    }
}