namespace ProyectoWeb.Models.VIewModels
{
    public class OrdenVM
    {
        public Orden orden { get; set; }
        public IEnumerable<OrdenDetalle> ordenDetalles { get; set; }
    }
}
