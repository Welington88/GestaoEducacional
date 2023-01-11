using GestaoEducacional.Application.Interfaces.Base;
using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;

namespace GestaoEducacional.Application.Interfaces;

public interface INotaService : IBaseAppService
{
    Task<List<NotaViewModel>> Get();

    Task<NotaViewModel> GetId(int id);

    Task<bool> Post(NotaDto NotaDto);

    Task<bool> Put(int id, NotaDto NotaDTO);

    Task<bool> Delete(int id);
}

