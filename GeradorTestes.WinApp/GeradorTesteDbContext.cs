using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.WinApp
{
    public class GeradorTesteDbContext : DbContext
    {
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Materia> Materias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=GeradorTestesOrm;Integrated Security=True";

            optionsBuilder.UseSqlServer(connectionString);

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Disciplina>(disciplinaBuilder =>
            {
                disciplinaBuilder.ToTable("TBDisciplina");

                disciplinaBuilder.Property(d => d.Id)
                .IsRequired()
                .HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<Materia>(materiaBuilder =>
            {
                materiaBuilder.ToTable("TBMateria");
                materiaBuilder.Property(m => m.Id).IsRequired()
                .ValueGeneratedOnAdd();

                materiaBuilder.Property(m => m.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

                materiaBuilder.Property(m => m.Serie)
                .IsRequired()
                .HasConversion<int>();

                materiaBuilder.HasOne(m => m.Disciplina)
                .WithMany(d => d.Materias).IsRequired()
                .HasForeignKey("Disclipina_Id")
                .HasConstraintName("FK_TBMateria.TBDisciplina")
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Ignore<Questao>();
            modelBuilder.Ignore<Alternativa>();
            modelBuilder.Ignore<Teste>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
