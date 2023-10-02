using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Datos;
using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;
using System.Data;


namespace ProyectoWeb.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class CategoriaController : Controller
    {
        public readonly ICategoriaRepositorio _catRepo;

        public CategoriaController(ICategoriaRepositorio catRepo)
        {
            _catRepo = catRepo;
        }

        public IActionResult Index() {

            IEnumerable<Categoria> lista = _catRepo.ObtenerTodos();
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

            if (ModelState.IsValid)
            {
                _catRepo.Agregar(categoria);
                _catRepo.Grabar();
                TempData[WC.Exitosa] = "Creado Exiotasamente";

                return RedirectToAction(nameof(Index));
            }
            TempData[WC.Error] = "Error al crear";
                return View(categoria);
        }
        //Get
        public IActionResult Edit(int ?Id)
        {
            if(Id == 0 || Id == null)
            {
                return NotFound();
            }
            var db = _catRepo.Obtener(Id.GetValueOrDefault());
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
               _catRepo.Actualizar(categoria);
                _catRepo.Grabar();
                TempData[WC.Exitosa] = "Editado Exiotasamente";

                return RedirectToAction(nameof(Index));
            }
            TempData[WC.Error] = "Erro al Editar";
            return View(categoria);
        }
        public IActionResult Delete(int ?Id)
        {
            if (Id == 0 || Id == null)
            {
                return NotFound();
            }
            var db = _catRepo.Obtener(Id.GetValueOrDefault());
            if (db == null)
            {
                return NotFound();
            }
            return View(db);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Categoria categoria)
        {
            if(categoria == null)
            {
                return NotFound();

            }
                _catRepo.Remover(categoria);
                _catRepo.Grabar();
            TempData[WC.Exitosa] = "Removido Exitosamente";

            return RedirectToAction(nameof(Index));
        }


    }
}
