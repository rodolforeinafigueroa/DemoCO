using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Entidades;

namespace ERP.AccesoDatos
{
    public class ERPContext : DbContext
    {
        public DbSet<Compania> Companias { get; set; }
        public DbSet<Regional> Regionales { get; set; }
        public DbSet<Contacto>  Contactos { get; set; }
        public DbSet<CentroOperacion> CentrosOperacion { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                if(Environment.MachineName == "P17FB90")
                {
                    options.UseSqlServer(@"Server = localhost\sqlexpress; Database = ERPDemoCHR; Integrated Security = true");                    
                }
                else
                {
                    options.UseSqlServer("Server=(local); Database=ERPDemoRRF;User Id=sa; Password=sa");
                }                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Contacto>()
                .HasOne(e => e.CentroOperacion)
                .WithOne(e => e.Contacto)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CentroOperacion>()
                   .HasOne(e => e.Regional)
                   .WithMany(c => c.CentrosOperacion)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
