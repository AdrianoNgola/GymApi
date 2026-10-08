using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Aplication.DTOs
{
    public class CursoPostDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O campo Descrição deve ter no máximo 100 caracteres.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo Carga Horária é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo Carga Horária deve ser um número positivo.")]
        public int CargaHoraria { get; set; }

        [Required(ErrorMessage = "O campo Preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O campo Preço deve ser um número positivo.")]
        public decimal Preco { get; set; }

    

    }
}