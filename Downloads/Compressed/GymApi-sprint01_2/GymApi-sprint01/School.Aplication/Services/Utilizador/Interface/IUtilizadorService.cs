using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Aplication.DTOs.Utilizador;
using School.Domain.Pagination;

namespace School.Aplication.Services.User.Interface
{
    public interface IUtilizadorService
    {
        Task<UtilizadorGetDTO> GetByIdAsync(Guid id);
        Task<PageList<UtilizadorGetDTO>> GetAllAsync(int pageNumber, int pageSize);
        Task<UtilizadorGetDTO> AddAsync(UtilizadorPostDTO utilizadorPostDTO);
        Task<UtilizadorGetDTO> UpdateAsync(UtilizadorPutDTO utilizadorPutDTO, Guid Id);
        Task<UtilizadorGetDTO> DeleteAsync(Guid id);
        Task<UtilizadorGetDTO> ActivarAsync(Guid id);
    }

   
}