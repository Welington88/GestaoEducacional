using GestaoEducacional.Application.Interfaces;
using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Domain.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GestaoEducacional.Application.Services;

public class NotaService : Hub, INotaService
{

    private readonly IConfiguration _configuration;
    private readonly INotaRepository _repository;
    private readonly ILogger<NotaService> _logger;

    public NotaService(IConfiguration configuration, INotaRepository repository, ILogger<NotaService> logger)
    {
        _configuration = configuration;
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<NotaViewModel>> Get()
    {
        try
        {
            var listaNotas = await _repository.Get();
            listaNotas = listaNotas.OrderBy(n => n.Aluno.Nome).ToList();
            return listaNotas;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<NotaViewModel> GetId(int id)
    {
        try
        {
            var listaNotas= await _repository.GetId(id);
            return listaNotas;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Post(NotaDto notaDTO)
    {
        try
        {
            if (notaDTO is null)
            {
                return false;
            }

            if (notaDTO.ValorNota < 0)
            {
                return false;
            }
            
            var nota = await _repository.Post(notaDTO);
            return nota;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Put(int id, NotaDto notaDTO)
    {
        try
        {
            if (notaDTO.ValorNota < 0)
            {
                return false;
            }
            var nota = await _repository.Put(id, notaDTO);
            return nota;
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var nota = await _repository.Delete(id);
            return nota;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}