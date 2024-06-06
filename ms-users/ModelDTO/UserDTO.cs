using ms_users.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_users.ModelDTO
{
    [NotMapped]
    public class UserDTO
    {
        [JsonProperty("id")]
        [Required(ErrorMessage = "El ID es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre completo no debe exceder los 100 caracteres.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de usuario no debe exceder los 100 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
        [StringLength(20, ErrorMessage = "El número de teléfono no debe exceder los 20 caracteres.")]
        public string PhoneNumber { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        public UserDTO()
        {
            Id = 0;
            FullName = string.Empty;
            Email = string.Empty;
            Username = string.Empty;
            CreatedAt = string.Empty;
            PhoneNumber = string.Empty;
            IsActive = true;
        }

        public UserDTO(int id,string fullName, string username,string createdAt, string email, string phoneNumber)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Username = username;
            CreatedAt = createdAt;
            PhoneNumber = phoneNumber;
            IsActive = true;
        }

        public static UserDTO FromUser(User user)
        {
            return new UserDTO(user.Id,user.FullName, user.Username, user.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"), user.Email, user.PhoneNumber);
        }

        public User ToUser()
        {
            return new User(FullName, Username, Email,string.Empty ,PhoneNumber);
        }



    }
}
