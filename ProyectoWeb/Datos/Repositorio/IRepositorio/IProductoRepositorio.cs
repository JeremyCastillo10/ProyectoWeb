using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio.IRepositorio
{
    public interface IProductoRepositorio: IRepositorio<Producto>
    {
        void Actualizar(Producto producto);

        IEnumerable<SelectListItem>ObtenerTodosDropDownList(string obj);
    }
}
