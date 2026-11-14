using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Domain.Entities
{
    public class Cliente
    {
        public Guid ClienteId { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }
        public Genero? genero { get; set; }
        public Guid GeneroId { get; set; }
        public DateTime DataNascimento { get; set; }
        public Utilizador? utilizador { get; set; }
        public Guid CriadoUtilizadorId { get; set; }
         public Guid? ActualUtilizadorId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataActualizacao { get; set; }
        public bool Activo { get; set; }

    }
}