using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models.VIewModels;

namespace ProyectoWeb.Controllers
{
    public class VentaController : Controller
    {
        private readonly IVentaRepositorio _ventaRepo;
        private readonly IVentaDetalleRepositorio _ventaDetalleRepo;

        public VentaController(IVentaRepositorio ventaRepo, IVentaDetalleRepositorio ventaDetalleRepo)
        {
            _ventaRepo = ventaRepo;
            _ventaDetalleRepo = ventaDetalleRepo;
            
        }
        public IActionResult Index()
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
            return View(ventaVM);
        }
    }
}
