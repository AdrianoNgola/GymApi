using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Aplication.DTOs.Nota;
using School.Aplication.Services.Nota.Interface;
using School.Domain.Interfaces;

namespace School.Aplication.Services.Nota
{
    public class NotaService : INotaService
    {
        private readonly INotaRepository _notaRepository;

        public NotaService(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository;
        }
        public async Task<NotaGetDTO> AddAsync(NotaPostDTO notaPostDTO)
        {
            try
            {
                
                var nota = new Domain.Entities.Nota
                {
                    MatriculaId = notaPostDTO.MatriculaId,
                    ValorNota = notaPostDTO.ValorNota,
                    
                };

                var criarNota = await _notaRepository.AddAsync(nota);
                return new NotaGetDTO
                {
                    Id = criarNota.Id,
                    MatriculaId = criarNota.Id,
                    ValorNota = criarNota.ValorNota,
                    Aprovado = criarNota.Aprovado
                };


            }
            catch (Exception ex)
            {
                
                throw new ApplicationException("Erro ao criar matricula, contacta o Administrador", ex);
            }
        }

        public async Task<NotaGetDTO?> DeleteAsync(int id)
        {
             var nota = await _notaRepository.DeleteAsync(id);
                if (nota == null)
                {
                    return null;
                }

                return new NotaGetDTO
                {
                    Id = nota.Id,
                    MatriculaId = nota.MatriculaId,
                    ValorNota = nota.ValorNota,
                    Aprovado = nota.Aprovado
                };
        }

        public async Task<List<NotaGetDTO>> GetAllAsync()
        {
            var nota = await _notaRepository.GetAllAsync();
            var notaGet =new  List<NotaGetDTO>();
                foreach(var notas in nota)
                {
                
                    notaGet.Add(new NotaGetDTO
                    {
                        Id = notas.Id,
                        MatriculaId = notas.MatriculaId,
                        ValorNota = notas.ValorNota,
                        Aprovado = notas.Aprovado
                    });
                }
                return notaGet;
        }

        public async Task<NotaGetDTO?> GetByIdAsync(int id)
        {
            var nota = await _notaRepository.GetByIdAsync(id);
            if(nota == null)
              return null;
            return new NotaGetDTO
            {
                Id = nota.Id,
                MatriculaId = nota.MatriculaId,
                ValorNota = nota.ValorNota,
                Aprovado = nota.Aprovado
            };
        }

        public async Task<NotaGetDTO> UpdateAsync(NotaPutDTO notaPutDTO)
        {
             try
            {
                var nota = await _notaRepository.GetByIdAsync(notaPutDTO.Id);
                if (nota == null)
                {
                    throw new KeyNotFoundException($"Turma com ID {notaPutDTO.Id} não encontrada.");
                }

                // Atualiza os campos do curso
                nota.MatriculaId = notaPutDTO.MatriculaId;
                nota.ValorNota = notaPutDTO.ValorNota;

                if(notaPutDTO.ValorNota <= 9)
                    nota.Aprovado = false;
                else
                 nota.Aprovado = true;

                var atualizarNota = await _notaRepository.UpdateAsync(nota);

                return new NotaGetDTO
                {
                     Id = atualizarNota.Id,
                    MatriculaId = nota.MatriculaId,
                    ValorNota = nota.ValorNota,
                    Aprovado = nota.Aprovado
                };

            }
            catch(Exception ex)
            {
                throw new ApplicationException("Erro ao atualizar turma.", ex);
            }
        }
    }
}