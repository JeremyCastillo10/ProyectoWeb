using Microsoft.AspNetCore.Mvc;
using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;
using ProyectoWeb.Models.VIewModels;
using ProyectoWeb.Utilidades;

namespace ProyectoWeb.Controllers
{
    public class OrdenController : Controller
    {
        private readonly IOrdenRepositorio _ordenRepo;
        private readonly IOrdenDetalleRepositorio _ordenDetalleRepo;

        [BindProperty]
        public OrdenVM OrdenVM { get; set; }
        public OrdenController(IOrdenRepositorio ordenRepo, IOrdenDetalleRepositorio ordenDetalleRepo)
        {
            _ordenRepo = ordenRepo;
            _ordenDetalleRepo = ordenDetalleRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detalle(int id) 
        {
            OrdenVM = new OrdenVM()
            {
                orden = _ordenRepo.ObtenerPrimero(o => o.Id == id),
                ordenDetalles = _ordenDetalleRepo.ObtenerTodos(o=> o.OrdenId == id, incluirPropiedades:"Producto")
            };
            return View(OrdenVM);
        }
        [HttpPost]
        public IActionResult Detalle() 
        {
            List<CarroCompra> carroComprasLista = new List<CarroCompra>();

            OrdenVM.ordenDetalles = _ordenDetalleRepo.ObtenerTodos(o => o.OrdenId == OrdenVM.orden.Id);
            foreach(var detalle in OrdenVM.ordenDetalles)
            {
                CarroCompra carroCompra = new CarroCompra() 
                {
                    ProductoId = detalle.ProductoId
                };
                carroComprasLista.Add(carroCompra);
            }
            HttpContext.Session.Clear();
            HttpContext.Session.Set(WC.SessionCarroCompras, carroComprasLista);
            HttpContext.Session.Set(WC.SessionOrdenId, OrdenVM.orden.Id);

            return RedirectToAction("Index", "Carro");
        }
        #region APIS
        [HttpGet]
        public IActionResult ObtenerListaOrdenes()
        {
            return Json(new { data = _ordenRepo.ObtenerTodos() });
        }

        #endregion
    }
}
