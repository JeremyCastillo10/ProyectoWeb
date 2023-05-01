﻿using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Models;

namespace ProyectoWeb.Datos
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<TipoAplicacion> TipoAplicacion { get; set; }
        public DbSet<Producto> Producto { get; set; }   
    }
}
