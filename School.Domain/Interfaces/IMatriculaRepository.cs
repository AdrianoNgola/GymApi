using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Domain.Entities;

namespace School.Domain.Interfaces
{
    public interface IMatriculaRepository
    {
        Task<Matricula?> GetByIdAsync(int id);
        Task<List<Matricula>> GetAllAsync();
        Task<Matricula> AddAsync(Matricula matricula);
        Task<Matricula> UpdateAsync(Matricula matricula);
        Task<Matricula?> DeleteAsync(int id);
    }
}