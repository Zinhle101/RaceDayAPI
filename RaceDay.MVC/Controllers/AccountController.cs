
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RaceDay.MVC.Models;
using RaceDay.MVC.Services;
using System.Security.Claims;
using System.Net.Http.Json;

namespace RaceDay.MVC.Controllers
{
    // Handles login, registration and logout.
    public class AccountController : Controller
    {
        private readonly ApiService _apiService;

        public AccountController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Displays the login page.
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // Sends login details to the API.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var account = new AccountRequest
            {
                UserEmail = model.UserEmail,
                Password = model.Password
            };

            var response = await _apiService.PostAsync(
                "api/Accounts/login", account);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(
                    "", "Invalid email address or password.");

                return View(model);
            }

            // Read the login information returned by the API.
            var loginResult =
                await response.Content.ReadFromJsonAsync<ApiLoginResponse>();

            if (loginResult == null || string.IsNullOrEmpty(loginResult.Token))
            {
                ModelState.AddModelError(
                    "", "Unable to complete login.");

                return View(model);
            }

            // Save the API token in the session.
            HttpContext.Session.SetString(
                "ApiToken", loginResult.Token);

            // Store the user's identity and role.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,
                    loginResult.UserID.ToString()),

                new Claim(ClaimTypes.Name,
                    loginResult.UserEmail),

                new Claim(ClaimTypes.Role,
                    loginResult.Role)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            // Sign the user into the MVC website.
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            // Send the user to the correct dashboard.
            if (loginResult.Role == "Organiser")
            {
                return RedirectToAction("Dashboard", "Organiser");
            }

            return RedirectToAction("Dashboard", "Participant");
        }

        // Displays the registration page.
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // Sends new participant details to the API.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var account = new AccountRequest
            {
                UserName = model.UserName,
                UserSurname = model.UserSurname,
                UserEmail = model.UserEmail,
                Password = model.Password,
                Role = "Participant"
            };

            var response = await _apiService.PostAsync(
                "api/Accounts/register", account);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                ModelState.AddModelError(
                    "", string.IsNullOrWhiteSpace(error)
                        ? "Registration failed."
                        : error);

                return View(model);
            }

            TempData["Success"] =
                "Your account has been created. Please log in.";

            return RedirectToAction("Login");
        }

        // Logs the user out of the MVC website.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
