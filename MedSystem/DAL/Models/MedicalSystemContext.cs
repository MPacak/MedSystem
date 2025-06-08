using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MedicalSystemContext : DbContext
    {
        public MedicalSystemContext(DbContextOptions<MedicalSystemContext> options) : base(options)
         {
            // Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
         }

        /* public const string CONNECTION_STRING = @"
            Host=severely-champion-ling.data-1.use1.tembo.io;Port=5432;Username=postgres;Password=Yme5WV0N40FMmQH4;Database=postgres;SSL Mode=Require
        ";*/
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Checkup> Checkups { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
              //  optionsBuilder.UseNpgsql(CONNECTION_STRING);
           
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Gender>().HasData(
              new Gender { Id = 1, Type = "Male" },
              new Gender { Id = 2, Type = "Female" }
          );
           foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("timestamp without time zone"); 
                    }
                }
            }

            modelBuilder.Entity<Drug>()
        .Property(d => d.DoseType)
        .HasConversion<string>();
        }
    }
}
