using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;

namespace GestaoEducacional.Domain.Repositories;

public interface INotaRepository 
{
    Task<List<NotaViewModel>> Get();

    Task<NotaViewModel> GetId(int id);

    Task<bool> Post(NotaDto NotaDto);

    Task<bool> Put(int id, NotaDto NotaDto);

    Task<bool> Delete(int id);
}

