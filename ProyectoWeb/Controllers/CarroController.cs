using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using ProyectoWeb.Datos;
using ProyectoWeb.Datos.Repositorio;
using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;
using ProyectoWeb.Models.VIewModels;
using ProyectoWeb.Utilidades;
using ProyectoWeb.Utilidades.BrainTree;
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
        private readonly IVentaRepositorio _ventaRepos;
        private readonly IVentaDetalleRepositorio _ventaDetalleRepo;
        private readonly IBraintTreeGate _brain;


        public CarroController(IProductoRepositorio productoRepo, IUsuariosAplicacionRepositorio usuariosAplicacionRepo,
            IOrdenRepositorio ordenRepo, IOrdenDetalleRepositorio ordenDetalleRepos, IVentaRepositorio ventaRepo, IVentaDetalleRepositorio ventaDetalleRepo
            , IBraintTreeGate brain)
        {
            _productoRepos = productoRepo;  
            _usariosAplicacionRepo = usuariosAplicacionRepo;
            _ordenRepos = ordenRepo;
            _ordenDetalleRepo = ordenDetalleRepos;
            _ventaRepos = ventaRepo;
            _ventaDetalleRepo = ventaDetalleRepo;
            _brain = brain;
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
        public IActionResult IndexPost(IEnumerable<Producto> prodLista)
        {
            List<CarroCompra> carroComprasLista = new List<CarroCompra>();
            foreach (Producto prod in prodLista)
            {
                carroComprasLista.Add(new CarroCompra
                {
                    ProductoId = prod.Id,
                    Cantidad = prod.Cantidad,
                });
            }
            HttpContext.Session.Set(WC.SessionCarroCompras, carroComprasLista);


            return RedirectToAction(nameof(Resumen));
        }

        public IActionResult Resumen()
        {
            UsuariosAplicacion usuarioAplicacion;
            if (User.IsInRole(WC.AdminRole))
            {
                if (HttpContext.Session.Get<int>(WC.SessionOrdenId) != 0)
                {
                    Orden orden = _ordenRepos.ObtenerPrimero(u => u.Id == HttpContext.Session.Get<int>(WC.SessionOrdenId));
                    usuarioAplicacion = new UsuariosAplicacion()
                    {
                        Email = orden.Email,
                        NombreCompleto = orden.NombreCompleto,
                        PhoneNumber = orden.Telefono
                    };
                }
                else
                {
                    usuarioAplicacion = new UsuariosAplicacion();
                }
                var gateway = _brain.GetGateway();
                var clientToken = gateway.ClientToken.Generate();
                ViewBag.ClientToken= clientToken;
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                usuarioAplicacion = _usariosAplicacionRepo.ObtenerPrimero(u => u.Id == claim.Value);

   
            }
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
                UsuariosAplicacion = usuarioAplicacion,

            };
            foreach(var carro in carroCompraslist) 
            {
                Producto prodTemp = _productoRepos.ObtenerPrimero(p => p.Id == carro.ProductoId);
                prodTemp.Cantidad = carro.Cantidad;
                ProductoUsuarioVM.ProductoLista.Add(prodTemp);
            }
            return View(ProductoUsuarioVM);

        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [ActionName("Resumen")]
        public IActionResult ResumenPost(ProductoUsuarioVM productoUsuarioVM)
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (User.IsInRole(WC.AdminRole))
            {
                //creamos venta
                Venta venta = new Venta()
                {
                    CreadoPorUsusarioId = claim.Value,
                    FinalVentaTotal = productoUsuarioVM.ProductoLista.Sum(x => x.Precio * x.Cantidad),
                    Direccion = productoUsuarioVM.UsuariosAplicacion.Direccion,
                    Ciudad = productoUsuarioVM.UsuariosAplicacion.Ciudad,
                    Email = productoUsuarioVM.UsuariosAplicacion.Email,
                    Telefono = productoUsuarioVM.UsuariosAplicacion.PhoneNumber,
                    NombreCompleto = productoUsuarioVM.UsuariosAplicacion.NombreCompleto,
                    FechaVenta = DateTime.Now,
                    EstadoVenta = WC.Estadopendiente

                };
                _ventaRepos.Agregar(venta);
                _ventaRepos.Grabar();

                foreach(var prod in productoUsuarioVM.ProductoLista)
                {
                    VentaDetalle ventaDetalle = new VentaDetalle()
                    {
                        VentaId = venta.Id,
                        PrecioCU = prod.Precio,
                        Cantidad = prod.Cantidad,
                        ProductoId= prod.Id,
                    };
                    _ventaDetalleRepo.Agregar(ventaDetalle);
                }
                _ventaDetalleRepo.Grabar();
                return RedirectToAction(nameof(Confirmacion), new {id = venta.Id});
            }
            else
            {
                //creamos orden
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
                foreach (var prod in productoUsuarioVM.ProductoLista)
                {
                    OrdenDetalle ordenDetalle = new OrdenDetalle()
                    {
                        OrdenId = orden.Id,
                        ProductoId = prod.Id
                    };
                    _ordenDetalleRepo.Agregar(ordenDetalle);
                }
            }
           

            _ordenDetalleRepo.Grabar();
            return RedirectToAction(nameof(Confirmacion));

        }
        public IActionResult Confirmacion(int id = 0) 
        {
            Venta venta = _ventaRepos.ObtenerPrimero(v => v.Id == id);
            HttpContext.Session.Clear();
            return View(venta);
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
        public IActionResult ActualizarCarro(IEnumerable<Producto> prodLista)
        {
            List<CarroCompra> carroComprasLista = new List<CarroCompra>();
            foreach(Producto prod in prodLista)
            {
                carroComprasLista.Add(new CarroCompra
                {
                    ProductoId = prod.Id,
                    Cantidad = prod.Cantidad,
                });
            }
            HttpContext.Session.Set(WC.SessionCarroCompras, carroComprasLista);

            return RedirectToAction(nameof(Index));
        }
    }
}
