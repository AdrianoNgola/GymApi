
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.API.Models;
using School.Aplication.DTOs.Utilizador;
using School.Aplication.Services.User.Interface;
using School.Domain.Account;


namespace School.API.Controllers.Utilizador
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtilizadorController : Controller
    {
         private readonly IUtilizadorService _utilizadorService;
         private readonly IAuthenticate _authenticate;

        public UtilizadorController(IUtilizadorService utilizadorService, IAuthenticate authenticate)
        {
            _utilizadorService = utilizadorService;
            _authenticate = authenticate;
        }

        [HttpPost("register")]
       // [Authorize(Roles = "Administrador")]
        
        public async Task<ActionResult> CreateUser(UtilizadorPostDTO utilizadorPostDTO)
        {
            var userExists = await _authenticate.UserExists(utilizadorPostDTO.Email);
            if (userExists)
            {
                return BadRequest("Já existe um utilizador com esse e-mail!.");
            }
            var utilizador = await _utilizadorService.AddAsync(utilizadorPostDTO);
            if (utilizador == null)
            {
                return BadRequest("Não foi possível criar o utilizador.");
            }

            var token = _authenticate.GenerateToken(utilizador.Email?.ToLower(), utilizador.Id.ToString(), utilizadorPostDTO.PerfilId);

            return Ok(new{Nome = utilizador.Nome, Token = token});
        }

         [HttpGet]
         [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
        {
            var utilizador = await _utilizadorService.GetAllAsync(paginationParams.page, paginationParams.pageSize);


            if (utilizador == null || !utilizador.Any())
               return NotFound("Não foi encontrado nenhum Utilizador.");

            var result = new
            {
                items = utilizador,                // lista da página
                page = utilizador.CurrentPage,     // página atual
                pageSize = utilizador.PageSize,    // tamanho da página
                total = utilizador.TotalCount,     // total de registros
                totalPages = utilizador.TotalPages // total de páginas
            };

            return Ok(result);
        }

      

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador")]
         public async Task<IActionResult> GetById(Guid id)
        {
            var utilizador = await _utilizadorService.GetByIdAsync(id);
            if (utilizador == null)
            {
                return NotFound($"Utilizador com ID {id} não encontrado.");
            }
            return Ok(utilizador);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteUtilizador(Guid id)
        {
            var userDeletado = await _utilizadorService.DeleteAsync(id);
            if (userDeletado == null)
            {
                return NotFound($"Utilizador com ID {id} não encontrado.");
            }
            return Ok(new {message = "Usuário deletado com sucesso!"});
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActivarUtilizador(Guid id)
        {
            var userDeletado = await _utilizadorService.ActivarAsync(id);
            if (userDeletado == null)
            {
                return NotFound($"Utilizador com ID {id} não encontrado.");
            }
            return Ok(new {message = "Usuário activo com sucesso!"});
        }

        
         [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id,UtilizadorPutDTO utilizadorPutDTO)
        {
           
            var utilizadorAtualizado = await _utilizadorService.UpdateAsync(utilizadorPutDTO, id);
            if (utilizadorAtualizado == null)
            {
                return NotFound($"Utilizador com ID {id} não encontrado.");
            }
            return Ok(new {message = "Utilizador atualizado com sucesso!"});
        }
    }
}