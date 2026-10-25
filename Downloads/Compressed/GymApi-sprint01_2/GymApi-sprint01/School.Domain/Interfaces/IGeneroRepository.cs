using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Domain.Entities;

namespace School.Domain.Interfaces
{
    public interface IGeneroRepository
    {
        Task<Genero?> GetByIdAsync(Guid id);
        Task<List<Genero>> GetAllAsync();
    }
}