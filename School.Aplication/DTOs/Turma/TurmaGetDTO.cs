using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Domain.Entities;

namespace School.Aplication.DTOs.Turma
{
    public class TurmaGetDTO
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        

        
    }
}