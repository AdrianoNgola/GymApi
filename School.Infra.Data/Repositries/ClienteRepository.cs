using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.Domain.Account;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Domain.Pagination;
using School.Infra.Data.Helpers;
using School.InfraData.Context;

namespace School.Infra.Data.Repositries
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AplicationDbContext _context;
        private readonly IAuthenticate _auth;

        public ClienteRepository(AplicationDbContext context, IAuthenticate auth)
        {
            _context = context;
            _auth = auth;
        }
        public async Task<Cliente?> ActivarAsync(Guid id)
        {
            var cliente = await _context.Cliente.Where(x => x.ClienteId == id && x.Activo == false).FirstOrDefaultAsync();
            if(cliente != null)
            {
                cliente.Activo = true;
                cliente.DataFim = null;
                cliente.DataFim = DateTime.UtcNow;
                cliente.ActualUtilizadorId = _auth.GetUtilizadorId();
                _context.Cliente.Update(cliente);
                await _context.SaveChangesAsync();
                return cliente;
            }
            
            return null;
        }

        public async Task<Cliente> AddAsync(Cliente cliente)
        {
            await _context.Cliente.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente?> DeleteAsync(Guid id)
        {
            var cliente = await _context.Cliente.Where(x => x.ClienteId == id && x.Activo == true).FirstOrDefaultAsync();
            if(cliente != null)
            {
                cliente.Activo = false;
                cliente.DataFim = DateTime.UtcNow;
                cliente.ActualUtilizadorId = _auth.GetUtilizadorId();
                _context.Cliente.Update(cliente);
                await _context.SaveChangesAsync();
                return cliente;
            }
            
            return null;
        }

        public async Task<PageList<Cliente>> GetAllAsync(int pageNumber, int pageSize)
        {
            var Query = _context.Cliente.Where(x => x.Activo != false).AsNoTracking();
            return await PaginationHelpers.CreateAsync(Query, pageNumber, pageSize);
        }

        public async Task<Cliente?> GetByIdAsync(Guid id)
        {
            return await _context.Cliente.Where(x => x.ClienteId == id && x.Activo != false).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Cliente> UpdateAsync(Cliente cliente)
        {
            _context.Cliente.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
    }
}