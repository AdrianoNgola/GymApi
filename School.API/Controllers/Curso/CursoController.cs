using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using School.Aplication.DTOs;
using School.Aplication.Services.Curso;

namespace School.API.Controllers.Curso
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : Controller
    {
        private readonly ICursoService _curso;

        public CursoController(ICursoService curso)
        {
            _curso = curso;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll()
        {
            var cursos = await _curso.GetAllAsync();
            if(cursos == null)
                return NotFound($"Não foi encontrado nenhum curso.");
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador, Aluno")]
        public async Task<IActionResult> GetById(int id)
        {
            var curso = await _curso.GetByIdAsync(id);
            if (curso == null)
            {
                return NotFound($"Curso com ID {id} não encontrado.");
            }
            return Ok(curso);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CreateCurso(CursoPostDTO cursoPost)
        {
            var NovoCurso = await _curso.AddAsync(cursoPost);
            if(NovoCurso == null)
            {
                return BadRequest("Não foi possível criar o curso.");
            }
            return Ok(new {message = "Curso criado com sucesso!"});
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateCurso(CursoPutDTO cursoPut)
        {
            var cursoAtualizado = await _curso.UpdateAsync(cursoPut);
            if (cursoAtualizado == null)
            {
                return NotFound();
            }
            return Ok(new {message = "Curso atualizado com sucesso!"});
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var cursoDeletado = await _curso.DeleteAsync(id);
            if (cursoDeletado == null)
            {
                return NotFound();
            }
            return Ok(new {message = "Curso deletado com sucesso!"});
        }

    }
}