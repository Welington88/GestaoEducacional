using System;

namespace GestaoEducacional.CC.Dto.ViewModels;
#nullable disable
public class DisciplinaViewModel
{
    public int IdDisciplina { get; set; }

    public string DescricaoDisciplina { get; set; }

    public List<string> Alunos { get; set; }

    public string Professor { get; set; }

    public string Curso { get; set; }
}