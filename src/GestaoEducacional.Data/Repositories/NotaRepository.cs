using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Data.Contexts;
using GestaoEducacional.Domain.DTOs;
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
            var listaNotas = await _context.Notas
                .Include(a => a.Aluno)
                .Where(a => a.MatriculaAluno == a.Aluno.MatriculaAluno)
                .Include(d => d.Disciplina)
                .Where(d => d.Disciplinas.IdDisciplina == d.Disciplina)
                .ToListAsync();
            
            var listaAlunos = await _context.Alunos.Include(n => n.Nota)
                .Where(n => n.Nota.Select(m => m.MatriculaAluno == n.MatriculaAluno).FirstOrDefault())
                .ToListAsync();

            var listaNotasViewModel = new List<NotaViewModel>();

            foreach (var notas in listaNotas)
            {
                var NotasViewModel = NotaTransformation.GetViewModel(notas);
                listaNotasViewModel.Add(NotasViewModel);
            }
                
            return listaNotasViewModel;
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
            var nota = await _context.Notas
                .Include(a => a.Aluno)
                .Where(a => a.MatriculaAluno == a.Aluno.MatriculaAluno)
                .Include(d => d.Disciplina)
                .Where(d => d.Disciplinas.IdDisciplina == d.Disciplina)
                .Where(d => d.IdNota == id)
                .ToListAsync();

            var NotasViewModel = NotaTransformation.GetViewModel(nota.FirstOrDefault());
            return NotasViewModel;
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

