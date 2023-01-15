using Dapper;
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
            var listaCursosViewModel = new List<CursoViewModel>();
           
            var listaCursos =   from cursos in _context.Set<Curso>()
                                join disciplinas in _context.Set<Disciplina>()
                                    on cursos.IdCurso equals disciplinas.IdCurso into grouping
                                from disciplinas in grouping.DefaultIfEmpty()
                                select new { cursos, disciplinas };

            listaCursos = listaCursos.Distinct();
            foreach (var curso in listaCursos.ToList())
            {
                #region Query

                var queryAluno = @"SELECT  c.IdCurso,  c.DescricaoCurso , 
                                count( DISTINCT n.MatriculaAluno) as QtdAlunos,  avg(n.ValorNota) as MediaAlunos From
                                (
		                                (
			                                Cursos  c INNER JOIN Disciplinas d
				                                ON (c.IdCurso = d.IdCurso)
		                                )
	                                INNER JOIN Notas n
		                                ON(d.IdDisciplina =  n.Disciplina)
                                )
                                WHERE d.IdCurso = :IdCurso
                                GROUP BY d.IdCurso";

                var queryProfessor = @"SELECT  c.IdCurso,  c.DescricaoCurso ,   count( DISTINCT p.IdProfessor) as  QtdProfessores From
                                (
		                                (
			                                Cursos  c INNER JOIN Disciplinas d
				                                ON (c.IdCurso = d.IdCurso)
		                                )
		                                INNER JOIN Professores p 
			                                ON( d.IdProfessor = p.IdProfessor) 
                                )
                                WHERE c.IdCurso = :IdCurso
                                GROUP BY c.IdCurso
                                ;";
                #endregion

                var parameters = new { IdCurso = curso.cursos.IdCurso };
                CommandDefinition command = new CommandDefinition(queryAluno, parameters);
                var quantCursoProfAluno = _context.Database.GetDbConnection().Query<CursoAlunoDto>(command).FirstOrDefault();

                command = new CommandDefinition(queryProfessor, parameters);
                var quantProfessores = _context.Database.GetDbConnection().Query<CursoAlunoDto>(command).FirstOrDefault();
                if (!(quantProfessores is null) && !(quantCursoProfAluno is null))
                {
                    quantCursoProfAluno.QtdProfessores = quantProfessores is null ? 0 : quantProfessores.QtdProfessores;
                }

                var cursoViewModel = CursoTransformation.GetViewModel(curso.cursos, listaCursos.Select(c => c.disciplinas).ToList(), quantCursoProfAluno);
                if (!(quantProfessores is null))
                {
                    if (cursoViewModel.NumeroProfessores != quantProfessores.QtdProfessores)
                    {
                        cursoViewModel.NumeroProfessores = quantProfessores is null ? 0 : quantProfessores.QtdProfessores;
                    }
                }
                var validarAdd = listaCursosViewModel.Where(c => c.IdCurso == cursoViewModel.IdCurso).ToList().Count();
                if (validarAdd == 0)
                {
                    listaCursosViewModel.Add(cursoViewModel);
                }
            }
            
            return await Task.FromResult(listaCursosViewModel);
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
            var cursoBase =  await _context.Cursos.Where(c => c.IdCurso == id).ToListAsync();
            if (cursoBase.Count == 0)
            {
                return new CursoViewModel();
            }
            var curso =  from cursos in _context.Set<Curso>()
                               join disciplinas in _context.Set<Disciplina>()
                                    on cursos.IdCurso equals disciplinas.IdCurso into grouping
                                from disciplinas in grouping.DefaultIfEmpty()
                                where cursos.IdCurso == id
                                select new { cursos, disciplinas };
            
            var listaCursosViewModel = new List<CursoViewModel>();
            #region Query
            var queryAluno = @"SELECT  c.IdCurso,  c.DescricaoCurso , 
                                count( DISTINCT n.MatriculaAluno) as QtdAlunos,  avg(n.ValorNota) as MediaAlunos From
                                (
		                                (
			                                Cursos  c INNER JOIN Disciplinas d
				                                ON (c.IdCurso = d.IdCurso)
		                                )
	                                INNER JOIN Notas n
		                                ON(d.IdDisciplina =  n.Disciplina)
                                )
                                WHERE d.IdCurso = :IdCurso
                                GROUP BY d.IdCurso";

            var queryProfessor = @"SELECT  c.IdCurso,  c.DescricaoCurso ,   count( DISTINCT p.IdProfessor) as  QtdProfessores From
                                (
		                                (
			                                Cursos  c INNER JOIN Disciplinas d
				                                ON (c.IdCurso = d.IdCurso)
		                                )
		                                INNER JOIN Professores p 
			                                ON( d.IdProfessor = p.IdProfessor) 
                                )
                                WHERE c.IdCurso = :IdCurso
                                GROUP BY c.IdCurso
                                ;";
            #endregion

            var parameters = new { IdCurso = id};
            CommandDefinition command = new CommandDefinition(queryAluno, parameters);
            var quantCursoProfAluno = _context.Database.GetDbConnection().Query<CursoAlunoDto>(command).FirstOrDefault();

            command = new CommandDefinition(queryProfessor, parameters);
            var quantProfessores = _context.Database.GetDbConnection().Query<CursoAlunoDto>(command).FirstOrDefault();
            if (!(quantProfessores is null))
            {
                if (!(quantProfessores is null) && !(quantCursoProfAluno is null))
                {
                    quantCursoProfAluno.QtdProfessores = quantProfessores is null ? 0 : quantProfessores.QtdProfessores;
                }
            }

            var cursosViewModel = CursoTransformation.GetViewModel(curso.Select(c => c.cursos).FirstOrDefault(), curso.Select(d => d.disciplinas).ToList(), quantCursoProfAluno);
            if (!(quantProfessores is null))
            {
                if (cursosViewModel.NumeroProfessores != quantProfessores.QtdProfessores && quantProfessores != null)
                {
                    cursosViewModel.NumeroProfessores = quantProfessores is null ? 0 : quantProfessores.QtdProfessores;
                }
            }
            return await Task.FromResult(cursosViewModel);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Post(CursoDto cursoDTO)
    {
        try
        {
            var CursosDomain = CursoTransformation.GetDomain(cursoDTO);

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

    public async Task<bool> Put(int id, CursoDto cursoDTO)
    {
        try
        {
            cursoDTO.IdCurso = id;
            var cursoBase = GetId(id);
            if (cursoBase is null || cursoDTO.IdCurso != id)
            {
                return false;
            }
            
            var CursoUpdate = CursoTransformation.GetDomain(cursoDTO);
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

