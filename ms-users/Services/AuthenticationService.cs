using ms_users.Infrastructure.Data;
using ms_users.Interface;
using ms_users.Models;

namespace ms_users.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IServiceCryptography _serviceCryptography;
        private AppDbContext _context;

        public AuthenticationService(ITokenService tokenService, AppDbContext context, IServiceCryptography serviceCryptography)
        {
            _tokenService = tokenService;
            _context = context;
            _serviceCryptography = serviceCryptography;
        }

        public ResponseLogin Login(Login login)
        {
            try
            {
                var user =  _context.Users.FirstOrDefault(u => u.Username == login.Username) ?? throw new KeyNotFoundException("Usuario {login.Username} no encontrado.");

                if (!user.IsActive)
                {
                    throw new Exception("Usuario inactivo.");
                }

                if (!_serviceCryptography.GenerateHash(login.Password).Equals(user.Password))
                {
                    throw new Exception("Contraseña incorrecta.");
                }

                return new ResponseLogin
                {
                    User = user,
                    AccessToken = _tokenService.GenerateToken(user)
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autenticar usuario. " + ex.Message);
            }
        }
    }
}
