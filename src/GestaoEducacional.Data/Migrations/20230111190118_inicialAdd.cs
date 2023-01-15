using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoEducacional.Data.Migrations
{
    /// <inheritdoc />
    public partial class inicialAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    MatriculaAluno = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.MatriculaAluno);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    IdCurso = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DescricaoCurso = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.IdCurso);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    IdProfessor = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Salario = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.IdProfessor);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    IdNota = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Disciplina = table.Column<int>(type: "INTEGER", nullable: false),
                    MatriculaAluno = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorNota = table.Column<float>(type: "REAL", nullable: false),
                    AlunoMatriculaAluno = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.IdNota);
                    table.ForeignKey(
                        name: "FK_Notas_Alunos_AlunoMatriculaAluno",
                        column: x => x.AlunoMatriculaAluno,
                        principalTable: "Alunos",
                        principalColumn: "MatriculaAluno");
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    IdDisciplina = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DescricaoDisciplina = table.Column<string>(type: "TEXT", nullable: true),
                    IdProfessor = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCurso = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfessorIdProfessor = table.Column<int>(type: "INTEGER", nullable: true),
                    CursoIdCurso = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.IdDisciplina);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Cursos_CursoIdCurso",
                        column: x => x.CursoIdCurso,
                        principalTable: "Cursos",
                        principalColumn: "IdCurso");
                    table.ForeignKey(
                        name: "FK_Disciplinas_Professores_ProfessorIdProfessor",
                        column: x => x.ProfessorIdProfessor,
                        principalTable: "Professores",
                        principalColumn: "IdProfessor");
                });

            migrationBuilder.CreateTable(
                name: "AlunoDisciplina",
                columns: table => new
                {
                    AlunoMatriculaAluno = table.Column<int>(type: "INTEGER", nullable: false),
                    DisciplinaIdDisciplina = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoDisciplina", x => new { x.AlunoMatriculaAluno, x.DisciplinaIdDisciplina });
                    table.ForeignKey(
                        name: "FK_AlunoDisciplina_Alunos_AlunoMatriculaAluno",
                        column: x => x.AlunoMatriculaAluno,
                        principalTable: "Alunos",
                        principalColumn: "MatriculaAluno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoDisciplina_Disciplinas_DisciplinaIdDisciplina",
                        column: x => x.DisciplinaIdDisciplina,
                        principalTable: "Disciplinas",
                        principalColumn: "IdDisciplina",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    IdMatricula = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdDisciplina = table.Column<int>(type: "INTEGER", nullable: false),
                    MatriculaAluno = table.Column<int>(type: "INTEGER", nullable: false),
                    AlunoMatriculaAluno = table.Column<int>(type: "INTEGER", nullable: true),
                    DisciplinaIdDisciplina = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.IdMatricula);
                    table.ForeignKey(
                        name: "FK_Matriculas_Alunos_AlunoMatriculaAluno",
                        column: x => x.AlunoMatriculaAluno,
                        principalTable: "Alunos",
                        principalColumn: "MatriculaAluno");
                    table.ForeignKey(
                        name: "FK_Matriculas_Disciplinas_DisciplinaIdDisciplina",
                        column: x => x.DisciplinaIdDisciplina,
                        principalTable: "Disciplinas",
                        principalColumn: "IdDisciplina");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunoDisciplina_DisciplinaIdDisciplina",
                table: "AlunoDisciplina",
                column: "DisciplinaIdDisciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_CursoIdCurso",
                table: "Disciplinas",
                column: "CursoIdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_ProfessorIdProfessor",
                table: "Disciplinas",
                column: "ProfessorIdProfessor");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AlunoMatriculaAluno",
                table: "Matriculas",
                column: "AlunoMatriculaAluno");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_DisciplinaIdDisciplina",
                table: "Matriculas",
                column: "DisciplinaIdDisciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_AlunoMatriculaAluno",
                table: "Notas",
                column: "AlunoMatriculaAluno");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoDisciplina");

            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}
