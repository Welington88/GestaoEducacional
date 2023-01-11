using System;

namespace GestaoEducacional.CC.Dto.ViewModels;
#nullable disable
public class AlunoViewModel
{
    public int MatriculaAluno { get; set; }

    public string Nome { get; set; }

    public DateTime DataNascimento { get; set; }

    public List<NotaViewModel> Notas { get; set; }

}

