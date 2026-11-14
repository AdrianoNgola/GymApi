using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Aplication.DTOs.Cliente
{
    public class ClientePutDTO
    {
        public string? Nome {get; set;}
        public string? Telefone {get; set;}
        public string? Endereco {get; set;}
        public DateTime DataNacimento {get; set;}
        public Guid GeneroId {get; set;}
    }
}