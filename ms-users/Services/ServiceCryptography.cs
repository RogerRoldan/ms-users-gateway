using System.Security.Cryptography;
using System.Text;
using ms_users.Interface;

namespace ms_users.Services
{
    public class ServiceCryptography : IServiceCryptography
    {
        public string GenerateHash(string password)
        {
            // Convertir la cadena de entrada a bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);

            // Calcular el hash SHA-256
            byte[] hashBytes = SHA256.HashData(inputBytes);

            // Convertir los bytes del hash a una cadena hexadecimal
            StringBuilder sb = new();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
