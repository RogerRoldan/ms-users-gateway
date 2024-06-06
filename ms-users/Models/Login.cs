using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_users.Models
{
    [NotMapped]
    public class Login
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set; }

        public Login()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

        public Login(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public struct ResponseLogin()
    {
        public User User { get; set; }
        public string AccessToken { get; set; }
    }
}
