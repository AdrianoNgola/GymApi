using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.InfraData.Context;

namespace School.InfraData.Repositries
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly AplicationDbContext _context;

        public MatriculaRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Matricula> AddAsync(Matricula matricula)
        {
            _context.Matricula.Add(matricula);
            await _context.SaveChangesAsync();
            return matricula;
        }

        public async Task<Matricula?> DeleteAsync(int id)
        {
           var matricula = await _context.Matricula.Where(x => x.Excluido == false && x.Id == id).FirstOrDefaultAsync();
           if(matricula != null)
            {
                 matricula.Excluido = true;
               _context.Matricula.Update(matricula);
               await _context.SaveChangesAsync();
               return matricula;
            } 

          return null;
            
        }

        public async Task<List<Matricula>> GetAllAsync()
        {
            return await _context.Matricula.Include(x => x.User).Include(x => x.Turma).Where(x => x.Excluido == false).ToListAsync();
        }

        public async Task<Matricula?> GetByIdAsync(int id)
        {
            return await _context.Matricula.Include(x => x.User).Include(x => x.Turma).Where(x => x.Id == id && x.Excluido == false).FirstOrDefaultAsync();
        }

        public async Task<Matricula> UpdateAsync(Matricula matricula)
        {
            _context.Matricula.Update(matricula);
            await _context.SaveChangesAsync();
            return matricula;
        }
    }
}