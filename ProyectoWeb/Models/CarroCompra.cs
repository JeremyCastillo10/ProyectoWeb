using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWeb.Models
{
    public class CarroCompra
    {
        public int ProductoId { get; set; }

        [NotMapped]
        public int Cantidad { get; set;}
    }
}
