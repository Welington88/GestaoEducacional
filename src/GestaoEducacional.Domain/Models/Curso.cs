using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GestaoEducacional.Domain.Entities.Base;

namespace GestaoEducacional.Domain.Entities;
#nullable disable
public class Curso : Entity<Curso>
{
	[Key]
	public int IdCurso { get; set; }

    [Required]
    [StringLength(50)]
    public string DescricaoCurso { get; set; }
	
	[JsonIgnore]
	[NotMapped]
    public virtual ICollection<Disciplina> Disciplina { get; set; }
	
}

