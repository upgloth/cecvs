using System;
using System.Collections.Generic;

namespace CECVS.Vacay.Domain.Models
{
    public partial class Ferias
    {
        public int IdFerias { get; set; }
        public int IdFuncionario { get; set; }
        public DateTime DtInicio { get; set; }
        public int QtDias { get; set; }

        public virtual Funcionario IdFuncionarioNavigation { get; set; } = null!;
    }
}
