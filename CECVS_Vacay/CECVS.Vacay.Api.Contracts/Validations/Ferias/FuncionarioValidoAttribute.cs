using CECVS.Vacay.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Api.Contracts.Validations.Ferias
{
    public class FuncionarioValidoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var id_funcionario = (int)value;

            // Carrega o ferias service
            var departamentoService = (IFuncionarioService)validationContext.GetService(typeof(IFuncionarioService));

            // Verifica ultima ferias e proxima, se existirem
            var funcionario = departamentoService.GetFuncionarioByIdAsync(id_funcionario).Result;

            if (funcionario == null)
            {
                return new ValidationResult($"Funcionário inválido.");
            }

            return ValidationResult.Success;
        }
    }
}
