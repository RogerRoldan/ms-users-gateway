using ms_users.Models;

namespace ms_users.Interface
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
