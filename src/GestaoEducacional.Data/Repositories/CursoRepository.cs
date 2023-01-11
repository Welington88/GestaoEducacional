using System;
using System.Linq;
using GestaoEducacional.CC.Dto.DTOs;
using GestaoEducacional.CC.Dto.Enums;
using GestaoEducacional.CC.Dto.ViewModels;
using GestaoEducacional.Data.Contexts;
using GestaoEducacional.Domain.DTOs;
using GestaoEducacional.Domain.Entities;
using GestaoEducacional.Domain.Repositories;
using Microsoft.Build.Framework;
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
            var listaAlunos = await _context.Alunos.ToListAsync();
            var notas = await _context.Notas.ToListAsync();
            var disciplinas = await _context.Disciplinas.ToListAsync();
            var listaAlunosViewModel = new List<AlunoViewModel>();

            foreach (var aluno in listaAlunos)
            {
                var alunoViewModel = AlunoTransformation.GetViewModel(aluno,disciplinas,notas);
                listaAlunosViewModel.Add(alunoViewModel);
            }
                
            return listaAlunosViewModel;
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
            var listaAlunos = await _context.Alunos.ToListAsync();
            var AlunoConsulta = listaAlunos.Where<Aluno>(a => a.MatriculaAluno == matricula).FirstOrDefault();
            var aluno = listaAlunos.Where<Aluno>(a => a.MatriculaAluno == AlunoConsulta.MatriculaAluno).FirstOrDefault();
            var notas = await _context.Notas.ToListAsync();
            var disciplinas = await _context.Disciplinas.ToListAsync();

            var AlunosViewModel = AlunoTransformation.GetViewModel(aluno,disciplinas,notas);
            return AlunosViewModel;
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

