using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Domain.Entities;

namespace GestaoEducacional.Domain.DTOs;
#nullable disable
public static class AlunoTransformation
{

	public static Aluno GetDomain(AlunoDto DTO) {

        Aluno domain = new Aluno() {
            MatriculaAluno = DTO.MatriculaAluno,
            DataNascimento = DTO.DataNascimento,
            Nome = DTO.Nome
		};

		return domain;
	}

    public static List<Aluno> GetDomain(List<Aluno> listDTO)
    {

		var listDomain = new List<Aluno>();

        foreach (var DTO in listDTO)
		{
            Aluno domain = new Aluno()
            {
				MatriculaAluno = DTO.MatriculaAluno,
                DataNascimento = DTO.DataNascimento,
                Nome = DTO.Nome
            };

			listDomain.Add(domain);
        }

        return listDomain;
    }

    public static AlunoViewModel GetViewModel(Aluno domain, List<Disciplina> disciplinas, List<Nota> notas) {

        var listaNotas = new List<NotaViewModel>();

        foreach (var n in notas)
        {
            var disciplina = disciplinas.Where<Disciplina>(d => d.IdDisciplina == n.Disciplina).FirstOrDefault();

            var notaViewModel = new NotaViewModel() {
                Disciplina = disciplina.DescricaoDisciplina,
                ValorNota = n.ValorNota
            };
            listaNotas.Add(notaViewModel);
        }

        var viewModel = new AlunoViewModel() {
			DataNascimento = domain.DataNascimento,
            MatriculaAluno = domain.MatriculaAluno,
            Nome = domain.Nome,
            Notas = listaNotas
		};

		return viewModel;
	}
}

