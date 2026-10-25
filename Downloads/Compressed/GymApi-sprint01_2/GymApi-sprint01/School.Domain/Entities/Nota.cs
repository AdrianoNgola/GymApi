using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Domain.Entities
{
    public class Nota
    {
        public int Id { get; set; }
        public int MatriculaId { get; set; }
        public decimal ValorNota { get; set; }
        public bool Aprovado { get; set; }
        public bool Excluido { get; set; }

        public Matricula? Matricula { get; set; }
    }
}