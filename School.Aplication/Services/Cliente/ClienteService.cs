using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Aplication.DTOs.Cliente;
using School.Aplication.Services.Cliente.Interface;
using School.Domain.Account;
using School.Domain.Interfaces;
using School.Domain.Pagination;

namespace School.Aplication.Services.Cliente
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
         private readonly IGeneroRepository _genero;
         private readonly IAuthenticate _auth;
          

        public ClienteService(IClienteRepository clienteRepository, IGeneroRepository generoRepository, IAuthenticate auth)
        {
            _clienteRepository = clienteRepository;
            _genero = generoRepository;
            _auth = auth;
           
        }
        public async Task<ClienteGetDTO> ActivarAsync(Guid id)
        {
            try
            {
               var cliente = await _clienteRepository.ActivarAsync(id);
                if(cliente == null)
                {
                    return null;
                }
            
                var genero = await _genero.GetByIdAsync(cliente.GeneroId);
                return new ClienteGetDTO
                {
                    Id = cliente.ClienteId,
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                    Endereco = cliente.Endereco,
                    DataNacimento = cliente.DataNascimento,
                    Genero = genero?.Nome,
                    Activo = cliente.Activo
                };
             }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao activar o cliente: {ex.Message}");
            }
        }

        public async Task<ClienteGetDTO> AddAsync(ClientePostDTO clientePostDTO)
        {
            try
            {

                var cliente = new School.Domain.Entities.Cliente
                {
                    Nome = clientePostDTO.Nome,
                    Telefone = clientePostDTO.Telefone,
                    Endereco = clientePostDTO.Endereco,
                    GeneroId = clientePostDTO.GeneroId,
                    DataNascimento = clientePostDTO.DataNacimento,
                    Activo = true,
                    DataInicio = DateTime.UtcNow,
                    CriadoUtilizadorId = _auth.GetUtilizadorId(),
                };

                var genero = await _genero.GetByIdAsync(cliente.GeneroId);
                var NovoCliente = await _clienteRepository.AddAsync(cliente);

                return new ClienteGetDTO
                {
                    Id = NovoCliente.ClienteId,
                    Nome = NovoCliente.Nome,
                    Telefone = NovoCliente.Telefone,
                    Genero = genero?.Nome,
                    DataNacimento = NovoCliente.DataNascimento,
                    Activo = NovoCliente.Activo

                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao adicionar cliente: {ex.Message}");
            }
        }

        public async Task<ClienteGetDTO> DeleteAsync(Guid id)
        {
             try
            {
               var cliente = await _clienteRepository.DeleteAsync(id);
                if(cliente == null)
                {
                    return null;
                }
                
                var genero = await _genero.GetByIdAsync(cliente.GeneroId);
                return new ClienteGetDTO
                {
                    Id = cliente.ClienteId,
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                    Endereco = cliente.Endereco,
                    DataNacimento = cliente.DataNascimento,
                    Genero = genero?.Nome,
                    Activo = cliente.Activo,
                    
                };
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao Excluir cliente: {ex.Message}");
            }
        }

        public async Task<PageList<ClienteGetDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var cliente = await _clienteRepository.GetAllAsync(pageNumber, pageSize);
                var clienteDtos = new List<ClienteGetDTO>();

                foreach (var clientes in cliente)
                {
                    var genero = await _genero.GetByIdAsync(clientes.GeneroId);
                    clienteDtos.Add(new ClienteGetDTO
                    {
                        Id = clientes.ClienteId,
                        Nome = clientes.Nome,
                        Telefone = clientes.Telefone,
                        Endereco = clientes.Endereco,
                        DataNacimento = clientes.DataNascimento,
                        Genero = genero?.Nome,
                        Activo = clientes.Activo
                    });
                }

                return new PageList<ClienteGetDTO>(clienteDtos, cliente.TotalCount, cliente.CurrentPage, cliente.PageSize);
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao obter clientes: {ex.Message}");
            }
        }

        public async Task<ClienteGetDTO> GetByIdAsync(Guid id)
        {
             try
            {
                var cliente = await _clienteRepository.GetByIdAsync(id);
                if(cliente == null)
                {
                    return null;
                   
                }

                var genero = await _genero.GetByIdAsync(cliente.GeneroId);

                return new ClienteGetDTO
                {
                    Id = cliente.ClienteId,
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                    Endereco = cliente.Endereco,
                    DataNacimento = cliente.DataNascimento,
                    Genero = genero?.Nome,
                    Activo = cliente.Activo
                };
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao obter cliente: {ex.Message}");
            }
        }

        public async Task<ClienteGetDTO> UpdateAsync(ClientePutDTO clientePutDTO, Guid Id)
        {
             try
            {
                var cliente = await _clienteRepository.GetByIdAsync(Id);
                if(cliente == null)
                {
                   return null;
                }
                
                var genero = await _genero.GetByIdAsync(cliente.GeneroId);

                cliente.Nome = clientePutDTO.Nome;
                cliente.Endereco = clientePutDTO.Endereco;
                cliente.DataNascimento = clientePutDTO.DataNacimento;
                cliente.GeneroId = clientePutDTO.GeneroId;
                cliente.Telefone = clientePutDTO.Telefone;
                cliente.ActualUtilizadorId = _auth.GetUtilizadorId();
                cliente.DataActualizacao = DateTime.UtcNow;
                
                
                await _clienteRepository.UpdateAsync(cliente);
                return new ClienteGetDTO
                {
                    Id = cliente.ClienteId,
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                    Endereco = cliente.Endereco,
                    DataNacimento = cliente.DataNascimento,
                    Genero = genero?.Nome,
                    Activo = cliente.Activo
                };
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao actualizar cliente: {ex.Message}");
            }
        }
    }
}