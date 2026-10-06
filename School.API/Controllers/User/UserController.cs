using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using School.API.Extantions;
using School.API.Models;
using School.Aplication.DTOs.User;
using School.Aplication.Services.User.Interface;
using School.Domain.Account;

namespace School.API.Controllers.Curso
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _user;
         private readonly IAuthenticate _authenticate;

        public UserController(IUserService user, IAuthenticate authenticate)
        {
            _user = user;
            _authenticate = authenticate;
        }

        [HttpPost("register")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> CreateUser(UserPostDTO userPostDTO)
        {
            var userExists = await _authenticate.UserExists(userPostDTO.Email);
            if (userExists)
            {
                return BadRequest("Já existe um usuário com esse e-mail!.");
            }
            var user = await _user.AddAsync(userPostDTO);
            if (user == null)
            {
                return BadRequest("Não foi possível criar o usuário.");
            }

            var token = _authenticate.GenerateToken(user.Email.ToLower(), user.Id, user.Perfil);

            return Ok(new{Nome = user.Nome, Token = token});
        }

         [HttpGet]
         [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
        {
            var users = await _user.GetAllAsync(paginationParams.page, paginationParams.pageSize);

            if (users == null || !users.Any())
               return NotFound("Não foi encontrado nenhum Aluno.");

            var result = new
            {
                items = users,                // lista da página
                page = users.CurrentPage,     // página atual
                pageSize = users.PageSize,    // tamanho da página
                total = users.TotalCount,     // total de registros
                totalPages = users.TotalPages // total de páginas
            };

            return Ok(result);
        }

         [HttpGet("status")]
         //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll()
        {
            var User = await _user.GetAllAsync();
            if(User == null)
                return NotFound($"Não foi encontrado nenhum Aluno.");
            return Ok(User);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador, Aluno")]
         public async Task<IActionResult> GetById(int id)
        {
            var User = await _user.GetByIdAsync(id);
            if (User == null)
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }
            return Ok(User);
        }

        [HttpPost("login")]
        
         public async Task<ActionResult> GetUserToken(string email, string passeWord)
        {
            var user = await _authenticate.GetUserByEmail(email);
            var userValido = await _authenticate.UserAuthenticate(email, passeWord);
            if (!userValido)
            {
                return BadRequest(new { Message = "Usuário ou senha inválida." });
            }

            var token = _authenticate.GenerateToken(user.Email.ToLower(), user.Id, user.Perfil);

            return Ok(new { 
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
                Perfil = user.Perfil,
                Token = token 
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userDeletado = await _user.DeleteAsync(id);
            if (userDeletado == null)
            {
                return NotFound();
            }
            return Ok(new {message = "Usuário deletado com sucesso!"});
        }


        [HttpGet("Teste")]
        [Authorize(Roles = "Aluno")]
        public async Task<ActionResult> Teste()
        {
           
            return Ok(new{Message = "Teste de endpoint bem-sucedido!"});
        }
        
         [HttpPut("{id}")]
         [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateUser(int id,UserPutDTO cursoPutDTO)
        {
            cursoPutDTO.Id = id;
            var cursoAtualizado = await _user.UpdateAsync(cursoPutDTO);
            if (cursoAtualizado == null)
            {
                return NotFound();
            }
            return Ok(new {message = "Curso atualizado com sucesso!"});
        }
      
    }
}