using System;

namespace GestaoEducacional.Domain.Entities;
#nullable disable
public class DisciplinaViewModel
{
	public int IdDisciplina { get; set; }

	public string DescricaoDisciplina { get; set; }

    public int IdCurso { get; set; }

    public int IdProfessor { get; set; }
}