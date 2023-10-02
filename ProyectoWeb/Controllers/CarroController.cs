using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ProyectoWeb.Datos;
using ProyectoWeb.Datos.Repositorio.IRepositorio;
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
        private readonly IProductoRepositorio _productoRepos;
        private readonly IUsuariosAplicacionRepositorio _usariosAplicacionRepo;
        private readonly IOrdenRepositorio _ordenRepos;
        private readonly IOrdenDetalleRepositorio _ordenDetalleRepo;
        

        public CarroController(IProductoRepositorio productoRepo, IUsuariosAplicacionRepositorio usuariosAplicacionRepo,
            IOrdenRepositorio ordenRepo, IOrdenDetalleRepositorio ordenDetalleRepos)
        {
            _productoRepos = productoRepo;  
            _usariosAplicacionRepo = usuariosAplicacionRepo;
            _ordenRepos = ordenRepo;
            _ordenDetalleRepo = ordenDetalleRepos;
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
            IEnumerable<Producto> prodLis = _productoRepos.ObtenerTodos(p => proEnCarro.Contains(p.Id));
            List<Producto> prodListaFinal = new List<Producto>();
            foreach (var item in carroCompraslist)
            {
                Producto prodtemp = prodLis.FirstOrDefault(p => p.Id == item.ProductoId);
                prodtemp.Cantidad = item.Cantidad;
                prodListaFinal.Add(prodtemp);
                
            }

            return View(prodListaFinal);
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
            IEnumerable<Producto> prodLis = _productoRepos.ObtenerTodos(p => proEnCarro.Contains(p.Id));
            ProductoUsuarioVM = new ProductoUsuarioVM()
            {
                UsuariosAplicacion = _usariosAplicacionRepo.ObtenerPrimero(u => u.Id == claim.Value),
                ProductoLista = prodLis.ToList(),
            };
            return View(ProductoUsuarioVM);

        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [ActionName("Resumen")]
        public IActionResult ResumenPost(ProductoUsuarioVM productoUsuarioVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            Orden orden = new Orden()
            {
                UsuarioAplicacionId = claim.Value,
                NombreCompleto = productoUsuarioVM.UsuariosAplicacion.NombreCompleto,
                Telefono = productoUsuarioVM.UsuariosAplicacion.PhoneNumber,
                Email = productoUsuarioVM.UsuariosAplicacion.Email,
                FechaOrden = DateTime.Now,
            };
            
            _ordenRepos.Agregar(orden);
            _ordenRepos.Grabar();
            foreach(var prod in productoUsuarioVM.ProductoLista)
            {
                OrdenDetalle ordenDetalle = new OrdenDetalle()
                {
                    OrdenId = orden.Id,
                    ProductoId = prod.Id
                };
                _ordenDetalleRepo.Agregar(ordenDetalle);
            }

            _ordenDetalleRepo.Grabar();
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
            TempData[WC.Exitosa] = "Removido Exitosamente";
            return RedirectToAction(nameof(Index));
            
        }
    }
}
