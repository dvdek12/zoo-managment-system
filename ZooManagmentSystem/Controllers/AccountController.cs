using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Client;
using ZooManagmentSystem.Models.Employee;
using ZooManagmentSystem.ViewModels;

namespace ZooManagmentSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: /Account/Register
        public IActionResult Register() => View();

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (model.IsEmployee)
                {
                    var employee = new EmployeeModel { ApplicationUserId = user.Id };
                    _context.Employees.Add(employee);
                    await _userManager.AddToRoleAsync(user, "Pracownik");
                }
                else
                {
                    var client = new ClientModel { ApplicationUserId = user.Id };
                    _context.Clients.Add(client);
                    await _userManager.AddToRoleAsync(user, "Klient");
                }

                await _context.SaveChangesAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // GET: /Account/Login
        public IActionResult Login() => View();

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Nieprawidłowy email lub hasło");
            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
