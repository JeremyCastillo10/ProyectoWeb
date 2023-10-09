using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;
using ProyectoWeb.Models.VIewModels;

namespace ProyectoWeb.Datos.Repositorio
{
    public class VentaRepositorio : Repositorio<Venta>, IVentaRepositorio
    {
        private readonly ApplicationDbContext _context;
        public VentaRepositorio(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
        public void Actualizar(Venta venta)
        {
            _context.Update(venta);
        }
    }
}

