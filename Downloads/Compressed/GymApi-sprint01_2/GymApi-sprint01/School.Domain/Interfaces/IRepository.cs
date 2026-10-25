using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Domain.Pagination;

namespace School.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<PageList<T>> GetAllAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T?> DeleteAsync(Guid id);
         Task<T?> ActivarAsync(Guid id);
    }
}