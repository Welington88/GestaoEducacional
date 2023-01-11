using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GestaoEducacional.Domain.Entities.Base;

namespace GestaoEducacional.Domain.Entities;
#nullable disable
public class Matricula : Entity<Matricula>
{
    [Key]
    public int IdMatricula { get; set; }

    public int IdDisciplina { get; set; }

    public int MatriculaAluno { get; set; }

    [JsonIgnore]
    public virtual Aluno Aluno { get; set; }
    
    [JsonIgnore]
    public virtual Disciplina Disciplina { get; set; }
}

