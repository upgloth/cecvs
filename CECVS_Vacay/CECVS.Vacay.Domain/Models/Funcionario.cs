using System;
using System.Collections.Generic;

namespace CECVS.Vacay.Domain.Models
{
    public partial class Funcionario
    {
        public Funcionario()
        {
            Ferias = new HashSet<Ferias>();
        }

        public int IdFuncionario { get; set; }
        public string CoMatricula { get; set; } = null!;
        public string NoFuncionario { get; set; } = null!;

        public DateTime DtAdmissao { get; set; }

        public int IdDepartamento { get; set; }

        public virtual ICollection<Ferias> Ferias { get; set; }

        public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
    }
}
