using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;

namespace GestaoEducacional.Domain.Repositories;

public interface ICursoRepository 
{
    Task<List<CursoViewModel>> Get();

    Task<CursoViewModel> GetId(int id);

    Task<bool> Post(CursoDto CursoDto);

    Task<bool> Put(int id, CursoDto CursoDto);

    Task<bool> Delete(int id);
}

