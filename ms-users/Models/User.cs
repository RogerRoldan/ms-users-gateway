using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ms_users.Models
{
    [Table("users")] // Especifica el nombre de la tabla en la base de datos
    public class User
    {

        // Atributos y validaciones para cada propiedad
        [Key] // Indica que esta propiedad es la llave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Generación automática del ID
        [JsonProperty("id")] // Define el nombre de la propiedad en JSON
        [Column("id")] 
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre completo no debe exceder los 100 caracteres.")]
        [JsonProperty("full_name")] // Define el nombre de la propiedad en JSON
        [Column("full_name")] 
        public string FullName { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de usuario no debe exceder los 100 caracteres.")]
        [JsonProperty("username")]
        [Column("username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [JsonProperty("email")]
        [Column("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 255 caracteres.")]
        [JsonProperty("password")]
        [Column("password")]
        public string Password { get; set; }

        [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
        [StringLength(20, ErrorMessage = "El número de teléfono no debe exceder los 20 caracteres.")]
        [JsonProperty("phone_number")]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("created_at")]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("is_active")]
        [Column("is_active")]
        public bool IsActive { get; set; }

        // Constructor sin parámetros
        public User()
        {
            Id = 0;
            FullName = string.Empty;
            Email = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            PhoneNumber = string.Empty;
            CreatedAt = DateTime.Now;
            IsActive = true;
        }

        // Constructor con parámetros
        public User(string fullName,string username, string email, string password, string phoneNumber)
        {
            FullName = fullName;
            Email = email;
            Username = username;
            Password = password;
            PhoneNumber = phoneNumber;
            CreatedAt = DateTime.Now;
            IsActive = true;
        }
    }
}

