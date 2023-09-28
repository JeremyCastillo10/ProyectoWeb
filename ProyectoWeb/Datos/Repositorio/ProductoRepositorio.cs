using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        public readonly ApplicationDbContext _context;

        public ProductoRepositorio(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Actualizar(Producto producto)
        {
            _context.Update(producto);
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj)
        {
            if(obj == WC.CategoriaNombre)
            {
                return _context.Categoria.Select(c => new SelectListItem
                {
                    Text = c.NombreCategoria,
                    Value = c.Id.ToString()
                });
            }
            if(obj == WC.TipoAplicacionNombre) 
            {
                return _context.TipoAplicacion.Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                });
            }
            return null;
        }
    }
}
