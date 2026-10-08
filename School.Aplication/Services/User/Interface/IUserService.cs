using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Aplication.DTOs.User;
using School.Domain.Pagination;

namespace School.Aplication.Services.User.Interface
{
    public interface IUserService
    {
          Task<UserGetDTO> GetByIdAsync(int id);
        Task<PageList<UserGetDTO>> GetAllAsync(int pageNumber, int pageSize);
         Task<List<UserGetDTO>> GetAllAsync();
        Task<UserGetDTO> AddAsync(UserPostDTO userPostDTO);
        Task<UserGetDTO> UpdateAsync(UserPutDTO userPutDTO);
        Task<UserGetDTO> DeleteAsync(int id);
    }

   
}