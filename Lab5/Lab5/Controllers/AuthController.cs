using Lab5.DataLayer;
using Lab5.Migrations;
using Lab5.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lab5.Controllers
{
    public class AuthController : Controller
    {
        protected AuthContext context;
        public AuthController(AuthContext context) { 
            this.context = context;
        }

        public IActionResult Index()
        {
            //Pentru a introduce un admin
            //if(context.users.Where(u => u.role == 1).FirstOrDefault() == null)
            //{
            //context.users.Add(new User { login = "1", password = "1", role = 1 });
            //}
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StoreRegister(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (context.users.FirstOrDefault(u => u.login == model.login) != null)
                {
                    ModelState.AddModelError("login", "Un utilizator cu acest login exista deja.");
                    return View("Register",model);
                }
                User user = new User{login = model.login,password = model.password };
                context.users.Add(user);
                context.SaveChanges();
                ModelState.Clear();
                return View("Login");
            }
            return View("Register",model);
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StoreLogin(User model)
        {
            if (ModelState.IsValid)
            {
                var dbUser = context.users.FirstOrDefault(u => u.login == model.login && u.password == model.password);
                if (dbUser == null)
                {
                    ModelState.AddModelError("login", "Nu a fost gasit utilizatorul");
                    return View(model);
                }
                else {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, dbUser.login),
                        new Claim(ClaimTypes.Role, dbUser.role == 0 ? "User" : "Admin"),
                    };
                    ModelState.Clear();
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Login",model);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Auth");
        }
    }
}
