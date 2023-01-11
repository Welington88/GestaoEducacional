﻿using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Data.Contexts;
using GestaoEducacional.Domain.DTOs;
using GestaoEducacional.Domain.Entities;
using GestaoEducacional.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GestaoEducacional.Data.Repositories;
#nullable disable
public class CursoRepository : ICursoRepository
{
    private readonly ILogger<CursoRepository> _logger;
    private readonly Context _context;

    public CursoRepository(ILogger<CursoRepository> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }
    
    public async Task<List<CursoViewModel>> Get()
    {
        try
        {
            var listaCursos = await _context.Cursos
                .Include(c => c.Disciplina)
                .Where(d => d.Disciplina.Select(e => e.IdCurso == d.IdCurso).FirstOrDefault())
                .ToListAsync();
            
            var listaCursosViewModel = new List<CursoViewModel>();
            foreach (var curso in listaCursos)
            {
                var CursoViewModel = CursoTransformation.GetViewModel(curso, curso.Disciplina.ToList());
                listaCursosViewModel.Add(CursoViewModel);
            }
                
            return listaCursosViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CursoViewModel> GetId(int id)
    {
        try
        {
            var cursosResult = await _context.Cursos
                .Include(c => c.Disciplina)
                .Where(d => d.Disciplina.Select(e => e.IdCurso == d.IdCurso).FirstOrDefault())
                .Where(c => c.IdCurso == id)
                .ToListAsync();

            var listaCursosViewModel = new List<CursoViewModel>();

            var CursosViewModel = CursoTransformation.GetViewModel(cursosResult.FirstOrDefault(), cursosResult.FirstOrDefault().Disciplina.ToList());
            return CursosViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Post(CursoDto CursoDTO)
    {
        try
        {
            var listaCursos = await _context.Cursos.ToListAsync();
            var CursosDomain = CursoTransformation.GetDomain(CursoDTO);

            await _context.Cursos.AddAsync(CursosDomain);
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

    public async Task<bool> Put(int id, CursoDto CursoDTO)
    {
        try
        {
            var CursoBase = GetId(id);
            if (CursoBase is null || CursoDTO.IdCurso != id)
            {
                return false;
            }
            
            var CursoUpdate = CursoTransformation.GetDomain(CursoDTO);
            _context.ChangeTracker.Clear();
            _context.Cursos.Update(CursoUpdate);
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
            var listaCursos = await _context.Cursos.ToListAsync();
            var CursoBase = listaCursos.Where<Curso>(v => v.IdCurso == id).FirstOrDefault();
            if (CursoBase is null || CursoBase.IdCurso != id)
            {
                return false;
            }

            _context.Cursos.Remove(CursoBase);
            var result = await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
