using ProyectoWeb.Models;

namespace ProyectoWeb.Datos.Repositorio.IRepositorio
{
    public interface ICategoriaRepositorio: IRepositorio<Categoria>
    {
        void Actualizar(Categoria categoria);
    }
}
