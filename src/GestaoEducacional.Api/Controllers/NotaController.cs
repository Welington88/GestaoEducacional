using GestaoEducacional.Application.Interfaces;
using GestaoEducacional.CC.Dto.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
namespace GestaoEducacional.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotaController : ControllerBase
{
    private readonly INotaService _NotaService;
    private readonly ILogger<NotaController> _logger;
    private readonly IConfiguration _configuration;

    public NotaController(INotaService NotaService, ILogger<NotaController> logger, IConfiguration configuration)
    {
        _NotaService = NotaService;
        _configuration = configuration;
        _logger = logger;
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna lista de todas Notas.",
        Description = "Retorna lista de todas as Notas.")]
    [SwaggerResponse(200, @"ExisteNotas")]
    [SwaggerResponse(400, @"Erro ao retornar dados.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Lista")]
    public async Task<ActionResult> ListaTodasAsNotas()
    {
        try
        {
            var viewModel = await _NotaService.Get();

            _logger.LogInformation(1, "[API] [Nota] [GET] [SUCESSO].");
            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(2 ,"[API] [Nota] [GET] [Existe] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna lista de Notas por Número do Pedido.",
        Description = "Retorna lista de Notas por Número do Pedido.")]
    [SwaggerResponse(200, @"ExisteNotas")]
    [SwaggerResponse(400, @"Erro ao retornar dados.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Lista/{id}")]
    public async Task<ActionResult> ListaNotasId(int id)
    {
        try
        {
            var viewModel = await _NotaService.GetId(id);

            _logger.LogInformation(1, "[API] [Nota] [GET] [SUCESSO].");
            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(2 ,"[API] [Nota] [GET] [Existe] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Salva envio de Nota.",
        Description = "Salva os dados Nota.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Nota.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Criar")]
    public async Task<ActionResult> CriarNota(NotaDto notaDto)
    {
        try
        {
            var result = await _NotaService.Post(notaDto);
            if (!result)
            {
                return BadRequest("Erro ao Criar Nota ");
            }
            _logger.LogInformation(1, "[API] [Nota] [Post] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Nota] [Post] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Atualiza envio de Nota.",
        Description = "Atualiza os dados Nota.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Nota.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Atualizar/{id}")]
    public async Task<ActionResult> AtualizarNota(int id, NotaDto notaDto)
    {
        try
        {
            var NotaBanco = await _NotaService.GetId(id);

            var result = await _NotaService.Put(id, notaDto);
            if (!result)
            {
                return BadRequest("Erro ao Atualizar Nota ");
            }
            _logger.LogInformation(1, "[API] [Nota] [Put] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Nota] [Put] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpDelete]
    [SwaggerOperation(
        Summary = "Deleta envio de Nota.",
        Description = "Deleta envio de Nota.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Nota.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Excluir/{id}")]
    public async Task<ActionResult> ExcluirNota(int id)
    {
        try
        {
            var result = await _NotaService.Delete(id);
            if (!result)
            {
                return BadRequest("Erro ao Excluir Nota ");
            }
            _logger.LogInformation(1, "[API] [Nota] [Delete] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Nota] [Delete] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }
}