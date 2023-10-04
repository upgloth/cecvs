using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Api.Contracts.Validations.Ferias
{
    public class DataMinimaAttribute : ValidationAttribute
    {
        private readonly int _dias;
        public DataMinimaAttribute(int Dias)
        {
            _dias = Dias;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dataHoje = DateTime.Now;
            var dataValidando = (DateTime)value;

            if (dataHoje.AddDays(5) > dataValidando)
            {
                return new ValidationResult("A data de início deve ser superior à data atual + 5 dias.");
            }

            return ValidationResult.Success;
        }
    }
}
