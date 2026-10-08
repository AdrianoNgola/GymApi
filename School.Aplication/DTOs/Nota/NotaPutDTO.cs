using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Aplication.DTOs.Nota
{
    public class NotaPutDTO
    {
         [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Matrícula é obrigatório.")]
        public int MatriculaId { get; set; }
        [Required(ErrorMessage = "O campo Valor da Nota é obrigatório.")]
        public decimal ValorNota { get; set; }
        
    }
}