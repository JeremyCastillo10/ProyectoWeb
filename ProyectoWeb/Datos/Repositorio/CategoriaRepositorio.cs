
using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;
using System.Linq.Expressions;

namespace ProyectoWeb.Datos.Repositorio
{
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepositorio(ApplicationDbContext context):base(context) 
        { 
            _context = context;
        }

        public void Actualizar(Categoria categoria)
        {
            var catAnterior = _context.Categoria.FirstOrDefault(c => c.Id == categoria.Id);
            if (catAnterior != null) 
            {
                catAnterior.NombreCategoria = categoria.NombreCategoria;
                catAnterior.MostrarOrden = categoria.MostrarOrden;

            }
        }
    }
}
