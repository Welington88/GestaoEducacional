using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Domain.Entities;

namespace GestaoEducacional.Domain.DTOs;
#nullable disable
public static class ProfessorTransformation
{

	public static Professor GetDomain(ProfessorDto DTO) {

        Professor domain = new Professor() {
            IdProfessor = DTO.IdProfessor,
            DataNascimento = DTO.DataNascimento,
            Nome = DTO.Nome,
            Salario = DTO.Salario
		};

		return domain;
	}

    public static ProfessorViewModel GetViewModel(Professor domain) {
        var viewModel = new ProfessorViewModel() {
			IdProfessor = domain.IdProfessor,
            DataNascimento = domain.DataNascimento,
            Nome = domain.Nome,
            Salario = domain.Salario
		};

		return viewModel;
	}
}

