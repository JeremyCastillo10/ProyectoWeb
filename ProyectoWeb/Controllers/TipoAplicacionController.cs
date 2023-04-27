﻿using Microsoft.AspNetCore.Mvc;
using ProyectoWeb.Datos;
using ProyectoWeb.Models;

namespace ProyectoWeb.Controllers
{
    public class TipoAplicacionController : Controller
    {
        private ApplicationDbContext _applicationDbContext;

        public TipoAplicacionController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<TipoAplicacion> lista = _applicationDbContext.TipoAplicacion;
            return View(lista);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Create(TipoAplicacion tipo)
        {
            _applicationDbContext.TipoAplicacion.Add(tipo);
            _applicationDbContext.SaveChanges();

           return RedirectToAction(nameof(Index));
        }


    }
}



