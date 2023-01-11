using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Domain.Entities;

namespace GestaoEducacional.Domain.DTOs;
#nullable disable
public static class DisciplinaTransformation
{

	public static Disciplina GetDomain(DisciplinaDto DTO) {

        Disciplina domain = new Disciplina() {
            IdDisciplina = DTO.IdDisciplina,
            DescricaoDisciplina = DTO.DescricaoDisciplina,
            IdProfessor = DTO.Professor.IdProfessor,
            IdCurso = DTO.Curso.IdCurso
		};

		return domain;
	}

    public static DisciplinaViewModel GetViewModel(Disciplina domain, Curso curso, Professor professor, List<Aluno> alunos) {

        var listaAlunosViewModel = new List<string>();
        foreach (var item in alunos)
        {
            foreach (var nota in item.Nota)
            {
                var alunoViewModel = $"Matricula: {item.MatriculaAluno} - Nome :{item.Nome} - Disciplina : {nota.Disciplinas.DescricaoDisciplina} - Nota: {nota.ValorNota}";
                listaAlunosViewModel.Add(alunoViewModel);
            }
        }

        var viewModel = new DisciplinaViewModel() {
			IdDisciplina = domain.IdDisciplina,
            DescricaoDisciplina = domain.DescricaoDisciplina,
            Curso = curso.DescricaoCurso,
            Professor = professor.Nome,
            Alunos = listaAlunosViewModel
		};

		return viewModel;
	}
}

