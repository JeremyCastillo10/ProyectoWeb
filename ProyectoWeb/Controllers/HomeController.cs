using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Datos;
using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;
using ProyectoWeb.Models.VIewModels;
using ProyectoWeb.Utilidades;
using System.Diagnostics;

namespace ProyectoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly ICategoriaRepositorio _categoriaRepositorio;

        public HomeController(ILogger<HomeController> logger, IProductoRepositorio productoRepo, 
            ICategoriaRepositorio categoriaRepo)
        {   
            _logger = logger;
            _productoRepositorio = productoRepo;
            _categoriaRepositorio = categoriaRepo;

        }

        public IActionResult Index()
        {
            HomeVM vm = new HomeVM()
            {
                Productos = _productoRepositorio.ObtenerTodos(incluirPropiedades: "Categoria,TipoAplicacion"),
                Categorias = _categoriaRepositorio.ObtenerTodos()
            };
            return View(vm);
        }

        public IActionResult Detalle(int Id)
        {
            List<CarroCompra> carroComprasLista = new List<CarroCompra>();
            if (HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null &&
                HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroComprasLista = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }
            DetalleVM detalleVM = new DetalleVM()
            {
                Producto = _productoRepositorio.ObtenerPrimero(p => p.Id == Id, incluirPropiedades:"Categoria,TipoAplicacion"),
               ExisteProducto = false,
            };
            foreach(var item in carroComprasLista)
            {
                if(item.ProductoId == Id)
                {
                    detalleVM.ExisteProducto = true;
                }
            }
            return View(detalleVM); 
        }

        [HttpPost, ActionName("Detalle")]
        public IActionResult DetallePost(int Id)
        {
            List<CarroCompra> carroComprasLista = new List<CarroCompra>();
            if(HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null &&
                HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroComprasLista = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }
            carroComprasLista.Add(new CarroCompra { ProductoId = Id});
            HttpContext.Session.Set(WC.SessionCarroCompras, carroComprasLista);

            return RedirectToAction(nameof(Index));

        }
        public IActionResult RemoverDeCarro(int Id)
        {
            List<CarroCompra> carroComprasLista = new List<CarroCompra>();
            if (HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null &&
                HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroComprasLista = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }
            var productoAremovver = carroComprasLista.SingleOrDefault(x => x.ProductoId == Id); 
            if(productoAremovver != null)
            {
                carroComprasLista.Remove(productoAremovver);
                TempData[WC.Exitosa] = "Removido Exitosamente";
            }
            HttpContext.Session.Set(WC.SessionCarroCompras, carroComprasLista);

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}