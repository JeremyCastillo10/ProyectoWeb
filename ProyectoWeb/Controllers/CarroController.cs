using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ProyectoWeb.Datos;
using ProyectoWeb.Models;
using ProyectoWeb.Models.VIewModels;
using ProyectoWeb.Utilidades;
using System.Security.Claims;

namespace ProyectoWeb.Controllers
{
    [Authorize]
    public class CarroController : Controller
    {
        [BindProperty]
        public ProductoUsuarioVM ProductoUsuarioVM { get; set; }
        private readonly ApplicationDbContext _applicationDbContext;
        public IEmailSender EmailSender { get; set; }
        

        public CarroController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            List<CarroCompra> carroCompraslist = new List<CarroCompra>();
            if(HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null &&
                HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroCompraslist = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }
            List<int> proEnCarro = carroCompraslist.Select(p => p.ProductoId).ToList();
            IEnumerable<Producto> prodLis = _applicationDbContext.Producto.Where(p => proEnCarro.Contains(p.Id));
            return View(prodLis);
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            return RedirectToAction(nameof(Resumen));
        }

        public IActionResult Resumen()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            List<CarroCompra> carroCompraslist = new List<CarroCompra>();
            if (HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null &&
                HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroCompraslist = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }
            List<int> proEnCarro = carroCompraslist.Select(p => p.ProductoId).ToList();
            IEnumerable<Producto> prodLis = _applicationDbContext.Producto.Where(p => proEnCarro.Contains(p.Id));
            ProductoUsuarioVM = new ProductoUsuarioVM()
            {
                UsuariosAplicacion = _applicationDbContext.UsuariosAplicacion.FirstOrDefault(i => i.Id == claim.Value),
                ProductoLista = prodLis.ToList(),
            };
            return View(ProductoUsuarioVM);

        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [ActionName("Resumen")]
        public IActionResult ResumenPost(ProductoUsuarioVM productoUsuarioVM)
        {
            return RedirectToAction(nameof(Confirmacion));

        }
        public IActionResult Confirmacion() 
        {
            HttpContext.Session.Clear();
            return View();
        } 
        public IActionResult Remover(int Id)
        {
            List<CarroCompra> carroCompraslist = new List<CarroCompra>();
            if (HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null &&
                HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroCompraslist = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }
            carroCompraslist.Remove(carroCompraslist.FirstOrDefault(p => p.ProductoId == Id));
            HttpContext.Session.Set(WC.SessionCarroCompras, carroCompraslist);
            return RedirectToAction(nameof(Index));
        }
    }
}
