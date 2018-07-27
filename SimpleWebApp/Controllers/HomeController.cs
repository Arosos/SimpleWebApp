using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.Areas.Identity.Data;
using SimpleWebApp.Models;
using SimpleWebApp.ViewModels;

namespace SimpleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private SimpleWebAppContext _context;
        private readonly UserManager<SimpleWebAppUser> _userManager;

        public HomeController(SimpleWebAppContext context, UserManager<SimpleWebAppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var posts = from p in _context.Posts
                            orderby p.Timestamp descending
                            select p;
                HomeViewModel homeViewModel = new HomeViewModel()
                {
                    Posts = posts.ToList()
                };
                return View(homeViewModel);
            }
            return View();
        }
        
        public IActionResult About()
        {
            return View();
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
