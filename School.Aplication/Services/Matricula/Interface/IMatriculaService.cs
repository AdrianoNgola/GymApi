using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Aplication.DTOs.Matricula;

namespace School.Aplication.Services.Matricula.Interface
{
    public interface IMatriculaService
    {
        
        Task<DetalheMatriculaDTO?> GetByIdAsync(int id);
        Task<List<DetalheMatriculaDTO?>> GetAllAsync();
        Task<MatriculaGetDTO> AddAsync(MatriculaPostDTO matriculaPostDTO);
        Task<MatriculaGetDTO> UpdateAsync(MatriculaPutDTO matriculaPutDTO);
        Task<MatriculaGetDTO?> DeleteAsync(int id);
    }
}