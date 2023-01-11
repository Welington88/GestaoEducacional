using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.Enums;
using GestaoEducacional.CC.Dto.ViewModels;

namespace GestaoEducacional.Domain.Repositories;

public interface IVendaRepository 
{
    Task<List<VendaViewModel>> Get();

    Task<VendaViewModel> GetVendaId(int NumeroVenda);

    Task<bool> PostVenda(VendaDTO vendaServiceDTO);

    Task<bool> PutVenda(int NumeroVenda, VendaDTO vendaServiceDTO, Status lastStatus);

    Task<bool> DeleteVenda(int NumeroVenda);
}

