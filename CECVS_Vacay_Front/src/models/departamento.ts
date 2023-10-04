// public int IdDepartamento { get; set; }

//       [Required(ErrorMessage = "Nome é obrigatório.")]
//       [StringLength(50, MinimumLength = 3, ErrorMessage = "Nome deve ter no minímo 3 e no maxímo 50 caracteres.")]
//       public string NoDepartamento { get; set; } = null!;


interface IDepartamentoDTO {
  idDepartamento: number,
  noDepartamento: string
}

export type { IDepartamentoDTO };