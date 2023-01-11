using System;

namespace GestaoEducacional.CC.Dto.DTOs;
#nullable disable
public class CursosDto
{
	public int IdCurso { get; set; }

	public string DescricaoCurso { get; set; }

	public List<DisciplinaDto> Disciplinas { get; set; }
}

