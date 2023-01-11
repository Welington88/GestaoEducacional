using System;
using GestaoEducacional.Domain.Entities.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestaoEducacional.Domain.Entities;
#nullable disable
public class Professor : Entity<Professor>
{
    public int IdProfessor { get; set; }

    public string Nome { get; set; }

    public DateTime DataNascimento { get; set; }

    public decimal Salario { get; set; }
}

