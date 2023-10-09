using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio.IRepositorio
{
    public interface IVentaRepositorio:IRepositorio<Venta>
    {
        void Actualizar(Venta venta);
    }
}
