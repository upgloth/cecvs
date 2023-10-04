using CECVS.Vacay.Api.Contracts.Models;
using CECVS.Vacay.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Api.Contracts.Validations.Funcionario
{
    public class DepartamentoValidoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var id_departamento = (int)value;

            var departamentoService = (IDepartamentoService)validationContext.GetService(typeof(IDepartamentoService));

            var departamento = departamentoService.GetDepartamentoByIdAsync(id_departamento).Result;

            if (departamento == null)
            {
                return new ValidationResult($"Departamento inválido.");
            }

            return ValidationResult.Success;
        }
    }
}
