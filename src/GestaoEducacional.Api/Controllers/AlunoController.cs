using GestaoEducacional.Application.Interfaces;
using GestaoEducacional.CC.Dto.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Swashbuckle.AspNetCore.Annotations;
namespace GestaoEducacional.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly IAlunoService _AlunoService;
    private readonly ILogger<AlunoController> _logger;
    private readonly IConfiguration _configuration;

    public AlunoController(IAlunoService AlunoService, ILogger<AlunoController> logger, IConfiguration configuration)
    {
        _AlunoService = AlunoService;
        _configuration = configuration;
        _logger = logger;
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna lista de todas Alunos.",
        Description = "Retorna lista de todas as Alunos.")]
    [SwaggerResponse(200, @"ExisteAlunos")]
    [SwaggerResponse(400, @"Erro ao retornar dados.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Lista")]
    public async Task<ActionResult> ListaTodasOsAlunos()
    {
        try
        {
            var viewModel = await _AlunoService.Get();

            _logger.LogInformation(1, "[API] [Aluno] [GET] [SUCESSO].");
            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(2 ,"[API] [Aluno] [GET] [Existe] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna lista de Alunos por Número do Pedido.",
        Description = "Retorna lista de Alunos por Número do Pedido.")]
    [SwaggerResponse(200, @"ExisteAlunos")]
    [SwaggerResponse(400, @"Erro ao retornar dados.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Lista/{numeroMatricula}")]
    public async Task<ActionResult> ListaAlunosId(int numeroMatricula)
    {
        try
        {
            var viewModel = await _AlunoService.GetId(numeroMatricula);
            if (viewModel.Nome is null)
            {
                return NotFound("Not Found");
            }
            _logger.LogInformation(1, "[API] [Aluno] [GET] [SUCESSO].");
            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(2 ,"[API] [Aluno] [GET] [Existe] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Salva envio de Aluno.",
        Description = "Salva os dados Aluno.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Aluno.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Criar")]
    public async Task<ActionResult> CriarAluno(AlunoDto alunoDto)
    {
        try
        {
            var result = await _AlunoService.Post(alunoDto);
            if (!result)
            {
                return BadRequest("Erro ao Criar Aluno ");
            }
            _logger.LogInformation(1, "[API] [Aluno] [Post] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Aluno] [Post] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Atualiza envio de Aluno.",
        Description = "Atualiza os dados Aluno.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Aluno.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Atualizar/{numeroMatricula}")]
    public async Task<ActionResult> AtualizarAluno(int numeroMatricula, AlunoDto alunoDto)
    {
        try
        {
            var AlunoBanco = await _AlunoService.GetId(numeroMatricula);

            var result = await _AlunoService.Put(numeroMatricula, alunoDto);
            if (!result)
            {
                return BadRequest("Erro ao Atualizar Aluno ");
            }
            _logger.LogInformation(1, "[API] [Aluno] [Put] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Aluno] [Put] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpDelete]
    [SwaggerOperation(
        Summary = "Deleta envio de Aluno.",
        Description = "Deleta envio de Aluno.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Aluno.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Excluir/{numeroMatricula}")]
    public async Task<ActionResult> ExcluirAluno(int numeroMatricula)
    {
        try
        {
            var result = await _AlunoService.Delete(numeroMatricula);
            if (!result)
            {
                return BadRequest("Erro ao Excluir Aluno ");
            }
            _logger.LogInformation(1, "[API] [Aluno] [Delete] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Aluno] [Delete] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }
}