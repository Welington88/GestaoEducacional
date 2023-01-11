using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;

namespace GestaoEducacional.Domain.Repositories;

public interface IProfessorRepository 
{
    Task<List<ProfessorViewModel>> Get();

    Task<ProfessorViewModel> GetId(int id);

    Task<bool> Post(ProfessorDto ProfessorDto);

    Task<bool> Put(int id, ProfessorDto ProfessorDto);

    Task<bool> Delete(int id);
}

