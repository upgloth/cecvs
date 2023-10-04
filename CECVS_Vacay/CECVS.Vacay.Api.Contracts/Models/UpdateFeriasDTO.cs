﻿using CECVS.Vacay.Api.Contracts.Validations.Ferias;
using System.ComponentModel.DataAnnotations;

namespace CECVS.Vacay.Api.Contracts.Models
{
    public class UpdateFeriasDTO
    {
        //[Required(ErrorMessage = "Identificador Férias é obrigatório.")]
        public int IdFerias { get; set; }

        [Required(ErrorMessage = "Data de ínicio é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "Data de ínicio deve estar em formato de data.")]
        [DataMinima(Dias: 5)]
        [FinalSemana]
        [PeriodoValidoUpdate(Intervalo: 7)]
        public DateTime DtInicio { get; set; }

        [Required(ErrorMessage = "Quantidade de dias é obrigatório.")]
        [Range(5, 30, ErrorMessage = "Data de ínicio deve ser maior ou igual a 5 e menor ou igual a 30")]
        public int QtDias { get; set; }
    }
}
