using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Api.Contracts.Validations.Ferias
{
    public class FinalSemanaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dataValidando = (DateTime)value;

            if (dataValidando.DayOfWeek == DayOfWeek.Saturday || dataValidando.DayOfWeek == DayOfWeek.Sunday)
            {
                return new ValidationResult("A data de início não pode ser um final de semana.");
            }

            return ValidationResult.Success;
        }
    }
}
