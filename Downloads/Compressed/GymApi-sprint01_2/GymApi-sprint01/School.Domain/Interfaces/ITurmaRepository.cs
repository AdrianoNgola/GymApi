using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Domain.Entities;

namespace School.Domain.Interfaces
{
    public interface ITurmaRepository
    {
        Task<Turma?> GetByIdAsync(int id);
        Task<List<Turma>> GetAllAsync();
        Task<Turma?> AddAsync(Turma turma);
        Task<Turma?> UpdateAsync(Turma turma);
        Task<Turma?> DeleteAsync(int id);
        Task<bool> ExistsAsync(string nome);
    }
}