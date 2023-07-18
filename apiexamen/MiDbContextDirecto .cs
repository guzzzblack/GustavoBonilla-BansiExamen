using apiexamen.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiexamen
{
    public class MiDbContextDirecto : DbContext
    {
        public DbSet<tblExamen> Examen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar 'IdExamen' como clave primaria para la entidad 'tblExamen'
            modelBuilder.Entity<tblExamen>().HasKey(e => e.IdExamen);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=BdiExamen;Integrated Security=true;TrustServerCertificate=true");
        }
        public MiDbContextDirecto(DbContextOptions<MiDbContextDirecto> options) : base(options)
        {
        }

        // Define un DbSet para cada entidad con la que necesitas interactuar en el procedimiento almacenado.
        // Por ejemplo:
    }
}

