using GestaoEducacional.Application.Interfaces;
using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Domain.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GestaoEducacional.Application.Services;

public class CursoService : Hub, ICursoService
{

    private readonly IConfiguration _configuration;
    private readonly ICursoRepository _repository;
    private readonly ILogger<CursoService> _logger;

    public CursoService(IConfiguration configuration, ICursoRepository repository, ILogger<CursoService> logger)
    {
        _configuration = configuration;
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<CursoViewModel>> Get()
    {
        try
        {
            var listaCursos = await _repository.Get();
            listaCursos = listaCursos.OrderBy(c => c.DescricaoCurso).ToList();
            return listaCursos;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CursoViewModel> GetId(int id)
    {
        try
        {
            var listaCursos= await _repository.GetId(id);
            return listaCursos;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Post(CursoDto cursoDTO)
    {
        try
        {
            if (cursoDTO is null)
            {
                return false;
            }

            cursoDTO.DescricaoCurso = cursoDTO.DescricaoCurso.Trim();
            var curso = await _repository.Post(cursoDTO);
            return curso;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Put(int id, CursoDto CursoDTO)
    {
        try
        {
        
            var curso = await _repository.Put(id, CursoDTO);
            return curso;
            
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
            var curso = await _repository.Delete(id);
            return curso;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}