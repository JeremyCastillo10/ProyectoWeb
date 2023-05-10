using Microsoft.AspNetCore.Identity;

namespace ProyectoWeb.Models
{
    public class UsuariosAplicacion:IdentityUser
    {
        public string NombreCompleto { get; set; }
    }
}
