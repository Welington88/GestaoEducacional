using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GestaoEducacional.Domain.Entities.Base;

namespace GestaoEducacional.Domain.Entities;
#nullable disable
public class Disciplina : Entity<Disciplina>
{
    [Key]
    public int IdDisciplina { get; set; }

    [Required]
    [StringLength(50)]
    public string DescricaoDisciplina { get; set; }

    [Required]
    public int IdProfessor { get; set; }
    [Required]
    public int IdCurso { get; set; }

    [JsonIgnore]
    public virtual Professor Professor { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual Curso Curso { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual ICollection<Aluno> Aluno { get; set; }
}