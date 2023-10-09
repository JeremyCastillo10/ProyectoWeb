using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio.IRepositorio
{
    public interface IOrdenDetalleRepositorio: IRepositorio<OrdenDetalle>
    {
        void Actualizar(OrdenDetalle ordenDetalle);
    }
}
