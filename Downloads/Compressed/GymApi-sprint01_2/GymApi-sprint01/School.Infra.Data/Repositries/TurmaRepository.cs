using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.InfraData.Context;

namespace School.InfraData.Repositries
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly AplicationDbContext _context;
        

        public TurmaRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Turma?> AddAsync(Turma turma)
        {
            _context.Turma.Add(turma);
            await _context.SaveChangesAsync();
            return turma;
        }

        public async Task<Turma?> DeleteAsync(int id)
        {
            var turma = await _context.Turma.Where(x => x.Excluido == false && x.Id == id).FirstOrDefaultAsync();
            if (turma != null)
            {
                turma.Excluido = true;
                _context.Turma.Update(turma);
                await _context.SaveChangesAsync();
                return turma;
            }
            return null;
        }

        public async Task<bool> ExistsAsync(string nome)
        {
            var turma = await _context.Turma.Where(x => x.Excluido == false && x.Nome == nome).FirstOrDefaultAsync();
            if (turma != null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Turma>> GetAllAsync()
        {
            return await _context.Turma.Include(x => x.Curso).Where(x => x.Excluido == false).ToListAsync();
        }

        public async Task<Turma?> GetByIdAsync(int id)
        {
            return await _context.Turma.Include(x => x.Curso).Where(x => x.Excluido == false && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Turma?> UpdateAsync(Turma turma)
        {
            _context.Turma.Update(turma);
            await _context.SaveChangesAsync();
            return turma;
        }
    }
}