using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Aplication.DTOs.Turma;

namespace School.Aplication.Services.Turma.Interface
{
    public interface ITurmaService
    {
         Task<DetalheTurmaDTO?> GetByIdAsync(int id);
        Task<List<DetalheTurmaDTO?>> GetAllAsync();
        Task<TurmaGetDTO> AddAsync(TurmaPostDTO turmaPostDTO);
        Task<TurmaGetDTO> UpdateAsync(TurmaPutDTO turmaPutDTO);
        Task<TurmaGetDTO?> DeleteAsync(int id);
        Task<bool> ExistsAsync(string nome);
        
    }
}