using System.ComponentModel.DataAnnotations;

namespace ProyectoWeb.Models
{
    public class Categoria
    {
        [Key]
        public int Id  { get; set; }
        public string? NombreCategoria { get; set; }
        public int MostrarOrden { get; set; }
    }
}
