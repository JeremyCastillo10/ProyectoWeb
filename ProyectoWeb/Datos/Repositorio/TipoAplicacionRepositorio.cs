using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio
{
    public class TipoAplicacionRepositorio : Repositorio<TipoAplicacion>, ITipoAplicacionRepositorio
    {
        public readonly ApplicationDbContext _context;

        public TipoAplicacionRepositorio(ApplicationDbContext context):base(context) 
        { 
            _context = context;
        }

        public void Actualizar(TipoAplicacion tipo)
        {
            var tipoAnterior = _context.TipoAplicacion.FirstOrDefault(c => c.Id == tipo.Id);
            if (tipoAnterior != null)
            {
                tipoAnterior.Nombre = tipo.Nombre;
            }

        }
    }
}
