using System;

namespace GestaoEducacional.CC.Dto.DTOs;
#nullable disable
public class ProfessorDto
{
    public int IdProfessor { get; set; }

    public string Nome { get; set; }

    public DateTime DataNascimento { get; set; }

    public decimal Salario { get; set; }
}

