using System;
using System.Collections.Generic;

namespace CECVS.Vacay.Domain.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            Funcionarios = new HashSet<Funcionario>();
        }

        public int IdDepartamento { get; set; }
        public string NoDepartamento { get; set; } = null!;

        public virtual ICollection<Funcionario> Funcionarios { get; set; }
    }
}
