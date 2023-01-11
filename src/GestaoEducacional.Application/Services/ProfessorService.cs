using GestaoEducacional.Application.Interfaces;
using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GestaoEducacional.Application.Services;

public class ProfessorService : IProfessorService
{

    private readonly IConfiguration _configuration;
    private readonly IProfessorRepository _repository;
    private readonly ILogger<ProfessorService> _logger;

    public ProfessorService(IConfiguration configuration, IProfessorRepository repository, ILogger<ProfessorService> logger)
    {
        _configuration = configuration;
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<ProfessorViewModel>> Get()
    {
        try
        {
            var listaProfessores = await _repository.Get();
            return listaProfessores;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ProfessorViewModel> GetId(int id)
    {
        try
        {
            var listaProfessors= await _repository.GetId(id);
            return listaProfessors;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Post(ProfessorDto professorDTO)
    {
        try
        {
            if (professorDTO is null)
            {
                return false;
            }

             var Professor = await _repository.Post(professorDTO);
             return Professor;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Put(int id, ProfessorDto professorDTO)
    {
        try
        {
        
            var Professor = await _repository.Put(id, professorDTO);
            return Professor;
            
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
            var Professor = await _repository.Delete(id);
            return Professor;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}