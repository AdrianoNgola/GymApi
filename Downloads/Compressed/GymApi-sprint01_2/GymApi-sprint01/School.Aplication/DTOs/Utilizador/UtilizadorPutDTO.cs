
using System.ComponentModel.DataAnnotations;


namespace School.Aplication.DTOs.Utilizador
{
    public class UtilizadorPutDTO
    {
     
        public string? Nome { get; set; }
        public string? Email { get; set; }
        
        public DateTime DataNascimento { get; set; }
      
        public Guid GeneroId { get; set; }
        public Guid PerfilId { get; set; }

        public string? PalavraPasse { get; set; }
       
    }
}