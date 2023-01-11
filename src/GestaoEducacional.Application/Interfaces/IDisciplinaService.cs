using GestaoEducacional.Application.Interfaces.Base;
using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;

namespace GestaoEducacional.Application.Interfaces;

public interface IDisciplinaService : IBaseAppService
{
    Task<List<DisciplinaViewModel>> Get();

    Task<DisciplinaViewModel> GetId(int id);

    Task<bool> Post(DisciplinaDto DisciplinaDto);

    Task<bool> Put(int id, DisciplinaDto DisciplinaDTO);

    Task<bool> Delete(int id);
}

