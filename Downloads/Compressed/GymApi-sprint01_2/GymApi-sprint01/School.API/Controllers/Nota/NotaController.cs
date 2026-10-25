using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Aplication.DTOs.Nota;
using School.Aplication.Services.Nota.Interface;

namespace School.API.Controllers.Nota
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaController : Controller
    {
         private readonly INotaService _nota;

        public NotaController(INotaService nota)
        {
            _nota = nota;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll()
        {
            var nota = await _nota.GetAllAsync();
            if(nota == null)
                return NotFound($"Não foi encontrado nenhum nota.");
            return Ok(nota);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador, Aluno")]
        public async Task<IActionResult> GetById(int id)
        {
            var nota = await _nota.GetByIdAsync(id);
            if (nota == null)
            {
                return NotFound($"Nota com ID {id} não encontrado.");
            }
            return Ok(nota);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CreateNota(NotaPostDTO notaPost)
        {
            var NovoNota = await _nota.AddAsync(notaPost);
            if(NovoNota == null)
            {
                return BadRequest("Não foi possível criar o curso.");
            }
            return Ok(new {message = "Nota criado com sucesso!"});
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateNota(NotaPutDTO notaPut)
        {
            var notaAtualizado = await _nota.UpdateAsync(notaPut);
            if (notaAtualizado == null)
            {
                return NotFound();
            }
            return Ok(new {message = "Nota atualizado com sucesso!"});
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteNota(int id)
        {
            var notaDeletado = await _nota.DeleteAsync(id);
            if (notaDeletado == null)
            {
                return NotFound();
            }
            return Ok(new {message = "Nota deletado com sucesso!"});
        }

    }
}