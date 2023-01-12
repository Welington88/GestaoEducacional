using System.Net.Http.Headers;
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

    public static DisciplinaViewModel GetViewModel(Disciplina domain, Curso curso, Professor professor, List<Aluno> alunos, List<Nota> notas) {
        var listaNotas = new List<NotaDisciplinaViewModel>();

        if (!(alunos is null) && (!(notas is null)))
        {
            notas = notas.Where(n => n.Disciplina == domain.IdDisciplina).ToList();

            foreach (var nota in notas)
            {        
                notas = notas.Distinct().ToList();
                
                var aluno = alunos.Where(a => a.MatriculaAluno == nota.MatriculaAluno).FirstOrDefault();

                listaNotas.Add(
                    new NotaDisciplinaViewModel()
                    {
                        NomeAluno = $"{aluno.MatriculaAluno} - {aluno.Nome}",
                        ValorNota = nota.ValorNota
                    }
                );
            }
        }

        var listaAlunos = notas.Where(q => q.Disciplina == domain.IdDisciplina).Distinct().ToList();
        var listaAlunosDistindas = new List<Nota>();
        foreach (var aluno in listaAlunos)
        {
            if (listaAlunosDistindas.Where(a => a.MatriculaAluno == aluno.MatriculaAluno).ToList().Count == 0)
            {
                listaAlunosDistindas.Add(aluno);
            }
        }
        var qtdAlunos = listaAlunosDistindas.Count();
        var viewModel = new DisciplinaViewModel() {
			IdDisciplina = domain.IdDisciplina,
            DescricaoDisciplina = domain.DescricaoDisciplina,
            Curso = $"{curso.IdCurso} - {curso.DescricaoCurso}",
            Professor = $"{professor.IdProfessor} - {professor.Nome}",
            QuantidadeAlunos = qtdAlunos,
            Alunos = listaNotas
		};

		return viewModel;
	}
}

