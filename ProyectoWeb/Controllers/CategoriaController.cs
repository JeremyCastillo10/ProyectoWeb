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

        public async Task<IActionResult> Index() { 
        
            return View(await applicationDbContext.Categoria.ToListAsync());
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

            if (ModelState.IsValid)
            {
                applicationDbContext.Categoria.Add(categoria);
                applicationDbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
                return View(categoria);
        }
        //Get
        public IActionResult Edit(int Id)
        {
            if(Id == 0 || Id == null)
            {
                return NotFound();
            }
            var db = applicationDbContext.Categoria.Find(Id);
            if(db == null)
            {
                return NotFound();
            }
            return View(db);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {

            if (ModelState.IsValid)
            {
                applicationDbContext.Categoria.Update(categoria);
                applicationDbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }


    }
}
