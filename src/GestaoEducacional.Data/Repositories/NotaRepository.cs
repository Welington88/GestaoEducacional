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
public class NotaRepository : INotaRepository
{
    private readonly ILogger<NotaRepository> _logger;
    private readonly Context _context;

    public NotaRepository(ILogger<NotaRepository> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }
    
    public async Task<List<NotaViewModel>> Get()
    {
        try
        {
            var listaNotas =    from notas in _context.Set<Nota>()
                                join alunos in _context.Set<Aluno>()
                                    on notas.MatriculaAluno equals alunos.MatriculaAluno into grouping
                                from alunos in grouping.DefaultIfEmpty()
                                join disciplinas in _context.Set<Disciplina>()
                                    on notas.Disciplina equals disciplinas.IdDisciplina into groupingDisplina
                                from disciplinas in groupingDisplina.DefaultIfEmpty()
                                select new { notas, alunos, disciplinas };

            var listaNotasViewModel = new List<NotaViewModel>();
            var listaCursos = await _context.Cursos.ToListAsync();
            foreach (var notas in listaNotas.Select(s => s.notas).ToList())
            {
                var notasViewModel = NotaTransformation.GetViewModel(notas,
                            listaNotas.Select(s => s.alunos).ToList(),
                            listaNotas.Select(s => s.disciplinas).ToList(),
                            listaCursos);
                listaNotasViewModel.Add(notasViewModel);
            }
                
            return await Task.FromResult(listaNotasViewModel);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<NotaViewModel> GetId(int id)
    {
        try
        {
            var notasBase = await _context.Notas.Where(n => n.IdNota == id).ToListAsync();
            if (notasBase.Count == 0)
            {
                return new NotaViewModel();
            }
            
            var listaNotas = from notas in _context.Set<Nota>()
                             join alunos in _context.Set<Aluno>()
                                 on notas.MatriculaAluno equals alunos.MatriculaAluno into grouping
                             from alunos in grouping.DefaultIfEmpty()
                             join disciplinas in _context.Set<Disciplina>()
                                 on notas.Disciplina equals disciplinas.IdDisciplina into groupingDisplina
                             from disciplinas in groupingDisplina.DefaultIfEmpty()
                             where notas.IdNota == id
                             select new { notas, alunos, disciplinas };

            var listaCursos = await _context.Cursos.ToListAsync();

            var notasViewModel = NotaTransformation.GetViewModel(listaNotas.Select(s => s.notas).ToList().Where(n => n.IdNota == id).FirstOrDefault(),
                            listaNotas.Select(s => s.alunos).ToList(),
                            listaNotas.Select(s => s.disciplinas).ToList(),
                            listaCursos);

            return await Task.FromResult(notasViewModel);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Post(NotaDto notasDTO)
    {
        try
        {
            var notasDomain = NotaTransformation.GetDomain(notasDTO);

            await _context.Notas.AddAsync(notasDomain);
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

    public async Task<bool> Put(int id, NotaDto notasDTO)
    {
        try
        {
            var notasBase = GetId(id);
            if (notasBase is null || notasDTO.IdNota != id)
            {
                return false;
            }
            
            var NotasUpdate = NotaTransformation.GetDomain(notasDTO);
            _context.ChangeTracker.Clear();
            _context.Notas.Update(NotasUpdate);
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
            var listaNotasBase = await _context.Notas.ToListAsync();
            var notaBase = listaNotasBase.Where(n => n.IdNota == id).FirstOrDefault();
            if (notaBase is null || notaBase.IdNota != id)
            {
                return false;
            }

            _context.Notas.Remove(notaBase);
            var result = await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

