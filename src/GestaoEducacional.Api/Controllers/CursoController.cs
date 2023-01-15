using GestaoEducacional.Application.Interfaces;
using GestaoEducacional.CC.Dto.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
namespace GestaoEducacional.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursoController : ControllerBase
{
    private readonly ICursoService _CursoService;
    private readonly ILogger<CursoController> _logger;
    private readonly IConfiguration _configuration;

    public CursoController(ICursoService CursoService, ILogger<CursoController> logger, IConfiguration configuration)
    {
        _CursoService = CursoService;
        _configuration = configuration;
        _logger = logger;
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna lista de todas Cursos.",
        Description = "Retorna lista de todas as Cursos.")]
    [SwaggerResponse(200, @"ExisteCursos")]
    [SwaggerResponse(400, @"Erro ao retornar dados.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Lista")]
    public async Task<ActionResult> ListaTodasOsCursos()
    {
        try
        {
            var viewModel = await _CursoService.Get();

            _logger.LogInformation(1, "[API] [Curso] [GET] [SUCESSO].");
            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(2 ,"[API] [Curso] [GET] [Existe] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna lista de Cursos por Número do Pedido.",
        Description = "Retorna lista de Cursos por Número do Pedido.")]
    [SwaggerResponse(200, @"ExisteCursos")]
    [SwaggerResponse(400, @"Erro ao retornar dados.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Lista/{id}")]
    public async Task<ActionResult> ListaCursosId(int id)
    {
        try
        {
            var viewModel = await _CursoService.GetId(id);
            if (viewModel.DescricaoCurso is null)
            {
                return NotFound("NotFound");
            }
            _logger.LogInformation(1, "[API] [Curso] [GET] [SUCESSO].");
            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(2 ,"[API] [Curso] [GET] [Existe] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Salva envio de Curso.",
        Description = "Salva os dados Curso.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Curso.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Criar")]
    public async Task<ActionResult> CriarCurso(CursoDto cursoDto)
    {
        try
        {
            var result = await _CursoService.Post(cursoDto);
            if (!result)
            {
                return BadRequest("Erro ao Criar Curso ");
            }
            _logger.LogInformation(1, "[API] [Curso] [Post] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Curso] [Post] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Atualiza envio de Curso.",
        Description = "Atualiza os dados Curso.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Curso.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Atualizar/{id}")]
    public async Task<ActionResult> AtualizarCurso(int id, CursoDto cursoDto)
    {
        try
        {
            var cursoBanco = await _CursoService.GetId(id);

            var result = await _CursoService.Put(id, cursoDto);
            if (!result)
            {
                return BadRequest("Erro ao Atualizar Curso ");
            }
            _logger.LogInformation(1, "[API] [Curso] [Put] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Curso] [Put] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpDelete]
    [SwaggerOperation(
        Summary = "Deleta envio de Curso.",
        Description = "Deleta envio de Curso.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Curso.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Excluir/{id}")]
    public async Task<ActionResult> ExcluirCurso(int id)
    {
        try
        {
            var result = await _CursoService.Delete(id);
            if (!result)
            {
                return BadRequest("Erro ao Excluir Curso ");
            }
            _logger.LogInformation(1, "[API] [Curso] [Delete] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Curso] [Delete] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }
}