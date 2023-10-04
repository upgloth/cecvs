using CECVS.Vacay.Api.Contracts.Models;
using CECVS.Vacay.Domain.Interfaces.Services;
using CECVS.Vacay.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Api.Contracts.Validations.Ferias
{
    public class PeriodoValidoAttribute : ValidationAttribute
    {
        private readonly int _intervalo;

        public PeriodoValidoAttribute(int Intervalo)
        {
            _intervalo = Intervalo;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Valida apenas se é 7 dias após o último período, ignora ferias antes das últimas
            // Pega ultima, soma 7, se novo inicio maior ok

            var dtInicioNovaFerias = (DateTime)value;

            // Pega o objeto ferias em validação
            var ferias = (FeriasDTO)validationContext.ObjectInstance;

            // Carrega o ferias service
            var feriasService = (IFeriasService)validationContext.GetService(typeof(IFeriasService));

            var feriasList = feriasService.GetFeriasByIdFuncionarioAsync(ferias.IdFuncionario).Result;

            var ultimaFerias = feriasList.Any()? feriasList.OrderBy(x => x.DtInicio).Last():null;

            if (ultimaFerias != null)
            {
                if (dtInicioNovaFerias < ultimaFerias.DtInicio)
                {
                    return new ValidationResult($"Novas férias não pode ser agendada antes da última já lançada.");
                }

                var dtUltimaFeriasFimComIntervalo = ultimaFerias.DtInicio.AddDays(ultimaFerias.QtDias + this._intervalo);

                if (dtInicioNovaFerias <= dtUltimaFeriasFimComIntervalo)
                {
                    return new ValidationResult($"Intervalo mínimo entre férias não respeitado, {this._intervalo} Dias.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
