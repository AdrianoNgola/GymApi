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
    public class GeneroRepository : IGeneroRepository
    {
        private readonly AplicationDbContext _context;
        public GeneroRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Genero>> GetAllAsync()
        {
            return await _context.Genero.ToListAsync();
        }

        public  Task<Genero?> GetByIdAsync(Guid id)
        {
            return _context.Genero.Where(x => x.GeneroId == id).FirstOrDefaultAsync();
        }
    }
}