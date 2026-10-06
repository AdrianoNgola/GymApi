using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Aplication.DTOs
{
    public class CursoGetDTO
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }

        public int CargaHoraria { get; set; }
        public decimal Preco {get; set;}


    }
}