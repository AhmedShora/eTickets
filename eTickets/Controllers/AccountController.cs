using eTickets.Data;
using eTickets.Data.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(AppDbContext appDbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View(login);
            var user = await _userManager.FindByEmailAsync(login.EmailAddress);
            if (user != null)
            {
                //await _signInManager.SignInAsync(user, isPersistent: false);
                var check = await _userManager.CheckPasswordAsync(user, login.Password);
                if (check)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                    TempData["Error"] = "Email Or Password is wrong";
                    return View(login);
                }
            }

            TempData["Error"] = "Email Or Password is wrong";
            return View(login);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);
            var user = await _userManager.FindByEmailAsync(register.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "Email Is already in use";
                return View(register);
            }
            var newuser = new AppUser()
            {
                FullName = register.FullName,
                Email = register.EmailAddress,
                UserName = register.EmailAddress
            };

            var newUserResponse = await _userManager.CreateAsync(newuser, register.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newuser, UserRoles.User);

            }
            return View("RegisterCompleted");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }
        public async Task<IActionResult> Users()
        {
            var users = await _appDbContext.Users.ToListAsync();
            return View(users);
        }
    }

}
