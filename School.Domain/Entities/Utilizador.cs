using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace School.Domain.Entities
{
    public class Utilizador
    {
        public Guid UtilizadorId { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public byte[]? PalavraPasseHash { get; set; }
        public byte[]? PalavraPasseSalt { get; set; }
        public DateTime DataNascimento { get; set; }
        public Guid GeneroId { get; set; }
        public Guid PerfilId { get; set; }
        public bool Activo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public DateTime DataActualizacao { get; set; }

        public Perfil? Perfil { get; set; }
        public Genero? Genero { get; set; }

    }
}