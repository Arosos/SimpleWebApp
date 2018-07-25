using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.Areas.Identity.Data;
using SimpleWebApp.Models;

namespace SimpleWebApp.Controllers
{
    public class PostsController : Controller
    {
        private SimpleWebAppContext _conext;
        private readonly UserManager<SimpleWebAppUser> _userManager;

        public PostsController(SimpleWebAppContext context, UserManager<SimpleWebAppUser> userManager)
        {
            _conext = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include:"Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                SimpleWebAppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                post.Author = user;
                post.AuthorID = user.Id;
                post.Timestamp = DateTime.UtcNow;
                _conext.Add(post);
                await _conext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}