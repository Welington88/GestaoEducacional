using System;

namespace GestaoEducacional.CC.Dto.DTOs;
#nullable disable
public class NotaDto
{
    public int IdNota { get; set; }

    public DisciplinaDto Disciplina { get; set; }

    public AlunoDto Aluno { get; set; }

    public float ValorNota { get; set; }
}

