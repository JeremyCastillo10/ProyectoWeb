using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Datos;
using ProyectoWeb.Models;

namespace ProyectoWeb.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        public CategoriaController(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Categoria> lista = applicationDbContext.Categoria;

            return View(lista);
        }


        //Get
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            applicationDbContext.Categoria.Add(categoria);
            applicationDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }




    }
}
