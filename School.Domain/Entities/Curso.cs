using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Domain.Entities
{
    public class Curso
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }

        public int CargaHoraria { get; set; }
        public decimal Preco {get; set;}

        public bool Excluido { get; set; }
        public ICollection<Turma>? Turma { get; set; }
    }
}