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
        disciplinas = disciplinas.Where(d=> d != null).ToList();
        disciplinas = disciplinas.Where(d => d.IdCurso == domain.IdCurso).ToList();
        int numeroProfessores = (int)0;
        int numeroAlunos = (int)0;
        float mediaCurso = (float)0;

        if (!(cursoAlunoDto is null))
        {
            numeroProfessores = cursoAlunoDto.QtdProfessores;
            numeroAlunos = cursoAlunoDto.QtdAlunos;
            mediaCurso = cursoAlunoDto.MediaAlunos;
        }

        if (disciplinas.Count > 0)
        {
            foreach (var disciplina in disciplinas)
            {
                if (!(disciplina is null))
                {
                    listaDisciplinasViewModel.Add(disciplina.DescricaoDisciplina);
                }
            }

            var viewModel = new CursoViewModel()
            {
                IdCurso = domain.IdCurso,
                DescricaoCurso = domain.DescricaoCurso,
                NumeroProfessores = numeroProfessores,
                NumeroAlunos = numeroAlunos,
                MediaCurso = mediaCurso,
                Disciplinas = listaDisciplinasViewModel

            };
  
            return viewModel;
        }
        else
        {
            CursoViewModel resultViewModel;

            if (!(domain is null))
            {
               resultViewModel =  new CursoViewModel()
                {
                    IdCurso = domain.IdCurso,
                    DescricaoCurso = domain.DescricaoCurso,
                    NumeroProfessores = numeroProfessores,
                    NumeroAlunos = numeroAlunos,
                    MediaCurso = mediaCurso,
                    Disciplinas = listaDisciplinasViewModel
                };
            }
            else
            {
                resultViewModel = new CursoViewModel();
            }

            return resultViewModel;
        }
        
	}
}

