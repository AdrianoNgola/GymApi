using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Models
{
    public class PaginationParams
    {
        [Range(1, int.MaxValue, ErrorMessage = "A pagina deve ser maior que 0")]
        public int page { get; set; }
        [Range(8, int.MaxValue, ErrorMessage = "O tamanho da pagina deve ser maior que 0")]
        public int pageSize { get; set; }
    }
}