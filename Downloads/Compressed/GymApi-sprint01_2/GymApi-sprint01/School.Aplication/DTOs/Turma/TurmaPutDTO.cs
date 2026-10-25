using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Aplication.DTOs.Turma
{
    public class TurmaPutDTO
    {
        [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O campo Descrição deve ter no máximo 150 caracteres.")]
        public string? Descricao { get; set; }
    }
}