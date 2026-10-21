
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.InfraData.Context;
using Microsoft.EntityFrameworkCore;
using School.Domain.Pagination;
using School.Infra.Data.Helpers;

namespace School.Infra.Data.Repositries
{
    public class UtilizadorRepository : IUtilizadorRepository
    {

        private readonly AplicationDbContext _context;

        public UtilizadorRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Utilizador> AddAsync(Utilizador utilizador)
        {
           await _context.AddAsync(utilizador);
           await _context.SaveChangesAsync();
            return utilizador;
        }

        public async Task<Utilizador?> DeleteAsync(Guid id)
        {
           var utilizador = await _context.Utilizador.Where(x => x.UtilizadorId == id && x.Activo == true).FirstOrDefaultAsync();
            if(utilizador != null)
            {
                utilizador.Activo = false;
                utilizador.DataFim = DateTime.UtcNow;
                _context.Utilizador.Update(utilizador);
                await _context.SaveChangesAsync();
                return utilizador;
            }
            
            return null;
        }


         public async Task<Utilizador?> ActivarAsync(Guid id)
        {
           var utilizador = await _context.Utilizador.Where(x => x.UtilizadorId == id && x.Activo == false).FirstOrDefaultAsync();
            if(utilizador != null)
            {
                utilizador.Activo = true;
                utilizador.DataFim = DateTime.UtcNow;
                _context.Utilizador.Update(utilizador);
                await _context.SaveChangesAsync();
                return utilizador;
            }
            
            return null;
        }

        public async Task<PageList<Utilizador>> GetAllAsync(int pageNumber, int pageSize)
        {
            var Query = _context.Utilizador.Where(x => x.Activo != false).AsNoTracking();
            return await PaginationHelpers.CreateAsync(Query, pageNumber, pageSize);
        }

        public async Task<Utilizador?> GetByIdAsync(Guid id)
        {
            var utilizador = await _context.Utilizador.Where(x => x.UtilizadorId == id && x.Activo == true).FirstOrDefaultAsync();
            return utilizador;
        }

        public async Task<Utilizador> UpdateAsync(Utilizador utilizador)
        {
            utilizador.DataActualizacao = DateTime.UtcNow;
            _context.Utilizador.Update(utilizador);
            await _context.SaveChangesAsync();
            return utilizador;
        }
    }
}