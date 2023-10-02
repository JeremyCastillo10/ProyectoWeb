using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWeb.Models
{
    public class Producto
    {
        public Producto()
        {
            Cantidad = 1;
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es Obligatorio")]
        public string NombreProducto { get; set; }
        [Required(ErrorMessage = "La descripcion corta es Obligatoria")]
        public string DescripcionCorta { get; set; }
        [Required(ErrorMessage = "La descripcion del producto es obligatoria")]
        public string DescripcionProducto { get; set; }
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public double Precio { get; set; }

        public string ?ImagenUrl { get; set; }

        //Foreign Key

        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public virtual Categoria ?Categoria { get; set; }

        public int TipoAplicacionId { get; set; }
        [ForeignKey("TipoAplicacionId")]
        public virtual TipoAplicacion? TipoAplicacion { get; set; }

        [Range(1, 10000)]
        public int Cantidad { get; set; }
    }
}
