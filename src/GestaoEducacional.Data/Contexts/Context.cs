using GestaoEducacional.Data.Extensions;
using GestaoEducacional.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoEducacional.Data.Contexts;
#nullable disable
public class Context : DbContext
{
		public Context(DbContextOptions<Context> options) : base(options) 
		{
		}

		public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas{ get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Professor> Professores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }
}