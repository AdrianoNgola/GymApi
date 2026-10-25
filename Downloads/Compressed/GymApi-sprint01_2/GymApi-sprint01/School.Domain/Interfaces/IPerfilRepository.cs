using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Domain.Entities;

namespace School.Domain.Interfaces
{
    public interface IPerfilRepository
    {
        Task<Perfil?> GetByIdAsync(Guid id);
        Task<List<Perfil>> GetAllAsync();
    }
}