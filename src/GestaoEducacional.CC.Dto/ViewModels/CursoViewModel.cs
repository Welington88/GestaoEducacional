using System;

namespace GestaoEducacional.CC.Dto.ViewModels;
#nullable disable
public class CursoViewModel
{
    public int IdCurso { get; set; }

    public string DescricaoCurso { get; set; }
    
    public int NumeroProfessores { get; set; } 
    
    public int NumeroAlunos { get; set; }

    public float MediaCurso { get; set; } 

    public List<string> Disciplinas { get; set; }
}

