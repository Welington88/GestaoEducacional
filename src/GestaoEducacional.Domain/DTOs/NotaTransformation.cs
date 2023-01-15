using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Domain.Entities;

namespace GestaoEducacional.Domain.DTOs;
#nullable disable
public static class NotaTransformation
{

	public static Nota GetDomain(NotaDto DTO) {

        Nota domain = new Nota() {
            IdNota = DTO.IdNota,
            MatriculaAluno = DTO.Aluno.MatriculaAluno,
            Disciplina = DTO.Disciplina.IdDisciplina,
            ValorNota = DTO.ValorNota
		};

		return domain;
	}

    public static NotaViewModel GetViewModel(Nota domain,List<Aluno> alunos, List<Disciplina> disciplinas, List<Curso> cursos) {

        var aluno = alunos.Where(a => a.MatriculaAluno == domain.MatriculaAluno).FirstOrDefault();
        var disciplina = disciplinas.Where(d => d.IdDisciplina == domain.Disciplina).FirstOrDefault();
        var curso = cursos.Where(c => c.IdCurso == disciplina.IdCurso).FirstOrDefault();

        var viewModel = new NotaViewModel() {
            IdNota = domain.IdNota,
            ValorNota = domain.ValorNota,
            Disciplina = new DisciplinaNotaViewModel() { IdDisciplina = disciplina.IdDisciplina, DescricaoDisciplina = curso.DescricaoCurso + " => " + disciplina.DescricaoDisciplina },
            Aluno = new AlunoNotaViewModel() { MatriculaAluno = aluno.MatriculaAluno, Nome = aluno.Nome }
		};

		return viewModel;
	}
}

