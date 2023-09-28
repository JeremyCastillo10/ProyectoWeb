using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWeb.Models
{
    public class Orden
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioAplicacionId { get; set; }
        [ForeignKey("UsuarioAplicacionId")]
        public UsuariosAplicacion UsuarioAplicacion { get; set; }
        public DateTime FechaOrden { get; set; }

        [Required]
        public string Telefono { get; set; }
        [Required]
        public string NombreCompleto { get; set; }
        [Required]
        public string Email{ get; set; }

    }
}
