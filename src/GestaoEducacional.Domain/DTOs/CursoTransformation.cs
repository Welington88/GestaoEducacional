using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Domain.Entities;

namespace GestaoEducacional.Domain.DTOs;
#nullable disable
public static class CursoTransformation
{

	public static Curso GetDomain(CursoDto DTO) {

        Curso domain = new Curso() {
            IdCurso = DTO.IdCurso,
            DescricaoCurso = DTO.DescricaoCurso
		};

		return domain;
	}

    public static CursoViewModel GetViewModel(Curso domain, List<Disciplina> disciplinas, CursoAlunoDto cursoAlunoDto) {

        var listaDisciplinasViewModel = new List<string>();
        disciplinas = disciplinas.Where(d => d.IdCurso == domain.IdCurso).ToList();
        if (disciplinas.Count >0)
        {
            foreach (var disciplina in disciplinas)
            {
                if (!(disciplina is null))
                {
                    listaDisciplinasViewModel.Add($"{disciplina.IdDisciplina} - {disciplina.DescricaoDisciplina}");
                }
            }

            var viewModel = new CursoViewModel()
            {
                IdCurso = domain.IdCurso,
                DescricaoCurso = domain.DescricaoCurso,
                NumeroProfessores = cursoAlunoDto.QtdProfessores,
                NumeroAlunos = cursoAlunoDto.QtdAlunos,
                MediaCurso = cursoAlunoDto.MediaAlunos,
                Disciplinas = listaDisciplinasViewModel
                
            };

            return viewModel;
        }
        else
        {
            return new CursoViewModel();
        }
        
	}
}

