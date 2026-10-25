using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo Senha deve ter no máximo 100 caracteres.")]
         [EmailAddress( ErrorMessage = "Este E-mail é inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [MinLength(8, ErrorMessage = "O campo Senha deve ter no máximo 8 caracteres.")]
        [MaxLength(100, ErrorMessage = "O campo Senha deve ter no máximo 100 caracteres.")]
        public string? Senha { get; set; } 
       
    }
}