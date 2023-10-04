//  public int IdFuncionario { get; set; }

//         [Required(ErrorMessage = "Matricula é obrigatória.")]
//         [RegularExpression(@"^[Cc]\d{6}$", ErrorMessage = "Matrícula deve estar no formato CXXXXXX (ex: C123123|c123123).")]
//         public string CoMatricula { get; set; } = null!;

//         [Required(ErrorMessage = "Nome é obrigatório.")]
//         [StringLength(50, MinimumLength = 3, ErrorMessage = "Nome deve ter no minímo 3 e no maxímo 50 caracteres.")]
//         public string NoFuncionario { get; set; } = null!;

//         [Required(ErrorMessage = "Data de admissão é obrigatória.")]
//         [DataType(DataType.Date, ErrorMessage = "Data de admissão deve estar em formato de data.")]
//         [DataAdmissao]
//         public DateTime DtAdmissao { get; set; }

//         [Required(ErrorMessage = "Departamento é obrigatório.")]
//         [DepartamentoValido]
//         public int IdDepartamento { get; set; }

interface IFuncionarioDTO {
    idFuncionario: number,
    coMatricula: string,
    noFuncionario: string,
    dtAdmissao: string,
    idDepartamento: number,
    noDepartamento: string,
}

export type { IFuncionarioDTO };
