using CECVS.Vacay.Api.Contracts.Models;
using CECVS.Vacay.Domain.Interfaces.Services;
using CECVS.Vacay.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Api.Contracts.Validations.Funcionario
{
    public class DataAdmissaoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dataAdmissao = (DateTime)value;

            if (dataAdmissao > DateTime.Now)
            {
                return new ValidationResult($"Data de admissão não pode ser no futuro.");
            }

            return ValidationResult.Success;
        }
    }
}
