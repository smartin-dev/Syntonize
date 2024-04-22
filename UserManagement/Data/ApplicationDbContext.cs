using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UserManagement.Data.Models;

namespace UserManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base(ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString)
        {
        }

        // Agrega DbSet para cada entidad de tu base de datos
        public DbSet<User> Users { get; set; }
        // Agrega más DbSet según sea necesario para otras entidades

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configura el modelo de base de datos, como restricciones, relaciones, etc.
            // Por ejemplo:
            // modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();
        }
    }
}