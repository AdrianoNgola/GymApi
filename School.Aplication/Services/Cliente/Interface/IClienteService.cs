using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Aplication.DTOs.Cliente;
using School.Domain.Pagination;

namespace School.Aplication.Services.Cliente.Interface
{
    public interface IClienteService
    {
        Task<ClienteGetDTO> GetByIdAsync(Guid id);
        Task<PageList<ClienteGetDTO>> GetAllAsync(int pageNumber, int pageSize);
        Task<ClienteGetDTO> AddAsync(ClientePostDTO clientePostDTO);
        Task<ClienteGetDTO> UpdateAsync(ClientePutDTO clientePutDTO, Guid Id);
        Task<ClienteGetDTO> DeleteAsync(Guid id);
        Task<ClienteGetDTO> ActivarAsync(Guid id);
    }
}