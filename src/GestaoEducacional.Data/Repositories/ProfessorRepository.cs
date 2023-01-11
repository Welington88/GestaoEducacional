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
public class ProfessorRepository : IProfessorRepository
{
    private readonly ILogger<ProfessorRepository> _logger;
    private readonly Context _context;

    public ProfessorRepository(ILogger<ProfessorRepository> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<ProfessorViewModel>> Get()
    {
        try
        {
            var listaProfessors = await _context.Professores.ToListAsync();
            
            var listaProfessorsViewModel = new List<ProfessorViewModel>();

            foreach (var professor in listaProfessors)
            {
                var ProfessorViewModel = ProfessorTransformation.GetViewModel(professor);
                listaProfessorsViewModel.Add(ProfessorViewModel);
            }
                
            return listaProfessorsViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ProfessorViewModel> GetId(int id)
    {
        try
        {
            var listaProfessors = await _context.Professores
                .ToListAsync();

            var listaProfessorsViewModel = new List<ProfessorViewModel>();
            var professorConsulta = listaProfessors.Where<Professor>(c => c.IdProfessor == id).FirstOrDefault();

            var professorsViewModel = ProfessorTransformation.GetViewModel(professorConsulta);
            return professorsViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Post(ProfessorDto ProfessorDTO)
    {
        try
        {
            var listaProfessors = await _context.Professores.ToListAsync();
            var professorsDomain = ProfessorTransformation.GetDomain(ProfessorDTO);

            await _context.Professores.AddAsync(professorsDomain);
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

    public async Task<bool> Put(int id, ProfessorDto professorDTO)
    {
        try
        {
            var professorBase = GetId(id);
            if (professorBase is null || professorDTO.IdProfessor != id)
            {
                return false;
            }
            
            var ProfessorUpdate = ProfessorTransformation.GetDomain(professorDTO);
            _context.ChangeTracker.Clear();
            _context.Professores.Update(ProfessorUpdate);
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
            var listaProfessores = await _context.Professores.ToListAsync();
            var professorBase = listaProfessores.Where<Professor>(v => v.IdProfessor == id).FirstOrDefault();
            if (professorBase is null || professorBase.IdProfessor != id)
            {
                return false;
            }

            _context.Professores.Remove(professorBase);
            var result = await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}