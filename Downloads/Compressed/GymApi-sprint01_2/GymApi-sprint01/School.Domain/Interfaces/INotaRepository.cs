using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Domain.Entities;

namespace School.Domain.Interfaces
{
    public interface INotaRepository
    {
        Task<Nota?> GetByIdAsync(int id);
        Task<List<Nota>> GetAllAsync();
        Task<Nota> AddAsync(Nota nota);
        Task<Nota> UpdateAsync(Nota nota);
        Task<Nota?> DeleteAsync(int id);
    }
}