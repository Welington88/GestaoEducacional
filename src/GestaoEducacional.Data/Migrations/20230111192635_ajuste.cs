using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoEducacional.Data.Migrations
{
    /// <inheritdoc />
    public partial class ajuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplinas_Cursos_CursoIdCurso",
                table: "Disciplinas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Alunos_AlunoMatriculaAluno",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Disciplinas_DisciplinaIdDisciplina",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Alunos_AlunoMatriculaAluno",
                table: "Notas");

            migrationBuilder.DropTable(
                name: "AlunoDisciplina");

            migrationBuilder.DropIndex(
                name: "IX_Notas_AlunoMatriculaAluno",
                table: "Notas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_AlunoMatriculaAluno",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_DisciplinaIdDisciplina",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Disciplinas_CursoIdCurso",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "AlunoMatriculaAluno",
                table: "Notas");

            migrationBuilder.DropColumn(
                name: "AlunoMatriculaAluno",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "DisciplinaIdDisciplina",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "CursoIdCurso",
                table: "Disciplinas");

            migrationBuilder.AlterColumn<float>(
                name: "ValorNota",
                table: "Notas",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoDisciplina",
                table: "Disciplinas",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoCurso",
                table: "Cursos",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Alunos",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ValorNota",
                table: "Notas",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<int>(
                name: "AlunoMatriculaAluno",
                table: "Notas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlunoMatriculaAluno",
                table: "Matriculas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisciplinaIdDisciplina",
                table: "Matriculas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoDisciplina",
                table: "Disciplinas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "CursoIdCurso",
                table: "Disciplinas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoCurso",
                table: "Cursos",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Alunos",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

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

            migrationBuilder.CreateIndex(
                name: "IX_Notas_AlunoMatriculaAluno",
                table: "Notas",
                column: "AlunoMatriculaAluno");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AlunoMatriculaAluno",
                table: "Matriculas",
                column: "AlunoMatriculaAluno");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_DisciplinaIdDisciplina",
                table: "Matriculas",
                column: "DisciplinaIdDisciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_CursoIdCurso",
                table: "Disciplinas",
                column: "CursoIdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoDisciplina_DisciplinaIdDisciplina",
                table: "AlunoDisciplina",
                column: "DisciplinaIdDisciplina");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplinas_Cursos_CursoIdCurso",
                table: "Disciplinas",
                column: "CursoIdCurso",
                principalTable: "Cursos",
                principalColumn: "IdCurso");

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Alunos_AlunoMatriculaAluno",
                table: "Matriculas",
                column: "AlunoMatriculaAluno",
                principalTable: "Alunos",
                principalColumn: "MatriculaAluno");

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Disciplinas_DisciplinaIdDisciplina",
                table: "Matriculas",
                column: "DisciplinaIdDisciplina",
                principalTable: "Disciplinas",
                principalColumn: "IdDisciplina");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Alunos_AlunoMatriculaAluno",
                table: "Notas",
                column: "AlunoMatriculaAluno",
                principalTable: "Alunos",
                principalColumn: "MatriculaAluno");
        }
    }
}
