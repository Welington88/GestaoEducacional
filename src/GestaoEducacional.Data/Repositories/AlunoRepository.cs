using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Data.Contexts;
using GestaoEducacional.Domain.DTOs;
using GestaoEducacional.Domain.Entities;
using GestaoEducacional.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GestaoEducacional.Data.Repositories;
#nullable disable
public class AlunoRepository : IAlunoRepository
{
    private readonly ILogger<AlunoRepository> _logger;
    private readonly Context _context;

    public AlunoRepository(ILogger<AlunoRepository> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<AlunoViewModel>> Get()
    {
        try
        {
            var query = from alunos in _context.Set<Aluno>()
                        join notas in _context.Set<Nota>()
                            on alunos.MatriculaAluno equals notas.MatriculaAluno into grouping
                        from notas in grouping.DefaultIfEmpty()
                        join disciplinas in _context.Set<Disciplina>()
                           on notas.Disciplina equals disciplinas.IdDisciplina into groupingDisplina
                        from disciplinas in groupingDisplina.DefaultIfEmpty()
                        select new { alunos, notas , disciplinas};
            
            var listaAlunosViewModel = new List<AlunoViewModel>();

            foreach (var aluno in query.Select(q => q.alunos).ToList())
            {
                var alunoViewModel = AlunoTransformation.GetViewModel(aluno, query.Select(q => q.disciplinas).ToList() , query.Select(q => q.notas).ToList());
                listaAlunosViewModel.Add(alunoViewModel);
            }
                
            return await Task.FromResult(listaAlunosViewModel);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<AlunoViewModel> GetId(int matricula)
    {
        try
        {
            var listaAlunos = await _context.Alunos.Include(n => n.Nota)
                .Where(n => n.Nota.Select(e => e.MatriculaAluno == n.MatriculaAluno).FirstOrDefault())
                .Where(a => a.MatriculaAluno == matricula)
                .ToListAsync();
            var alunoConsulta = listaAlunos.FirstOrDefault();

            var alunoViewModel = AlunoTransformation.GetViewModel(alunoConsulta,
                    listaAlunos.FirstOrDefault().Disciplina.ToList(),
                    listaAlunos.FirstOrDefault().Nota.ToList()
            );
            return alunoViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Post(AlunoDto AlunoDTO)
    {
        try
        {
            var listaAlunosBancoDeDados = await _context.Alunos.ToListAsync();
            var listaAlunos = await _context.Alunos.ToListAsync();
            var AlunosDomain = AlunoTransformation.GetDomain(AlunoDTO);

            await _context.Alunos.AddAsync(AlunosDomain);
            var result = _context.SaveChangesAsync();

            if (result is null)
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Put(int matricula, AlunoDto AlunoDTO)
    {
        try
        {
            var AlunoBase = GetId(matricula);
            if (AlunoBase is null || AlunoDTO.MatriculaAluno != matricula)
            {
                return false;
            }
            
            var AlunoUpdate = AlunoTransformation.GetDomain(AlunoDTO);
            _context.ChangeTracker.Clear();
            _context.Alunos.Update(AlunoUpdate);
            var result = await _context.SaveChangesAsync();
            return true;
            
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
            var listaAlunos = await _context.Alunos.ToListAsync();
            var AlunoBase = listaAlunos.Where<Aluno>(v => v.MatriculaAluno == matricula).FirstOrDefault();
            if (AlunoBase is null || AlunoBase.MatriculaAluno != matricula)
            {
                return false;
            }

            _context.Alunos.Remove(AlunoBase);
            var result = await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

