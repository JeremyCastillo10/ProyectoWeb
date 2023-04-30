using System.ComponentModel.DataAnnotations;

namespace ProyectoWeb.Models
{
    public class Categoria
    {
        [Key]
        public int Id  { get; set; }

        [Required(ErrorMessage ="Nombre de categoria Obligatorio")]
        public string? NombreCategoria { get; set; }

        [Required(ErrorMessage = "Orden Obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage ="El orden debe ser mayor a cero")]
        public int MostrarOrden { get; set; }
    }
}
