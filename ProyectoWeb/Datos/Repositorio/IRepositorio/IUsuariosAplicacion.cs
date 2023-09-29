using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio.IRepositorio
{
    public interface IUsuariosAplicacion: IRepositorio<UsuariosAplicacion>
    {
        void Actualizar(UsuariosAplicacion usuariosAplicacion); 
    }
}
