using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RaceDay.API.Data;
using RaceDay.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RaceDay.API.Controllers
{
    // Handles user registration and login.
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly RaceDayContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        // Connects the controller to the database.
        // Connects the controller to the database and application settings.
        public AccountsController(
            RaceDayContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
        // Checks the user's details and generates an authentication token.
        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountRequest request)
        {
            // Finds the user using their email address.
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserEmail == request.UserEmail);

            // Checks whether the account exists.
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Verifies the submitted password against the stored hash.
            var result = _passwordHasher.VerifyHashedPassword(
                user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Creates claims containing the user's identity and role.
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.UserEmail),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // Reads the JWT signing key from configuration.
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                   _configuration["Jwt:Key"]!));

            // Creates the signing credentials for the token.
            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            // Creates a token that expires after one hour.
            var token = new JwtSecurityToken(
                issuer: "RaceDay.API",
                audience: "RaceDay.Client",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            // Converts the token into a string for the client.
            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            // Returns the token and basic account information.
            return Ok(new
            {
                token = tokenString,
                user.UserID,
                user.UserName,
                user.UserSurname,
                user.UserEmail,
                user.Role
            });
        }

    }
}
