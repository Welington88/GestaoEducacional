using System;

namespace GestaoEducacional.CC.Dto.ViewModels;
#nullable disable
public class ProfessorViewModel
{
    public int IdProfessor { get; set; }

    public string Nome { get; set; }

    public DateTime DataNascimento { get; set; }

    public decimal Salario { get; set; }
}

