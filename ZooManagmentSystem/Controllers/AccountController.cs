using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.DTOs;
using ZooManagmentSystem.DTOs.Employee;
using ZooManagmentSystem.Models.Client;
using ZooManagmentSystem.Models.Employee;
using ZooManagmentSystem.ViewModels;

namespace ZooManagmentSystem.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AccountController(IConfiguration configuration, UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 AppDbContext context)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        // POST: /Account/Register
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] ClientDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var client = new ClientModel
                {
                    ApplicationUserId = user.Id,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDay = model.BirthDay,
                };

                _context.Clients.Add(client);

                await _context.SaveChangesAsync();

                await _userManager.AddToRoleAsync(user, "Client");

                return Ok(new { message = "User registered successfuly!" });
            }

            return BadRequest(result.Errors);
        }

        // POST: /Account/Employee/Register
        [Route("RegisterEmployee")]
        [HttpPost]
        public async Task<IActionResult> RegisterEmployee([FromBody] EmployeeDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var employee = new EmployeeModel
                {
                    ApplicationUserId = user.Id,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDay = model.BirthDay,
                };

                _context.Employees.Add(employee);

                await _context.SaveChangesAsync();

                await _userManager.AddToRoleAsync(user, "Employee");

                return Ok(new { message = "Employee registered successfuly!" });
            }
            return BadRequest(result.Errors);

        }

        // POST: /Account/Login
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Wrong email address or password." });
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (isPasswordValid)
            {
                // Synchronizacja roli Manager na podstawie flagi IsManagerial w bazie
                var employee = await _context.Employees
                    .Include(e => e.Role)
                    .FirstOrDefaultAsync(e => e.ApplicationUserId == user.Id);

                if (employee?.Role?.IsManagerial == true)
                {
                    // Dodaj rolę Manager jeśli jeszcze jej nie ma w Identity
                    if (!await _userManager.IsInRoleAsync(user, "Manager"))
                        await _userManager.AddToRoleAsync(user, "Manager");
                }
                else
                {
                    // Usuń rolę Manager jeśli pracownik nie jest już managerem
                    if (await _userManager.IsInRoleAsync(user, "Manager"))
                        await _userManager.RemoveFromRoleAsync(user, "Manager");
                }

                var roles = await _userManager.GetRolesAsync(user);
                var token = GenerateJwtToken(user, roles);
                return Ok(new
                {
                    token = token,
                    email = user.Email,
                    roles = roles
                });
            }

            return Unauthorized(new { message = "Wrong email address or password." });
        }

        // POST: /Account/Logout
        [Route("Logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/AccessDenied
        [Route("AccesDenied")]
        [HttpGet]
        public IActionResult AccessDenied() => View();


        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            // Dynamicznie dodajemy wszystkie role użytkownika do Claimów
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Pobieramy klucz z konfiguracji (appsettings.json)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3), // Token ważny przez 3 godziny
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
