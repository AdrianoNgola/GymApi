using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Aplication.DTOs.Matricula
{
    public class MatriculaPostDTO
    {
        [Required(ErrorMessage = "O campo Id do Usuário é obrigatório.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "O campo Id da Turma é obrigatório.")]
        public int TurmaId { get; set; }
        [Required(ErrorMessage = "O campo Data de Expiração é obrigatório.")]
        public DateTime DataExp { get; set; }
    }
}