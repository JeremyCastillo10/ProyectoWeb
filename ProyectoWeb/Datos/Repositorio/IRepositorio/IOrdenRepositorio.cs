using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio.IRepositorio
{
    public interface IOrdenRepositorio:IRepositorio<Orden>
    {
        void Actualizar(Orden orden);
    }
}
