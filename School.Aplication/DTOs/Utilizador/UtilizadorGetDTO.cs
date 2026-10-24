

namespace School.Aplication.DTOs.Utilizador
{
    public class UtilizadorGetDTO
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        
        public DateTime DataNascimento { get; set; }
        public string? Genero { get; set; }
        public string? Perfil { get; set; }

        public bool Activo { get; set; }
    }
}