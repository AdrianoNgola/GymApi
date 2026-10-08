using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Aplication.DTOs.Turma
{
    public class DetalheTurmaDTO
    {
        public int Id { get; set; }
        public CursoGetDTO? Curso { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        
    }
}