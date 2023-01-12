using GestaoEducacional.Application.Interfaces;
using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Domain.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GestaoEducacional.Application.Services;

public class AlunoService : Hub , IAlunoService
{

    private readonly IConfiguration _configuration;
    private readonly IAlunoRepository _repository;
    private readonly ILogger<AlunoService> _logger;

    public AlunoService(IConfiguration configuration, IAlunoRepository repository, ILogger<AlunoService> logger)
    {
        _configuration = configuration;
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<AlunoViewModel>> Get()
    {
        try
        {
            var listaAlunos = await _repository.Get();
            return listaAlunos;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<AlunoViewModel> GetId(int matriculaAluno)
    {
        try
        {
            var listaAlunos= await _repository.GetId(matriculaAluno);
            return listaAlunos;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Post(AlunoDto alunoDTO)
    {
        try
        {
            if (alunoDTO is null)
            {
                return false;
            }

             var Aluno = await _repository.Post(alunoDTO);
             return Aluno;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Put(int matricula, AlunoDto alunoDTO)
    {
        try
        {
        
            var Aluno = await _repository.Put(matricula, alunoDTO);
            return Aluno;
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Delete(int matricula)
    {
        try
        {
            var Aluno = await _repository.Delete(matricula);
            return Aluno;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken) {
        await Task.Delay(100000000, cancellationToken);
        yield return DateTime.Now;

    }
}