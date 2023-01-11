using GestaoEducacional.Application.Interfaces.Base;
using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;

namespace GestaoEducacional.Application.Interfaces;

public interface ICursoService : IBaseAppService
{
    Task<List<CursoViewModel>> Get();

    Task<CursoViewModel> GetId(int id);

    Task<bool> Post(CursoDto CursoDto);

    Task<bool> Put(int id, CursoDto CursoDTO);

    Task<bool> Delete(int id);
}

