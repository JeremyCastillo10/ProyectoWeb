using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;
using ProyectoWeb.Models.VIewModels;
using ProyectoWeb.Utilidades;

namespace ProyectoWeb.Controllers
{
    public class VentaController : Controller
    {
        private readonly IOrdenRepositorio _ordenRepo;
        private readonly IOrdenDetalleRepositorio _ordenDetalleRepo;
        private readonly IVentaRepositorio _ventaRepo;
        private readonly IVentaDetalleRepositorio _ventaDetalleRepo;
        [BindProperty]
        public VentaVM ventaVM { get; set; }
        public VentaController(IVentaRepositorio ventaRepo, IVentaDetalleRepositorio ventaDetalleRepo, IOrdenRepositorio ordenRepo, IOrdenDetalleRepositorio ordenDetalleRepo)
        {
            _ordenRepo = ordenRepo;
            _ordenDetalleRepo = ordenDetalleRepo;
            _ventaRepo = ventaRepo;
            _ventaDetalleRepo = ventaDetalleRepo;
            
        }
        public IActionResult Index(string buscarNombre=null, string buscarEmail=null, string buscarTelefono = null, string Estado = null)
        {
            VentaVM ventaVM = new VentaVM()
            {
                VentaLista = _ventaRepo.ObtenerTodos(),
                EstadoLista = WC.ListaEstados.ToList().Select(l => new SelectListItem
                {
                    Text = l,
                    Value = l
                })
            };
            if(!string.IsNullOrEmpty(buscarNombre))
            {
                ventaVM.VentaLista = ventaVM.VentaLista.Where(u => u.NombreCompleto.ToLower().Contains(buscarNombre.ToLower()));
            }
            if (!string.IsNullOrEmpty(buscarEmail))
            {
                ventaVM.VentaLista = ventaVM.VentaLista.Where(u => u.Email.ToLower().Contains(buscarEmail.ToLower()));
            }
            if (!string.IsNullOrEmpty(buscarTelefono))
            {
                ventaVM.VentaLista = ventaVM.VentaLista.Where(u => u.Telefono.ToLower().Contains(buscarTelefono.ToLower()));
            }
            if (!string.IsNullOrEmpty(Estado) && Estado!= "--Estado--")
            {
                ventaVM.VentaLista = ventaVM.VentaLista.Where(u => u.EstadoVenta.ToLower().Contains(Estado.ToLower()));
            }
            return View(ventaVM);
        }
        public IActionResult Detalle(int id)
        {
            VentaVM ventaVM = new VentaVM()
            {
                venta = _ventaRepo.ObtenerPrimero(o => o.Id == id),
                ventaDetalles = _ventaDetalleRepo.ObtenerTodos(o => o.VentaId == id, incluirPropiedades: "Producto")
            };
            return View(ventaVM);
        }
        public IActionResult Eliminar()
        {
            Venta venta = _ventaRepo.ObtenerPrimero(o => o.Id == ventaVM.venta.Id);
            IEnumerable<VentaDetalle> ListaDetalle = _ventaDetalleRepo.ObtenerTodos(d => d.Id == ventaVM.venta.Id);
            _ventaDetalleRepo.RemoverTodos(ListaDetalle);
            _ventaRepo.Remover(venta);
            _ventaRepo.Grabar();
            TempData[WC.Exitosa] = "Removido Exitosamente";
            return RedirectToAction("Index");

        }

    }
}
