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

    public static NotaViewModel GetViewModel(Nota domain) {

        var viewModel = new NotaViewModel() {
            IdNota = domain.IdNota,
            ValorNota = domain.ValorNota,
            Disciplina = domain.Disciplinas.DescricaoDisciplina,
            Aluno = $"Matricula: {domain.Aluno.MatriculaAluno} - Nome: {domain.Aluno.Nome}"
		};

		return viewModel;
	}
}

