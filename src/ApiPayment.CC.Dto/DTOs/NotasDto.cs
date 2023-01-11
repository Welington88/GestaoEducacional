using System;
using GestaoEducacional.Domain.Entities.Base;

namespace GestaoEducacional.Domain.Entities;

public class Notas : Entity<Notas>
{
    public int IdNotas { get; set; }

    public int IdDisciplina { get; set; }

    public int MatriculaAluno { get; set; }
}

