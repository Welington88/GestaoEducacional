using GestaoEducacional.Application.Interfaces;
using GestaoEducacional.CC.Dto.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GestaoEducacional.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessorController : ControllerBase
{
    private readonly IProfessorService _ProfessorService;
    private readonly ILogger<ProfessorController> _logger;
    private readonly IConfiguration _configuration;

    public ProfessorController(IProfessorService ProfessorService, ILogger<ProfessorController> logger, IConfiguration configuration)
    {
        _ProfessorService = ProfessorService;
        _configuration = configuration;
        _logger = logger;
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna lista de todas Professors.",
        Description = "Retorna lista de todas as Professors.")]
    [SwaggerResponse(200, @"ExisteProfessors")]
    [SwaggerResponse(400, @"Erro ao retornar dados.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Lista")]
    public async Task<ActionResult> ListaTodasAsProfessors()
    {
        try
        {
            var viewModel = await _ProfessorService.Get();

            _logger.LogInformation(1, "[API] [Professor] [GET] [SUCESSO].");
            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(2 ,"[API] [Professor] [GET] [Existe] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna lista de Professors por Número do Pedido.",
        Description = "Retorna lista de Professors por Número do Pedido.")]
    [SwaggerResponse(200, @"ExisteProfessors")]
    [SwaggerResponse(400, @"Erro ao retornar dados.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Lista/{id}")]
    public async Task<ActionResult> ListaProfessorsId(int id)
    {
        try
        {
            var viewModel = await _ProfessorService.GetId(id);

            _logger.LogInformation(1, "[API] [Professor] [GET] [SUCESSO].");
            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(2 ,"[API] [Professor] [GET] [Existe] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Salva envio de Professor.",
        Description = "Salva os dados Professor.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Professor.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Criar")]
    public async Task<ActionResult> CriarProfessor(ProfessorDto professorDto)
    {
        try
        {
            var result = await _ProfessorService.Post(professorDto);
            if (!result)
            {
                return BadRequest("Erro ao Criar Professor ");
            }
            _logger.LogInformation(1, "[API] [Professor] [Post] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Professor] [Post] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Atualiza envio de Professor.",
        Description = "Atualiza os dados Professor.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Professor.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Atualizar/{id}")]
    public async Task<ActionResult> AtualizarProfessor(int id, ProfessorDto professorDto)
    {
        try
        {
            var ProfessorBanco = await _ProfessorService.GetId(id);

            var result = await _ProfessorService.Put(id, professorDto);
            if (!result)
            {
                return BadRequest("Erro ao Atualizar Professor ");
            }
            _logger.LogInformation(1, "[API] [Professor] [Put] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Professor] [Put] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpDelete]
    [SwaggerOperation(
        Summary = "Deleta envio de Professor.",
        Description = "Deleta envio de Professor.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Professor.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Excluir/{id}")]
    public async Task<ActionResult> ExcluirProfessor(int id)
    {
        try
        {
            var result = await _ProfessorService.Delete(id);
            if (!result)
            {
                return BadRequest("Erro ao Excluir Professor ");
            }
            _logger.LogInformation(1, "[API] [Professor] [Delete] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Professor] [Delete] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }
}