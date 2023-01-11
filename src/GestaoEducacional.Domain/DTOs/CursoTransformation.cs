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

    public static CursoViewModel GetViewModel(Curso domain, List<Disciplina> disciplinas) {

        var listaDisciplinasViewModel = new List<string>();

        foreach (var disciplina in disciplinas)
        {
            listaDisciplinasViewModel.Add(disciplina.DescricaoDisciplina);
        }

        var viewModel = new CursoViewModel() {
			IdCurso = domain.IdCurso,
            DescricaoCurso = domain.DescricaoCurso,
            Disciplinas = listaDisciplinasViewModel
		};

		return viewModel;
	}
}

