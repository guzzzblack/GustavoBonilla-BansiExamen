using apiexamen.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;

namespace apiexamen.Data
{
    public partial class MiContextoDB : DbContext
    {
        public MiContextoDB()
        {
        }
        public MiContextoDB(DbContextOptions<MiContextoDB> options) : base(options) { }

        public DbSet<tblExamen> Examen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblExamen>(entity =>
            {
                entity.HasKey(e => e.IdExamen)
                    .HasName("PK__tblExame__0E8DC9BEF61A1C04");

                entity.ToTable("tblExamen");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
