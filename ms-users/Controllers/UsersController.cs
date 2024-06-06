using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ms_users.Infrastructure.Data;
using ms_users.Interface;
using ms_users.ModelDTO;
using ms_users.Models;

namespace ms_users.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IServiceCryptography _serviceCryptography;

        public UsersController(AppDbContext context, IServiceCryptography serviceCryptography)
        {
            _context = context;
            _serviceCryptography = serviceCryptography;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetPeople()
        {
            List<User> users = await _context.Users.ToListAsync();
            List<UserDTO> usersDTO = new List<UserDTO>();

            foreach (User user in users)
            {
                usersDTO.Add(UserDTO.FromUser(user));
            }

            return usersDTO;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return UserDTO.FromUser(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            //Validate if the user or email already exists
            if (_context.Users.Any(e => e.Username == user.Username) || _context.Users.Any(e => e.Email == user.Email))
            {
                return BadRequest("El usuario o correo electrónico ya existe.");
            }

            _context.Users.Add(user);

            //Encrypt password
            user.Password = _serviceCryptography.GenerateHash(user.Password);
            user.CreatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }


    }
}
