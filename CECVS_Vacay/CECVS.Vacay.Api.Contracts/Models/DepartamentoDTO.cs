using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Api.Contracts.Models
{
    public class DepartamentoDTO
    {
        public int IdDepartamento { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nome deve ter no minímo 3 e no maxímo 50 caracteres.")]
        public string NoDepartamento { get; set; } = null!;
    }
}
