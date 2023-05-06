namespace ProyectoWeb.Models.VIewModels
{
    public class DetalleVM
    {
        public DetalleVM()
        {
            Producto = new Producto();
        }
        public Producto Producto { get; set; }
        public bool ExisteProducto { get; set; }
    }
}
