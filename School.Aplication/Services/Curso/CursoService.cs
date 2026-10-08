using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Aplication.DTOs;
using School.Aplication.Services.Curso;
using School.Domain.Entities;
using School.Domain.Interfaces;

namespace School.Aplication.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }
        public async Task<CursoGetDTO> AddAsync(CursoPostDTO cursoPostDTO)
        {
            try
            {
                // Criação da entidade
                var curso = new Domain.Entities.Curso
                {
                    Nome = cursoPostDTO.Nome,
                    Descricao = cursoPostDTO.Descricao,
                    Preco = cursoPostDTO.Preco,
                    CargaHoraria = cursoPostDTO.CargaHoraria
                };

                // Persistência no repositório
                var criarCurso = await _cursoRepository.AddAsync(curso);

                // Retorno do DTO
                return new CursoGetDTO
                {
                    Id = criarCurso.Id,
                    Nome = criarCurso.Nome,
                    Descricao = criarCurso.Descricao,
                    Preco = criarCurso.Preco,
                    CargaHoraria = criarCurso.CargaHoraria
                };
            }
            catch (ArgumentException ex)
            {
                // Erros de validação de entrada
                throw new InvalidOperationException($"Erro de validação: {ex.Message}");
            }
            catch (NullReferenceException ex)
            {
                 // Caso cursoPostDTO ou repositório estejam nulos
                throw new InvalidOperationException("Objecto ou dependência não inicializada.", ex);
            }
            catch (Exception ex)
            {
                 // Qualquer outro erro inesperado
                throw new ApplicationException("Erro ao criar curso.", ex);
            }

        }

        public async Task<CursoGetDTO?> DeleteAsync(int id)
        {
            try
            {
                var curso = await _cursoRepository.DeleteAsync(id);
                if (curso == null)
                {
                    return null;
                }

                return new CursoGetDTO
                {
                    Id = curso.Id,
                    Nome = curso.Nome,
                    Descricao = curso.Descricao,
                    Preco = curso.Preco,
                    CargaHoraria = curso.CargaHoraria
                };
            }
            catch (Exception ex)
            {
                 // Qualquer outro erro inesperado
                throw new ApplicationException("Erro ao deletar curso.", ex);
            }
        }

        public async Task<List<CursoGetDTO?>> GetAllAsync()
        {
            
            try
            {
                var curso = await _cursoRepository.GetAllAsync();
                if(curso == null)
                {
                    return null!;
                }
                var CursoGet =new  List<CursoGetDTO>();
                foreach(var cursos in curso)
                {
                    CursoGet.Add(new CursoGetDTO
                    {
                        Id = cursos.Id,
                        Nome = cursos.Nome,
                        Descricao = cursos.Descricao,
                        Preco = cursos.Preco,
                        CargaHoraria = cursos.CargaHoraria
                    });
                }
                return CursoGet!;
            }
            catch (Exception ex)
            {
                 // Qualquer outro erro inesperado
                throw new ApplicationException("Erro ao obter lista de cursos.", ex);
            }
        }

        public async Task<CursoGetDTO?> GetByIdAsync(int id)
        {
            try
            {
                var curso = await _cursoRepository.GetByIdAsync(id);
                if(curso == null)
                {
                    return null;
                }

                return new CursoGetDTO
                {
                    Id = curso.Id,
                    Nome = curso.Nome,
                    Descricao = curso.Descricao,
                    Preco = curso.Preco,
                    CargaHoraria = curso.CargaHoraria
                };
            }
            catch (Exception ex)
            {
                 // Qualquer outro erro inesperado
                throw new ApplicationException("Erro ao obter curso.", ex);
            }
        }

        public async Task<CursoGetDTO> UpdateAsync(CursoPutDTO cursoPutDTO)
        {
            try
            {
                var curso = await _cursoRepository.GetByIdAsync(cursoPutDTO.Id);
                if (curso == null)
                {
                    throw new KeyNotFoundException($"Curso com ID {cursoPutDTO.Id} não encontrado.");
                }

                // Atualiza os campos do curso
                curso.Nome = cursoPutDTO.Nome;
                curso.Descricao = cursoPutDTO.Descricao;
                curso.Preco = cursoPutDTO.Preco;
                curso.CargaHoraria = cursoPutDTO.CargaHoraria;

                var atualizarCurso = await _cursoRepository.UpdateAsync(curso);

                return new CursoGetDTO
                {
                    Id = atualizarCurso.Id,
                    Nome = atualizarCurso.Nome,
                    Descricao = atualizarCurso.Descricao
                };

            }
            catch(Exception ex)
            {
                throw new ApplicationException("Erro ao atualizar curso.", ex);
            }
        }
    }
}