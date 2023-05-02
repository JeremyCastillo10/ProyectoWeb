using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Datos;
using ProyectoWeb.Models;
using ProyectoWeb.Models.VIewModels;

namespace ProyectoWeb.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Producto.Include(p => p.Categoria)
                .Include(p => p.TipoAplicacion).ToListAsync());
        }

        public IActionResult Upsert(int? Id)
        {
            //IEnumerable<SelectListItem> categoriaDropDown = _context.Categoria.Select(m => new SelectListItem
            //{
            //    Text = m.NombreCategoria,
            //    Value = m.Id.ToString()
            //});
            //ViewBag.categoriaDropDown = categoriaDropDown;
            //Producto producto = new Producto();
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = _context.Categoria.Select(c => new SelectListItem{
                    Text = c.NombreCategoria,
                    Value = c.Id.ToString()
                }),
                TipoAplicacionLista = _context.TipoAplicacion.Select(c => new SelectListItem{
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                }),



            };

            if(Id == null)
            {
                return View(productoVM);
            }
            else
            {
                productoVM.Producto = _context.Producto.Find(Id);
                if(productoVM.Producto == null)
                {
                    return NotFound();
                }
                return View(productoVM.Producto);
            }

        }
    }
}
