using System;
using GestaoEducacional.Domain.Entities;

namespace GestaoEducacional.CC.Dto.DTOs;
#nullable disable
public class DisciplinaDto
{
	public int IdDisciplina { get; set; }

	public string DescricaoDisciplina { get; set; }

    public List<AlunoDto> Alunos { get; set; }

    public ProfessorDto Professor { get; set; }
}