using Microsoft.AspNetCore.Mvc;
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


    }
}



