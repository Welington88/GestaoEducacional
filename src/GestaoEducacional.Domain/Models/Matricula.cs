using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GestaoEducacional.Domain.Entities.Base;

namespace GestaoEducacional.Domain.Entities;
#nullable disable
public class Matricula : Entity<Matricula>
{
    [Key]
    public int IdMatricula { get; set; }

    [Required]
    public int IdDisciplina { get; set; }
    [Required]
    public int MatriculaAluno { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual Aluno Aluno { get; set; }
    
    [JsonIgnore]
    [NotMapped]
    public virtual Disciplina Disciplina { get; set; }
}

