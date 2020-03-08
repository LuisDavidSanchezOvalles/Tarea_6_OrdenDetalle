using System;
using System.Collections.Generic;
using System.Text;
using RegistroOrdenDetalle.Entidades;
using Microsoft.EntityFrameworkCore;

namespace RegistroOrdenDetalle.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = DAL\Ordenes.db");
        }
    }
}
