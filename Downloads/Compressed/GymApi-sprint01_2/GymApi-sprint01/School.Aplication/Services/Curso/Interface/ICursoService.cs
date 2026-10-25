using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Aplication.DTOs;

namespace School.Aplication.Services.Curso
{
    public interface ICursoService
    {
         Task<CursoGetDTO?> GetByIdAsync(int id);
        Task<List<CursoGetDTO?>> GetAllAsync();
        Task<CursoGetDTO> AddAsync(CursoPostDTO cursoPostDTO);
        Task<CursoGetDTO> UpdateAsync(CursoPutDTO cursoPutDTO);
        Task<CursoGetDTO?> DeleteAsync(int id);
    }
}