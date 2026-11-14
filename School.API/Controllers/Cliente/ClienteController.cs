using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.API.Models;
using School.Aplication.DTOs.Cliente;
using School.Aplication.Services.Cliente.Interface;

namespace School.API.Controllers.Cliente
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

         [HttpPost]
        [Authorize(Roles = "Administrador, Recepcionista")]
        public async Task<IActionResult> CreateNota(ClientePostDTO clientePost)
        {
            var cliente = await _clienteService.AddAsync(clientePost);
            if(cliente == null)
            {
                return BadRequest("Não foi possível adicionar o cliente.");
            }
            return Ok(new {message = "Cliente criado com sucesso!"});
        }

         [HttpGet]
         [Authorize(Roles = "Administrador, Recepcionista")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
        {
            var cliente = await _clienteService.GetAllAsync(paginationParams.page, paginationParams.pageSize);


            if (cliente == null || !cliente.Any())
               return NotFound("Não foi encontrado nenhum Cliente.");

            var result = new
            {
                items = cliente,                // lista da página
                page = cliente.CurrentPage,     // página atual
                pageSize = cliente.PageSize,    // tamanho da página
                total = cliente.TotalCount,     // total de registros
                totalPages = cliente.TotalPages // total de páginas
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador, Recepcionista")]
         public async Task<IActionResult> GetById(Guid id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound($"Cliente com ID {id} não encontrado.");
            }
            return Ok(cliente);
        }

         [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador, Recepcionista")]
        public async Task<IActionResult> DeleteCliente(Guid id)
        {
            var clienteDeletado = await _clienteService.DeleteAsync(id);
            if (clienteDeletado == null)
            {
                return NotFound($"Cliente com ID {id} não encontrado.");
            }
            return Ok(new {message = "Cliente deletado com sucesso!"});
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Administrador, Recepcionista")]
        public async Task<IActionResult> ActivarCliente(Guid id)
        {
            var clienteAtivado = await _clienteService.ActivarAsync(id);
            if (clienteAtivado == null)
            {
                return NotFound($"Cliente com ID {id} não encontrado.");
            }
            return Ok(new {message = "Cliente activo com sucesso!"});
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador, Recepcionista")]
        public async Task<IActionResult> UpdateClient(Guid id, ClientePutDTO clientePutDTO)
        {
           
            var clienteAtualizado = await _clienteService.UpdateAsync(clientePutDTO, id);
            if (clienteAtualizado == null)
            {
                return NotFound($"Cliente com ID {id} não encontrado.");
            }
            return Ok(new {message = "Cliente atualizado com sucesso!"});
        }
    }
}