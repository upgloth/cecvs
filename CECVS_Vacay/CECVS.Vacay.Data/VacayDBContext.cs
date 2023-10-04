using System;
using System.Collections.Generic;
using CECVS.Vacay.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CECVS.Vacay.Data
{
    public partial class VacayDBContext : DbContext, IVacayDBContext
    {
        public VacayDBContext() { }

        public VacayDBContext(DbContextOptions<VacayDBContext> options)
            : base(options) { }

        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Funcionario> Funcionarios { get; set; } = null!;
        public virtual DbSet<Ferias> Ferias { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Desktop\\Projects\\CECVS_03102023\\CECVS_Vacay\\DB\\Vacay.mdf;Integrated Security=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("PK_001_DEPARTAMENTO_ID_DEPARTAMENTO");

                entity.ToTable("001_departamentos");

                entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");

                entity.Property(e => e.NoDepartamento)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("no_departamento");
            });

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.HasKey(e => e.IdFuncionario)
                    .HasName("PK_002_FUNCIONARIO_ID_FUNCIONARIO");

                entity.ToTable("002_funcionarios");

                entity.HasIndex(e => e.CoMatricula, "UQ__002_func__A5416AAB8821C04A")
                    .IsUnique();

                entity.Property(e => e.IdFuncionario).HasColumnName("id_funcionario");

                entity.Property(e => e.CoMatricula)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("co_matricula");

                entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");

                entity.Property(e => e.NoFuncionario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("no_funcionario");

                entity.Property(e => e.DtAdmissao)
                   .HasColumnType("date")
                   .HasColumnName("dt_admissao");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Funcionarios)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_002_FUNCIONARIO_ID_DEPARTAMENTO");
            });

            modelBuilder.Entity<Ferias>(entity =>
            {
                entity.HasKey(e => e.IdFerias)
                    .HasName("PK_003_FERIAS_ID_FERIAS");

                entity.ToTable("003_ferias");

                entity.Property(e => e.IdFerias).HasColumnName("id_ferias");

                entity.Property(e => e.DtInicio)
                    .HasColumnType("date")
                    .HasColumnName("dt_inicio");

                entity.Property(e => e.IdFuncionario).HasColumnName("id_funcionario");

                entity.Property(e => e.QtDias).HasColumnName("qt_dias");

                entity.HasOne(d => d.IdFuncionarioNavigation)
                    .WithMany(p => p.Ferias)
                    .HasForeignKey(d => d.IdFuncionario)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_003_FERIAS_ID_FUNCIONARIO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
