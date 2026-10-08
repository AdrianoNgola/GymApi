using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Domain.Pagination;
using School.Infra.Data.Helpers;
using School.InfraData.Context;

namespace School.InfraData.Repositries
{
    public class UserRepository : IUserRepository
    {
        private readonly AplicationDbContext _context;

        public UserRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> AddAsync(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> DeleteAsync(int id)
        {
            var user = await _context.User.Where(x => x.Excluido == false && x.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Excluido = true;
                _context.User.Update(user);
                await _context.SaveChangesAsync();
                return user;
            }
            return null;
        }

        public async Task<PageList<User>> GetAllAsync(int pageNumber, int pageSize)
        {
            var Query = _context.User.Where(x => x.Excluido != true).AsNoTracking();
            return await PaginationHelpers.CreateAsync(Query, pageNumber, pageSize);
        }

         public async Task<List<User>> GetAllAsync()
        {
           
            return await _context.User.Where(x => x.Excluido == false).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.User.Where(x => x.Excluido !=true && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}