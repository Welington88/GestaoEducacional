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
            var listaDisplinas = await _context.Disciplinas.ToListAsync();

            var listaDisciplinasViewModel = new List<DisciplinaViewModel>();

            foreach (var disciplina in listaDisplinas)
            {
                var disciplinaViewModel = GetId(disciplina.IdDisciplina);
                listaDisciplinasViewModel.Add(disciplinaViewModel.Result);
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
            var disciplinaBase = await _context.Disciplinas.Where(d => d.IdDisciplina == id).ToListAsync();
            if (disciplinaBase.Count == 0)
            {
                return new DisciplinaViewModel();
            }

            var disciplina = await _context.Disciplinas.Where(d => d.IdDisciplina == id).FirstOrDefaultAsync();
            var cursos = await _context.Cursos.ToListAsync();
            var professores = await _context.Professores.ToListAsync();
            var alunos = await _context.Alunos.ToListAsync();
            var notas = await _context.Notas.ToListAsync();
            var disciplinaViewModel = DisciplinaTransformation.GetViewModel(disciplina,
                cursos.Where(c => c.IdCurso == disciplina.IdCurso).FirstOrDefault(),
                professores.Where(p => p.IdProfessor == disciplina.IdProfessor).FirstOrDefault(),
                alunos,
                notas
            );
            return await Task.FromResult(disciplinaViewModel);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Post(DisciplinaDto disciplinaDTO)
    {
        try
        {
            var disciplinasDomain = DisciplinaTransformation.GetDomain(disciplinaDTO);

            await _context.Disciplinas.AddAsync(disciplinasDomain);
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

    public async Task<bool> Put(int id, DisciplinaDto disciplinaDTO)
    {
        try
        {
            var disciplinaBase = GetId(id);
            if (disciplinaBase is null || disciplinaDTO.IdDisciplina != id)
            {
                return false;
            }
            
            var DisciplinaUpdate = DisciplinaTransformation.GetDomain(disciplinaDTO);
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

