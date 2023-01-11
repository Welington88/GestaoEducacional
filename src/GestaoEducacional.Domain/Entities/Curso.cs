using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GestaoEducacional.Domain.Entities.Base;

namespace GestaoEducacional.Domain.Entities;
#nullable disable
public class Curso : Entity<Curso>
{
	[Key]
	public int IdCurso { get; set; }

	public string DescricaoCurso { get; set; }
	
	[JsonIgnore]
    public virtual ICollection<Disciplina> Disciplina { get; set; }
	
}

