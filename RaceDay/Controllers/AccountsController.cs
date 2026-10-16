
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RaceDay.API.Data;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    // Handles user registration and login.
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly RaceDayContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        // Connects the controller to the database.
        public AccountsController(RaceDayContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        // POST: api/Accounts/register
        // Registers a new participant account.
        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountRequest request)
        {
            // Checks whether the email address is already registered.
            var emailExists = await _context.Users
                .AnyAsync(u => u.UserEmail == request.UserEmail);

            if (emailExists)
            {
                return BadRequest("This email address is already registered.");
            }

            // Creates a new user account.
            var user = new User
            {
                UserName = request.UserName,
                UserSurname = request.UserSurname,
                UserEmail = request.UserEmail,
                Role = "Participant"
            };

            // Hashes the password before saving it.
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            // Adds the user to the database.
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Registration successful.");
        }

        // POST: api/Accounts/login
        // Checks a user's email and password.
        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountRequest request)
        {
            // Finds the user using their email address.
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserEmail == request.UserEmail);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Verifies the password against the stored hash.
            var result = _passwordHasher.VerifyHashedPassword(
                user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Returns basic account information after successful login.
            return Ok(new
            {
                user.UserID,
                user.UserName,
                user.UserSurname,
                user.UserEmail,
                user.Role
            });
        }
    }
}
