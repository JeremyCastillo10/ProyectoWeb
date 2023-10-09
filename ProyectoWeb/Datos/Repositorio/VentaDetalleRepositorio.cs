using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio
{
    public class VentaDetalleRepositorio : Repositorio<VentaDetalle>, IVentaDetalleRepositorio
    {
        private readonly ApplicationDbContext _context;

        public VentaDetalleRepositorio(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }

        public void Actualizar(VentaDetalle ventaDetalle)
        {
            _context.Update(ventaDetalle);
        }
    }
}
