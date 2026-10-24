using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.InfraData.Context;

namespace School.Infra.Data.Repositries
{
    public class PerfilRepository: IPerfilRepository
    {
        private readonly AplicationDbContext _context;
        public PerfilRepository(AplicationDbContext context)
        {
            _context = context;
        }

         public Task<Perfil?> GetByIdAsync(Guid id)
        {
            return _context.Perfil.Where(x => x.PerfilId == id).FirstOrDefaultAsync();
        }
        public async Task<List<Perfil>> GetAllAsync()
        {
           return await _context.Perfil.ToListAsync();
        }

       
    }
}
      
            