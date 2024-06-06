using ms_users.Models;

namespace ms_users.Interface
{
    public interface IAuthenticationService
    {
        public ResponseLogin Login(Login login);
    }
}
