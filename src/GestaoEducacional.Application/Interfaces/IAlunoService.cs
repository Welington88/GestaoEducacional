using GestaoEducacional.Application.Interfaces.Base;
using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;

namespace GestaoEducacional.Application.Interfaces;

public interface IAlunoService : IBaseAppService
{
    Task<List<AlunoViewModel>> Get();

    Task<AlunoViewModel> GetId(int matricula);

    Task<bool> Post(AlunoDto alunoDto);

    Task<bool> Put(int NumeroAluno, AlunoDto alunoDTO);

    Task<bool> Delete(int matricula);
}

