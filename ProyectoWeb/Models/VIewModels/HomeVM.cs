﻿namespace ProyectoWeb.Models.VIewModels
{
    public class HomeVM
    {
        public IEnumerable<Producto> Productos { get; set; }
        public IEnumerable<Categoria> Categorias { get; set; }
    }
}
