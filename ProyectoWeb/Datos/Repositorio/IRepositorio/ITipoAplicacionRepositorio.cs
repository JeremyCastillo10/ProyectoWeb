using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio.IRepositorio
{
    public interface ITipoAplicacionRepositorio: IRepositorio<TipoAplicacion>
    {
        void Actualizar(TipoAplicacion tipo);
    }
}
