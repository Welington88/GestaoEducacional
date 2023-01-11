using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;

namespace GestaoEducacional.Domain.Repositories;

public interface IDisciplinaRepository 
{
    Task<List<DisciplinaViewModel>> Get();

    Task<DisciplinaViewModel> GetId(int id);

    Task<bool> Post(DisciplinaDto DisciplinaDto);

    Task<bool> Put(int id, DisciplinaDto DisciplinaDto);

    Task<bool> Delete(int id);
}

