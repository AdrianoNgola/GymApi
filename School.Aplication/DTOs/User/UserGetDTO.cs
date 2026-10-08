using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Aplication.DTOs.User
{
    public class UserGetDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Perfil { get; set; }
        public bool Excluido {get; set;}
       
    }
}