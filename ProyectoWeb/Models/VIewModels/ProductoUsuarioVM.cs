namespace ProyectoWeb.Models.VIewModels
{
    public class ProductoUsuarioVM
    {
        public ProductoUsuarioVM()
        {
            ProductoLista = new List<Producto>();
        }
        public UsuariosAplicacion UsuariosAplicacion { get; set; }
        IEnumerable<Producto> ProductoLista { get; set; }
    }
}
