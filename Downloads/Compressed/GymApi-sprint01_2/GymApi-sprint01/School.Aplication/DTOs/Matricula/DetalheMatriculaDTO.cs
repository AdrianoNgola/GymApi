using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Aplication.DTOs.Turma;


namespace School.Aplication.DTOs.Matricula
{
    public class DetalheMatriculaDTO
    {
        public int Id {get; set;}
        
        public TurmaGetDTO? Turma { get; set; }
        public DateTime DataMatricula { get; set; }
        public DateTime DataExp { get; set; }
        public bool Ativo { get; set; }
    }
}