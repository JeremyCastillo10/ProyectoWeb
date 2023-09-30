using Microsoft.AspNetCore.Mvc;
using ProyectoWeb.Datos.Repositorio.IRepositorio;

namespace ProyectoWeb.Controllers
{
    public class OrdenController : Controller
    {
        private readonly IOrdenRepositorio _ordenRepo;
        private readonly IOrdenDetalleRepositorio _ordenDetalleRepo;
        public OrdenController(IOrdenRepositorio ordenRepo, IOrdenDetalleRepositorio ordenDetalleRepo)
        {
            _ordenRepo = ordenRepo;
            _ordenDetalleRepo = ordenDetalleRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
