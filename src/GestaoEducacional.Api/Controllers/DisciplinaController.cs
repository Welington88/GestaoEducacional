using GestaoEducacional.Application.Interfaces;
using GestaoEducacional.CC.Dto.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
namespace GestaoEducacional.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DisciplinaController : ControllerBase
{
    private readonly IDisciplinaService _DisciplinaService;
    private readonly ILogger<DisciplinaController> _logger;
    private readonly IConfiguration _configuration;

    public DisciplinaController(IDisciplinaService DisciplinaService, ILogger<DisciplinaController> logger, IConfiguration configuration)
    {
        _DisciplinaService = DisciplinaService;
        _configuration = configuration;
        _logger = logger;
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna lista de todas Disciplinas.",
        Description = "Retorna lista de todas as Disciplinas.")]
    [SwaggerResponse(200, @"ExisteDisciplinas")]
    [SwaggerResponse(400, @"Erro ao retornar dados.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Lista")]
    public async Task<ActionResult> ListaTodasAsDisciplinas()
    {
        try
        {
            var viewModel = await _DisciplinaService.Get();

            _logger.LogInformation(1, "[API] [Disciplina] [GET] [SUCESSO].");
            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(2 ,"[API] [Disciplina] [GET] [Existe] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna lista de Disciplinas por Número do Pedido.",
        Description = "Retorna lista de Disciplinas por Número do Pedido.")]
    [SwaggerResponse(200, @"ExisteDisciplinas")]
    [SwaggerResponse(400, @"Erro ao retornar dados.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Lista/{id}")]
    public async Task<ActionResult> ListaDisciplinasId(int id)
    {
        try
        {
            var viewModel = await _DisciplinaService.GetId(id);

            _logger.LogInformation(1, "[API] [Disciplina] [GET] [SUCESSO].");
            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(2 ,"[API] [Disciplina] [GET] [Existe] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Salva envio de Disciplina.",
        Description = "Salva os dados Disciplina.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Disciplina.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Criar")]
    public async Task<ActionResult> CriarDisciplina(DisciplinaDto disciplinaDto)
    {
        try
        {
            var result = await _DisciplinaService.Post(disciplinaDto);
            if (!result)
            {
                return BadRequest("Erro ao Criar Disciplina ");
            }
            _logger.LogInformation(1, "[API] [Disciplina] [Post] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Disciplina] [Post] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Atualiza envio de Disciplina.",
        Description = "Atualiza os dados Disciplina.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Disciplina.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Atualizar/{id}")]
    public async Task<ActionResult> AtualizarDisciplina(int id, DisciplinaDto disciplinaDto)
    {
        try
        {
            var DisciplinaBanco = await _DisciplinaService.GetId(id);

            var result = await _DisciplinaService.Put(id, disciplinaDto);
            if (!result)
            {
                return BadRequest("Erro ao Atualizar Disciplina ");
            }
            _logger.LogInformation(1, "[API] [Disciplina] [Put] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Disciplina] [Put] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpDelete]
    [SwaggerOperation(
        Summary = "Deleta envio de Disciplina.",
        Description = "Deleta envio de Disciplina.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Disciplina.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Excluir/{id}")]
    public async Task<ActionResult> ExcluirDisciplina(int id)
    {
        try
        {
            var result = await _DisciplinaService.Delete(id);
            if (!result)
            {
                return BadRequest("Erro ao Excluir Disciplina ");
            }
            _logger.LogInformation(1, "[API] [Disciplina] [Delete] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Disciplina] [Delete] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }
}