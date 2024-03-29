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
            var listaAlunosDto = query.Select(q => q.alunos).ToList();
            foreach (var aluno in listaAlunosDto)
            {
                var alunoViewModel = AlunoTransformation.GetViewModel(aluno, query.Select(q => q.disciplinas).ToList() , query.Select(q => q.notas).ToList());
                var validarLista = listaAlunosViewModel.Where(a => a.MatriculaAluno == alunoViewModel.MatriculaAluno).ToList().Count();
                if (validarLista == 0)
                {
                    listaAlunosViewModel.Add(alunoViewModel);
                }
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
            AlunoViewModel alunoViewModel;
            var alunoConsultaBanco = await _context.Alunos
                .Where(a => a.MatriculaAluno == matricula)
                .ToListAsync();

            if (!(alunoConsultaBanco.Count == 0))
            {
                var notasLancadas = await _context.Notas.Where(n => n.MatriculaAluno == alunoConsultaBanco.FirstOrDefault().MatriculaAluno).ToListAsync();

                if (!(notasLancadas.Count == 0))
                {
                    var disciplinaAlunos = await _context.Disciplinas.ToListAsync();
                    alunoViewModel = AlunoTransformation.GetViewModel(alunoConsultaBanco.FirstOrDefault(),
                        disciplinaAlunos,
                        notasLancadas
                    );
                }
                else
                {
                    var listaAlunosSimples = await _context.Alunos
                    .Where(a => a.MatriculaAluno == matricula)
                    .ToListAsync();
                    var listaNotas = new List<Nota>();
                    var listaDisciplina = new List<Disciplina>();
                    alunoViewModel = AlunoTransformation.GetViewModel(alunoConsultaBanco.FirstOrDefault(),
                        listaDisciplina,
                        listaNotas
                    );
                }
            }
            else
            {
                alunoViewModel = new AlunoViewModel();
            }
            
            return alunoViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Post(AlunoDto alunoDTO)
    {
        try
        {
            var alunosDomain = AlunoTransformation.GetDomain(alunoDTO);

            await _context.Alunos.AddAsync(alunosDomain);
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

    public async Task<bool> Put(int matricula, AlunoDto alunoDTO)
    {
        try
        {
            var alunoBase = GetId(matricula);
            if (alunoBase is null || alunoDTO.MatriculaAluno != matricula)
            {
                return false;
            }
            
            var AlunoUpdate = AlunoTransformation.GetDomain(alunoDTO);
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

