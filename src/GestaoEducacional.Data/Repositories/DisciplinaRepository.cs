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
public class DisciplinaRepository : IDisciplinaRepository
{
    private readonly ILogger<DisciplinaRepository> _logger;
    private readonly Context _context;

    public DisciplinaRepository(ILogger<DisciplinaRepository> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }
    
    public async Task<List<DisciplinaViewModel>> Get()
    {
        try
        {
            var listaDisciplinas = await _context.Disciplinas
                .Include(p => p.Professor)
                .Where(p => p.Professor.IdProfessor == p.IdProfessor)
                .Include(c => c.Curso)
                .Where(c => c.Curso.IdCurso == c.IdCurso)
                .ToListAsync();
            
            var listaAlunos = await _context.Alunos.Include(n => n.Nota)
                .Where(n => n.Nota.Select(m => m.MatriculaAluno == n.MatriculaAluno).FirstOrDefault())
                .ToListAsync();

            var listaDisciplinasViewModel = new List<DisciplinaViewModel>();

            foreach (var disciplina in listaDisciplinas)
            {
                var DisciplinaViewModel = DisciplinaTransformation.GetViewModel(disciplina,
                    listaDisciplinas.Select(d => d.Curso).FirstOrDefault(),
                    listaDisciplinas.Select(d => d.Professor).FirstOrDefault(),
                    listaAlunos
                );
                listaDisciplinasViewModel.Add(DisciplinaViewModel);
            }
                
            return listaDisciplinasViewModel;
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
            var disciplina = await _context.Disciplinas
                .Include(p => p.Professor)
                .Where(p => p.Professor.IdProfessor == p.IdProfessor)
                .Include(c => c.Curso)
                .Where(c => c.Curso.IdCurso == c.IdCurso)
                .Where(d => d.IdDisciplina == id)
                .ToListAsync();

            var listaAlunos = await _context.Alunos.Include(n => n.Nota)
                .Where(n => n.Nota.Select(m => m.MatriculaAluno == n.MatriculaAluno).FirstOrDefault())
                .ToListAsync();

            var disciplinaViewModel = DisciplinaTransformation.GetViewModel(disciplina.FirstOrDefault(),
                disciplina.Select(d => d.Curso).FirstOrDefault(),
                disciplina.Select(d => d.Professor).FirstOrDefault(),
                listaAlunos
            );
            return disciplinaViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Post(DisciplinaDto DisciplinaDTO)
    {
        try
        {
            var listaDisciplinas = await _context.Disciplinas.ToListAsync();
            var DisciplinasDomain = DisciplinaTransformation.GetDomain(DisciplinaDTO);

            await _context.Disciplinas.AddAsync(DisciplinasDomain);
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

    public async Task<bool> Put(int id, DisciplinaDto DisciplinaDTO)
    {
        try
        {
            var DisciplinaBase = GetId(id);
            if (DisciplinaBase is null || DisciplinaDTO.IdDisciplina != id)
            {
                return false;
            }
            
            var DisciplinaUpdate = DisciplinaTransformation.GetDomain(DisciplinaDTO);
            _context.ChangeTracker.Clear();
            _context.Disciplinas.Update(DisciplinaUpdate);
            var result = await _context.SaveChangesAsync();
            return true;
            
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
            var listaDisciplinas = await _context.Disciplinas.ToListAsync();
            var DisciplinaBase = listaDisciplinas.Where<Disciplina>(v => v.IdDisciplina == id).FirstOrDefault();
            if (DisciplinaBase is null || DisciplinaBase.IdDisciplina != id)
            {
                return false;
            }

            _context.Disciplinas.Remove(DisciplinaBase);
            var result = await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

