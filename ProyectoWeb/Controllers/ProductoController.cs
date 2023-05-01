using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Datos;
using ProyectoWeb.Models;


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
            IEnumerable<SelectListItem> categoriaDropDown = _context.Categoria.Select(m => new SelectListItem
            {
                Text = m.NombreCategoria,
                Value = m.Id.ToString()
            });
            ViewBag.categoriaDropDown = categoriaDropDown;
            Producto producto = new Producto();

            if(Id == null)
            {
                return View(producto);
            }
            else
            {
                producto = _context.Producto.Find(Id);
                if(producto == null)
                {
                    return NotFound();
                }
                return View(producto);
            }

        }
    }
}
