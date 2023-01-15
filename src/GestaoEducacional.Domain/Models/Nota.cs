using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GestaoEducacional.Domain.Entities.Base;

namespace GestaoEducacional.Domain.Entities;
#nullable disable
public class Nota : Entity<Nota> 
{
    [Key]
    public int IdNota { get; set; }

    [Required]
    public int Disciplina { get; set; }
    [Required]
    public int MatriculaAluno { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public float ValorNota { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual Aluno Aluno { get; set; }
    
    [JsonIgnore]
    [NotMapped]
    public virtual Disciplina Disciplinas { get; set; }
}

