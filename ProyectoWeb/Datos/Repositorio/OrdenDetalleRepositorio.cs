using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio
{
    public class OrdenDetalleRepositorio : Repositorio<OrdenDetalle>, IOrdenDetalleRepositorio
    {
        private readonly ApplicationDbContext _context;

        public OrdenDetalleRepositorio(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }

        public void Actualizar(OrdenDetalle ordenDetalle)
        {
            _context.Update(ordenDetalle);
        }
    }
}
