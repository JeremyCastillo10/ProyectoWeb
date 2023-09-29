using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio.IRepositorio
{
    public interface IUsuariosAplicacionRepositorio: IRepositorio<UsuariosAplicacion>
    {
        void Actualizar(UsuariosAplicacion usuariosAplicacion); 
    }
}
