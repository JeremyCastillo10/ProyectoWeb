using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio
{
    public class OrdenRepositorio : Repositorio<Orden>, IOrdenRepositorio
    {
        private readonly ApplicationDbContext _context;
        public OrdenRepositorio(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }

        public void Actualizar(Orden orden)
        {
            _context.Update(orden);
        }
    }
}
