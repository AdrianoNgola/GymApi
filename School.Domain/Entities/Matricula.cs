using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Domain.Entities
{
    public class Matricula
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TurmaId { get; set; }
        public DateTime DataMatricula { get; set; }
        public DateTime DataExp { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Nota>? ValorNota { get; set; }
        public bool Excluido { get; set; }
        public User? User { get; set; }

        public Turma? Turma { get; set; }

    }
}