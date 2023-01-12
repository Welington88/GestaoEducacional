using System;
namespace GestaoEducacional.CC.Dto.DTOs;
#nullable disable
public class CursoAlunoDto
{
	public int IdCurso { get; set; }

	public string DescricaoCurso { get; set; }

	public int QtdProfessores { get; set; }

	public int QtdAlunos { get; set; }

	public float MediaAlunos { get; set; }
}

