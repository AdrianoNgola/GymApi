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
    public class CursoRepository : ICursoRepository
    {
        
        private readonly AplicationDbContext _context;
        
        public CursoRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Curso> AddAsync(Curso curso)
        {
            try
            {
                 _context.Curso.Add(curso);
                await _context.SaveChangesAsync();
                return curso;
            }
            catch (DbUpdateException ex)
            {
                // Erros relacionados ao banco de dados
                throw new InvalidOperationException("Erro ao adicionar curso ao banco de dados.", ex);
            }
            catch (ArgumentNullException ex)
            {
                // Caso o curso seja nulo
                throw new ArgumentNullException("O curso fornecido é nulo.", ex);
            }
            catch (Exception ex)
            {
                // Qualquer outro erro inesperado
                throw new ApplicationException("Erro inesperado ao adicionar curso.", ex);
            }
            

        }

        public async Task<Curso?> DeleteAsync(int id)
        {
            var curso = _context.Curso.Where(x => x.Excluido == false && x.Id == id).FirstOrDefault();
            if (curso != null)
            {
                curso.Excluido = true;
                _context.Curso.Update(curso);
               await _context.SaveChangesAsync();
                return curso;
                
            }


            return null;
        }

        public async Task<List<Curso>> GetAllAsync()
        {
           return await _context.Curso.Where(c => !c.Excluido).ToListAsync();
           
        }

        public async Task<Curso?> GetByIdAsync(int id)
        {
            var Curso = await _context.Curso.Where(c => c.Excluido == false && c.Id == id).FirstOrDefaultAsync();

            return Curso;

        }

        public async Task<Curso> UpdateAsync(Curso curso)
        {
             _context.Curso.Update(curso);
             await _context.SaveChangesAsync();
             return curso;
        }
    }
}