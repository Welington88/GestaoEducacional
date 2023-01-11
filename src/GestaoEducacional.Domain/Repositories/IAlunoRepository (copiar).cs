using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.Enums;
using GestaoEducacional.CC.Dto.ViewModels;

namespace GestaoEducacional.Domain.Repositories;

public interface IAlunoRepository 
{
    Task<List<VendaViewModel>> Get();

    Task<AlunoViewModel> GetVendaId(int Matricula);

    Task<bool> PostVenda(AlunoDto alunoDto);

    Task<bool> PutVenda(int Matricula, AlunoDto alunoDto);

    Task<bool> DeleteVenda(int Matricula);
}

