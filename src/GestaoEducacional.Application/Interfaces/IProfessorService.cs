using GestaoEducacional.Application.Interfaces.Base;
using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;

namespace GestaoEducacional.Application.Interfaces;

public interface IProfessorService : IBaseAppService
{
    Task<List<ProfessorViewModel>> Get();

    Task<ProfessorViewModel> GetId(int id);

    Task<bool> Post(ProfessorDto ProfessorDto);

    Task<bool> Put(int id, ProfessorDto ProfessorDTO);

    Task<bool> Delete(int id);
}

