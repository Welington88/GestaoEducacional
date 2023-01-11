using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;

namespace GestaoEducacional.Domain.Repositories;

public interface IAlunoRepository 
{
    Task<List<AlunoViewModel>> Get();

    Task<AlunoViewModel> GetId(int Matricula);

    Task<bool> Post(AlunoDto alunoDto);

    Task<bool> Put(int Matricula, AlunoDto alunoDto);

    Task<bool> Delete(int Matricula);
}

