using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Datos;
using ProyectoWeb.Models;

namespace ProyectoWeb.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        public CategoriaController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {

            return View(await applicationDbContext.Categoria.ToListAsync());
        }


        //Get
        public async Task<IActionResult> Create()
        {

            return View(await applicationDbContext.Categoria.ToListAsync());
        }




    }
}
