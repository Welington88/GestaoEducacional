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

    public int Disciplina { get; set; }

    public int MatriculaAluno { get; set; }

    public float ValorNota { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual Aluno Aluno { get; set; }
    
    [JsonIgnore]
    [NotMapped]
    public virtual Disciplina Disciplinas { get; set; }
}

