using System;

namespace GestaoEducacional.CC.Dto.ViewModels;
#nullable disable
public class NotaViewModel
{
    public int IdNota { get; set; }

    public DisciplinaNotaViewModel Disciplina { get; set; }

    public AlunoNotaViewModel Aluno { get; set; }

    public float ValorNota { get; set; }
}

