using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio.IRepositorio
{
    public interface IVentaDetalleRepositorio: IRepositorio<VentaDetalle>
    {
        void Actualizar(VentaDetalle ventaDetalle);
    }
}
