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
        private readonly IWebHostEnvironment _webHostEnvironment;   

        public ProductoController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Producto> lista = _context.Producto.Include(c => c.Categoria)
                                                      .Include(t => t.TipoAplicacion);
            return View(lista);
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
                CategoriaLista = _context.Categoria.Select(c => new SelectListItem
                {
                    Text = c.NombreCategoria,
                    Value = c.Id.ToString()
                }),
                TipoAplicacionLista = _context.TipoAplicacion.Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                })
            };

            if (Id == null)
            {
                // Crear un Nuevo Producto
                return View(productoVM);
            }
            else
            {
                productoVM.Producto = _context.Producto.Find(Id);
                if (productoVM.Producto == null)
                {
                    return NotFound();
                }
                return View(productoVM);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult Upsert(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (productoVM.Producto.Id == 0)
                {
                    // Crear
                    string upload = webRootPath + WC.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productoVM.Producto.ImagenUrl = fileName + extension;
                    _context.Producto.Add(productoVM.Producto);
                }
                else
                {
                    // Actualizar
                    var objProducto = _context.Producto.AsNoTracking().FirstOrDefault(p => p.Id == productoVM.Producto.Id);

                    if (files.Count > 0)  // Se carga nueva Imagen
                    {
                        string upload = webRootPath + WC.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        // borrar la imagen anterior
                        var anteriorFile = Path.Combine(upload, objProducto.ImagenUrl);
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        // fin Borrar imagen anterior

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productoVM.Producto.ImagenUrl = fileName + extension;
                    }  // Caso contrario si no se carga una nueva imagen
                    else
                    {
                        productoVM.Producto.ImagenUrl = objProducto.ImagenUrl;
                    }
                    _context.Producto.Update(productoVM.Producto);

                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }  // If ModelIsValid
            // Se llenan nuevamente las listas si algo falla
            productoVM.CategoriaLista = _context.Categoria.Select(c => new SelectListItem
            {
                Text = c.NombreCategoria,
                Value = c.Id.ToString()
            });
            productoVM.TipoAplicacionLista = _context.TipoAplicacion.Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.Id.ToString()
            });

            return View(productoVM);
        }
    }
}
