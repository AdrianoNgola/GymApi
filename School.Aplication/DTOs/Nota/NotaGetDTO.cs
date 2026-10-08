using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Aplication.DTOs.Nota
{
    public class NotaGetDTO
    {
        public int Id { get; set; }
        public int MatriculaId { get; set; }
        public decimal ValorNota { get; set; }
        public bool Aprovado { get; set; }
        
    }
}