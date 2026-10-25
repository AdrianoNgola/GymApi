using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Aplication.DTOs.Matricula
{
    public class MatriculaGetDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TurmaId { get; set; }
        public DateTime DataMatricula { get; set; }
        public DateTime DataExp { get; set; }
        public bool Ativo { get; set; }
    }
}