using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Aplication.DTOs.Turma;
using School.Aplication.Services.Turma.Interface;

namespace School.API.Controllers.Turma
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class TurmaController : Controller
    {
        private readonly ITurmaService _turmaService;
        public TurmaController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(TurmaPostDTO turmaPost)
        {
            if(await _turmaService.ExistsAsync(turmaPost.Nome) == false)
            {
                return BadRequest(new {message = "Já existe um curso com esse nome!"});
            }
            var turma = await _turmaService.AddAsync(turmaPost);
            if (turma == null)
              return BadRequest(new {message = "Não foi possível criar a turma."});

            return Ok(new {message = "Turma criada com sucesso!"});
            
            
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var turmas = await _turmaService.GetAllAsync();
            if(turmas == null)
                return NotFound($"Não foi encontrado nenhuma turma.");
            return Ok(turmas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var turma = await _turmaService.GetByIdAsync(id);
            if (turma == null)
            {
                return NotFound($"Turma com ID {id} não encontrada.");
            }
            return Ok(turma);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTurma(TurmaPutDTO turmaPut)
        {
            var turmaAtualizada = await _turmaService.UpdateAsync(turmaPut);
            if (turmaAtualizada == null)
            {
                return BadRequest("Não foi possível atualizar a turma.");
            }
            return Ok(new {message = "Turma atualizada com sucesso!"});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            var turmaDeletada = await _turmaService.DeleteAsync(id);
            if (turmaDeletada == null)
            {
                return NotFound($"Turma com ID {id} não encontrada.");
            }
            return Ok(new {message = "Turma deletada com sucesso!"});
        }
    }
}