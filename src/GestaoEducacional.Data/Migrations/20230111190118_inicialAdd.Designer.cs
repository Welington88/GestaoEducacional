﻿// <auto-generated />
using System;
using GestaoEducacional.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestaoEducacional.Data.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230111190118_inicialAdd")]
    partial class inicialAdd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("AlunoDisciplina", b =>
                {
                    b.Property<int>("AlunoMatriculaAluno")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DisciplinaIdDisciplina")
                        .HasColumnType("INTEGER");

                    b.HasKey("AlunoMatriculaAluno", "DisciplinaIdDisciplina");

                    b.HasIndex("DisciplinaIdDisciplina");

                    b.ToTable("AlunoDisciplina");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Aluno", b =>
                {
                    b.Property<int>("MatriculaAluno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("MatriculaAluno");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Curso", b =>
                {
                    b.Property<int>("IdCurso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescricaoCurso")
                        .HasColumnType("TEXT");

                    b.HasKey("IdCurso");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Disciplina", b =>
                {
                    b.Property<int>("IdDisciplina")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CursoIdCurso")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescricaoDisciplina")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdCurso")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdProfessor")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProfessorIdProfessor")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdDisciplina");

                    b.HasIndex("CursoIdCurso");

                    b.HasIndex("ProfessorIdProfessor");

                    b.ToTable("Disciplinas");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Matricula", b =>
                {
                    b.Property<int>("IdMatricula")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AlunoMatriculaAluno")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DisciplinaIdDisciplina")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdDisciplina")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MatriculaAluno")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdMatricula");

                    b.HasIndex("AlunoMatriculaAluno");

                    b.HasIndex("DisciplinaIdDisciplina");

                    b.ToTable("Matriculas");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Nota", b =>
                {
                    b.Property<int>("IdNota")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AlunoMatriculaAluno")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Disciplina")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MatriculaAluno")
                        .HasColumnType("INTEGER");

                    b.Property<float>("ValorNota")
                        .HasColumnType("REAL");

                    b.HasKey("IdNota");

                    b.HasIndex("AlunoMatriculaAluno");

                    b.ToTable("Notas");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Professor", b =>
                {
                    b.Property<int>("IdProfessor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Salario")
                        .HasColumnType("TEXT");

                    b.HasKey("IdProfessor");

                    b.ToTable("Professores");
                });

            modelBuilder.Entity("AlunoDisciplina", b =>
                {
                    b.HasOne("GestaoEducacional.Domain.Entities.Aluno", null)
                        .WithMany()
                        .HasForeignKey("AlunoMatriculaAluno")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestaoEducacional.Domain.Entities.Disciplina", null)
                        .WithMany()
                        .HasForeignKey("DisciplinaIdDisciplina")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Disciplina", b =>
                {
                    b.HasOne("GestaoEducacional.Domain.Entities.Curso", "Curso")
                        .WithMany("Disciplina")
                        .HasForeignKey("CursoIdCurso");

                    b.HasOne("GestaoEducacional.Domain.Entities.Professor", "Professor")
                        .WithMany("Disciplina")
                        .HasForeignKey("ProfessorIdProfessor");

                    b.Navigation("Curso");

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Matricula", b =>
                {
                    b.HasOne("GestaoEducacional.Domain.Entities.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoMatriculaAluno");

                    b.HasOne("GestaoEducacional.Domain.Entities.Disciplina", "Disciplina")
                        .WithMany()
                        .HasForeignKey("DisciplinaIdDisciplina");

                    b.Navigation("Aluno");

                    b.Navigation("Disciplina");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Nota", b =>
                {
                    b.HasOne("GestaoEducacional.Domain.Entities.Aluno", null)
                        .WithMany("Nota")
                        .HasForeignKey("AlunoMatriculaAluno");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Aluno", b =>
                {
                    b.Navigation("Nota");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Curso", b =>
                {
                    b.Navigation("Disciplina");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Professor", b =>
                {
                    b.Navigation("Disciplina");
                });
#pragma warning restore 612, 618
        }
    }
}