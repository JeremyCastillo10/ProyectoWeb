﻿using Microsoft.AspNetCore.Mvc;
using ProyectoWeb.Datos;
using ProyectoWeb.Models;
using ProyectoWeb.Utilidades;

namespace ProyectoWeb.Controllers
{
    public class CarroController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CarroController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            List<CarroCompra> carroCompraslist = new List<CarroCompra>();
            if(HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null &&
                HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroCompraslist = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }
            List<int> proEnCarro = carroCompraslist.Select(p => p.ProductoId).ToList();
            IEnumerable<Producto> prodLis = _applicationDbContext.Producto.Where(p => proEnCarro.Contains(p.Id));
            return View(prodLis);
        }
    }
}