using System;

namespace GestaoEducacional.CC.Dto.ViewModels;
#nullable disable
public class DisciplinaViewModel
{
    public int IdDisciplina { get; set; }

    public string DescricaoDisciplina { get; set; }

    public ProfessorDisciplinaViewModel Professor { get; set; }

    public CursoDisciplinaViewModel Curso { get; set; }

    public int QuantidadeAlunos { get; set; }

    public List<NotaDisciplinaViewModel> Alunos { get; set; }
}