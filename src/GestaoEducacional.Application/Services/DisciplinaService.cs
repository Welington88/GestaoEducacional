using GestaoEducacional.Application.Interfaces;
using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GestaoEducacional.Application.Services;

public class DisciplinaService : IDisciplinaService
{

    private readonly IConfiguration _configuration;
    private readonly IDisciplinaRepository _repository;
    private readonly ILogger<DisciplinaService> _logger;

    public DisciplinaService(IConfiguration configuration, IDisciplinaRepository repository, ILogger<DisciplinaService> logger)
    {
        _configuration = configuration;
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<DisciplinaViewModel>> Get()
    {
        try
        {
            var listaDisciplinas = await _repository.Get();
            return listaDisciplinas;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<DisciplinaViewModel> GetId(int id)
    {
        try
        {
            var listaDisciplinas= await _repository.GetId(id);
            return listaDisciplinas;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Post(DisciplinaDto disciplinaDTO)
    {
        try
        {
            if (disciplinaDTO is null)
            {
                return false;
            }

             var Disciplina = await _repository.Post(disciplinaDTO);
             return Disciplina;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Put(int id, DisciplinaDto disciplinaDTO)
    {
        try
        {
        
            var disciplina = await _repository.Put(id, disciplinaDTO);
            return disciplina;
            
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
            var disciplina = await _repository.Delete(id);
            return disciplina;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}