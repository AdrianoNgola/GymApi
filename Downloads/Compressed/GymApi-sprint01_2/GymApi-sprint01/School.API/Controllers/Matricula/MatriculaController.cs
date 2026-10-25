using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Aplication.DTOs.Matricula;
using School.Aplication.Services.Matricula.Interface;

namespace School.API.Controllers.Matricula
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class MatriculaController : Controller
    {
        private readonly IMatriculaService _matriculaService;

        public MatriculaController(IMatriculaService matriculaService)
        {
            _matriculaService = matriculaService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(MatriculaPostDTO matriculaPostDTO)
        {  
            var matricula = await _matriculaService.AddAsync(matriculaPostDTO);
                if(matricula != null)
                   return Ok(new {message = "Matricula criada com sucesso!"});;
                // Erros de validação ou dependências não inicializadas
            return BadRequest(new { message = "Dados inválidos para criação da matrícula." });
            
        }

         [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matricula = await _matriculaService.GetAllAsync();
            if(matricula == null)
                return NotFound($"Não foi encontrado nenhuma matricula.");
            return Ok(matricula);
        }

         [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var matricula = await _matriculaService.GetByIdAsync(id);
            if (matricula == null)
            {
                return NotFound($"Matricula com ID {id} não encontrada.");
            }
            return Ok(matricula);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTurma(MatriculaPutDTO matriculaPut)
        {
            var matriculaAtualizada = await _matriculaService.UpdateAsync(matriculaPut);
            if (matriculaAtualizada == null)
            {
                return BadRequest("Não foi possível atualizar a matricula.");
            }
            return Ok(new {message = "Matricula atualizada com sucesso!"});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            var matriculaDeletada = await _matriculaService.DeleteAsync(id);
            if (matriculaDeletada == null)
            {
                return NotFound($"Matricula com ID {id} não encontrada.");
            }
            return Ok(new {message = "Matricula deletada com sucesso!"});
        }


    }
}