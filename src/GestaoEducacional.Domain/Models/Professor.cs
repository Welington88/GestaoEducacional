using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GestaoEducacional.Domain.Entities.Base;

namespace GestaoEducacional.Domain.Entities;
#nullable disable
public class Professor : Entity<Professor>
{
    [Key]
    public int IdProfessor { get; set; }

    [Required]
    [StringLength(50)]
    public string Nome { get; set; }

    [Required]
    public DateTime DataNascimento { get; set; }

    [Range(0,100)]
    public decimal Salario { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual ICollection<Disciplina> Disciplina { get; set; }
}

