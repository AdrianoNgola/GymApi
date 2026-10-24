
using System.ComponentModel.DataAnnotations;


namespace School.Aplication.DTOs.Utilizador
{
    public class UtilizadorPostDTO
    {
       [Required(ErrorMessage = "O campo Nome é obrigatório.")]
       [MaxLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo E-mail deve ter no máximo 100 caracteres.")]
        [EmailAddress( ErrorMessage = "Este E-mail é inválido.")]
        public string? Email { get; set; }
        
        public DateTime DataNascimento { get; set; }
        public Guid GeneroId { get; set; }

        [Required(ErrorMessage = "O campo perfil é obrigatório.")]
        public Guid PerfilId { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [MinLength(8, ErrorMessage = "O campo Senha deve ter no máximo 8 caracteres.")]
        [MaxLength(100, ErrorMessage = "O campo Senha deve ter no máximo 100 caracteres.")]
        public string? PalavraPasse { get; set; }
        
    }
}