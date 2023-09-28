using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Datos;
using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;
using ProyectoWeb.Models.VIewModels;
using System.Data;

namespace ProyectoWeb.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ProductoController : Controller
    {
        private readonly IProductoRepositorio _prodRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;   

        public ProductoController(IProductoRepositorio prodRepo, IWebHostEnvironment webHostEnvironment)
        {
            _prodRepo = prodRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Producto> lista = _prodRepo.ObtenerTodos(incluirPropiedades: "Categoria,TipoAplicacion");
            return View(lista);
        }


        //GET
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
                CategoriaLista = _prodRepo.ObtenerTodosDropDownList(WC.CategoriaNombre),
                TipoAplicacionLista = _prodRepo.ObtenerTodosDropDownList(WC.TipoAplicacionNombre)
            };

            if (Id == null)
            {
                // Crear un Nuevo Producto
                return View(productoVM);
            }
            else
            {
                productoVM.Producto = _prodRepo.Obtener(Id.GetValueOrDefault());
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
                    _prodRepo.Agregar(productoVM.Producto);
                }
                else
                {
                    // Actualizar
                    var objProducto = _prodRepo.ObtenerPrimero(p => p.Id == productoVM.Producto.Id, isTracking :false);

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
                    _prodRepo.Actualizar(productoVM.Producto);

                }
                _prodRepo.Grabar();
                return RedirectToAction("Index");
            }  // If ModelIsValid
               // Se llenan nuevamente las listas si algo falla

            productoVM.CategoriaLista = _prodRepo.ObtenerTodosDropDownList(WC.CategoriaNombre);
            productoVM.TipoAplicacionLista = _prodRepo.ObtenerTodosDropDownList(WC.TipoAplicacionNombre);

            return View(productoVM);
        }

        //GET
        public IActionResult Delete(int ? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }
            Producto producto = _prodRepo.ObtenerPrimero(p => p.Id == Id, incluirPropiedades:"Categoria,TipoAplicacion");


            if(producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Producto producto)
        {
            if(producto == null)
            {
                return NotFound();
            }
            string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;

            var anteriorFile = Path.Combine(upload, producto.ImagenUrl);
            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }
            _prodRepo.Remover(producto);
            _prodRepo.Grabar();
            return RedirectToAction(nameof(Index));   
        }



    }
}
