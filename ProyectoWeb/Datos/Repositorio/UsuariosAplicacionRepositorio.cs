using ProyectoWeb.Datos.Repositorio.IRepositorio;
using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio
{
    public class UsuariosAplicacionRepositorio : Repositorio<UsuariosAplicacion>, IUsuariosAplicacionRepositorio
    {
        private readonly ApplicationDbContext _context;

        public UsuariosAplicacionRepositorio(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Actualizar(UsuariosAplicacion usuariosAplicacion)
        {
            _context.Update(usuariosAplicacion);    
        }
    }
}
