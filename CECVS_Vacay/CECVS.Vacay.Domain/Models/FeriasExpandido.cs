using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Domain.Models
{
    public class FeriasExpandido
    {
        public int IdFerias { get; set; }
        public int IdFuncionario { get; set; }
        public string CoMatricula { get; set; } = null!;
        public string NoFuncionario { get; set; } = null!;
        public DateTime DtInicio { get; set; }
        public int QtDias { get; set; }
    }
}
