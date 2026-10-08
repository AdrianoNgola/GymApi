using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using School.Domain.Entities;

namespace School.Domain.Interfaces
{
    public interface ICursoRepository
    {
         Task<Curso?> GetByIdAsync(int id);
        Task<List<Curso>> GetAllAsync();
        Task<Curso> AddAsync(Curso curso);
        Task<Curso> UpdateAsync(Curso curso);
        Task<Curso?> DeleteAsync(int id);
    }
}