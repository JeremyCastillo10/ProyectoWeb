using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWeb.Models
{
    public class UsuariosAplicacion:IdentityUser
    {
        public string NombreCompleto { get; set; }

        [NotMapped]
        public string Direccion { get; set; }

        [NotMapped]
        public string Ciudad { get; set; }
    }
}
