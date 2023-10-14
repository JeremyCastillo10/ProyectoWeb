using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoWeb.Models.VIewModels
{
    public class VentaVM
    {
        public IEnumerable<Venta> VentaLista { get; set; }
        public IEnumerable<SelectListItem> EstadoLista { get; set; }
        public IEnumerable<VentaDetalle> ventaDetalles { get; set; }
        public IEnumerable<Producto> prodLista { get; set; }
        public Venta venta { get; set; }



        public string Estado { get; set; }
    }
}
