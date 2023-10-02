using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Datos;
using ProyectoWeb.Datos.Repositorio;
using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;

namespace ProyectoWeb.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class TipoAplicacionController : Controller
    {
        public readonly ITipoAplicacionRepositorio _tipoRepo;



        public TipoAplicacionController(ITipoAplicacionRepositorio tipoRepo)
        {
            _tipoRepo = tipoRepo;
        }
        public  IActionResult Index()
        {
            IEnumerable<TipoAplicacion> lista = _tipoRepo.ObtenerTodos();

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
            if (ModelState.IsValid) {
                _tipoRepo.Agregar(tipo);
                _tipoRepo.Grabar();

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
            var db = _tipoRepo.Obtener(Id);
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
                _tipoRepo.Actualizar(tipo);
                _tipoRepo.Grabar();

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
            var db = _tipoRepo.Obtener(Id);
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
                _tipoRepo.Remover(tipo);
                _tipoRepo.Grabar();

                return RedirectToAction(nameof(Index));

        }


    }
}



