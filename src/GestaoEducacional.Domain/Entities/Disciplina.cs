using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GestaoEducacional.Domain.Entities.Base;

namespace GestaoEducacional.Domain.Entities;
#nullable disable
public class Disciplina : Entity<Disciplina>
{
    [Key]
    public int IdDisciplina { get; set; }

	public string DescricaoDisciplina { get; set; }

    public int IdProfessor { get; set; }

    public int IdCurso { get; set; }

    [JsonIgnore]
    public virtual Professor Professor { get; set; }

    [JsonIgnore]
    public virtual Curso Curso { get; set; }

    [JsonIgnore]
    public virtual ICollection<Aluno> Aluno { get; set; }
}