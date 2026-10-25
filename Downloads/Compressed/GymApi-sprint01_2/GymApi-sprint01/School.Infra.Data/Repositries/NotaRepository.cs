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
    public class NotaRepository : INotaRepository
    {
        private readonly AplicationDbContext _context;
        public NotaRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Nota> AddAsync(Nota nota)
        {
            if(nota.ValorNota >= 10)
              nota.Aprovado = true;
            else
                nota.Aprovado = true;

             _context.Nota.Add(nota);
             await _context.SaveChangesAsync();
             return nota;
        }

        public async Task<Nota?> DeleteAsync(int id)
        {
            var nota = await _context.Nota.Where(x => x.Id == id && x.Excluido != true).FirstOrDefaultAsync();
           if(nota != null)
           {
                nota.Excluido = true;
                _context.Nota.Update(nota);
                await _context.SaveChangesAsync();
                return nota;
           }
           return null;
        }

        public async Task<List<Nota>> GetAllAsync()
        {
           return await _context.Nota.Where(x => x.Excluido == false).ToListAsync();
          
        }

        public async Task<Nota?> GetByIdAsync(int id)
        {
            return await _context.Nota.Where(x => x.Id == id && x.Excluido == false).FirstOrDefaultAsync();
        }
        

        public async Task<Nota> UpdateAsync(Nota nota)
        {
            _context.Nota.Update(nota);
            await _context.SaveChangesAsync();
            return nota;
        }
    }
}