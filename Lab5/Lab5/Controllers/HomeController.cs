using Lab5.DataLayer;
using Lab5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Lab5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected AuthContext context;
        public HomeController(ILogger<HomeController> logger, AuthContext context)
        {
            this.context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PanouPersonal()
        {
            var login = User.FindFirst(ClaimTypes.Name)?.Value;
            var model = context.users.FirstOrDefault(u=>u.login == login);
            return View(model);
        }

        [Authorize(Roles ="Admin")]
        public IActionResult PanouPersonalAdmin()
        {
            var model = context.users.ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
