using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GestaoEducacional.Domain.Entities.Base;

namespace GestaoEducacional.Domain.Entities;
#nullable disable
public class Professor : Entity<Professor>
{
    [Key]
    public int IdProfessor { get; set; }

    public string Nome { get; set; }

    public DateTime DataNascimento { get; set; }

    public decimal Salario { get; set; }

    [JsonIgnore]
    public virtual ICollection<Disciplina> Disciplina { get; set; }
}

