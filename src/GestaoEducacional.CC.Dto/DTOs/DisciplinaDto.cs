using System;

namespace GestaoEducacional.CC.Dto.DTOs;
#nullable disable
public class DisciplinaDto
{
	public int IdDisciplina { get; set; }

	public string DescricaoDisciplina { get; set; }

    public ProfessorDto Professor { get; set; }

    public CursoDto Curso { get; set; }
}