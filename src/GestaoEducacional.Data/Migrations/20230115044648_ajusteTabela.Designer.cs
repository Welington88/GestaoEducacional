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
    [Migration("20230115044648_ajusteTabela")]
    partial class ajusteTabela
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Aluno", b =>
                {
                    b.Property<int>("MatriculaAluno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
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
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("IdCurso");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Disciplina", b =>
                {
                    b.Property<int>("IdDisciplina")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescricaoDisciplina")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("IdCurso")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdProfessor")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProfessorIdProfessor")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdDisciplina");

                    b.HasIndex("ProfessorIdProfessor");

                    b.ToTable("Disciplinas");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Matricula", b =>
                {
                    b.Property<int>("IdMatricula")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdDisciplina")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MatriculaAluno")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdMatricula");

                    b.ToTable("Matriculas");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Nota", b =>
                {
                    b.Property<int>("IdNota")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Disciplina")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MatriculaAluno")
                        .HasColumnType("INTEGER");

                    b.Property<float>("ValorNota")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("IdNota");

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
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Salario")
                        .HasColumnType("TEXT");

                    b.HasKey("IdProfessor");

                    b.ToTable("Professores");
                });

            modelBuilder.Entity("GestaoEducacional.Domain.Entities.Disciplina", b =>
                {
                    b.HasOne("GestaoEducacional.Domain.Entities.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorIdProfessor");

                    b.Navigation("Professor");
                });
#pragma warning restore 612, 618
        }
    }
}
