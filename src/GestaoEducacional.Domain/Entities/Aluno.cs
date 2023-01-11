using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GestaoEducacional.Domain.Entities.Base;

namespace GestaoEducacional.Domain.Entities;
#nullable disable
public class Aluno : Entity<Aluno>
{
    [Key]
    public int MatriculaAluno { get; set; }

    public string Nome { get; set; }

    public DateTime DataNascimento { get; set; }

    public virtual ICollection<Nota> Nota { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<Disciplina> Disciplina { get; set; }
}