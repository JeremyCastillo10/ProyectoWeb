using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {

            return View(await _applicationDbContext.TipoAplicacion.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Create(TipoAplicacion tipo)
        {
            if (ModelState.IsValid) {
                _applicationDbContext.TipoAplicacion.Add(tipo);
                _applicationDbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(tipo);
            
        }

        //GET
        public IActionResult Edit(int Id)
        {
            if(Id == 0 || Id == 0)
            {
                return NotFound();
            }
            var db = _applicationDbContext.TipoAplicacion.Find(Id);
            if (db == null) {
                return NotFound();
            }
            return View(db);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Edit(TipoAplicacion tipo)
        {
            if (ModelState.IsValid)
            {
                _applicationDbContext.TipoAplicacion.Update(tipo);
                _applicationDbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(tipo);

        }
        //GET
        public IActionResult Delete(int Id)
        {
            if (Id == 0 || Id == 0)
            {
                return NotFound();
            }
            var db = _applicationDbContext.TipoAplicacion.Find(Id);
            if (db == null)
            {
                return NotFound();
            }
            return View(db);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Delete(TipoAplicacion tipo)
        {
            if(tipo == null)
            {
                return NotFound();
            }
                _applicationDbContext.TipoAplicacion.Remove(tipo);
                _applicationDbContext.SaveChanges();

                return RedirectToAction(nameof(Index));

        }


    }
}



