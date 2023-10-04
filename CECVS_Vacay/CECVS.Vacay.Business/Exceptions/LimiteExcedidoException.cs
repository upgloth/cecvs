using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Business.Exceptions
{
    public class LimiteExcedidoException : Exception
    {
        public LimiteExcedidoException(string? message) : base(message) { }
    }
}
