using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleWebApp.Areas.Identity.Data;
using SimpleWebApp.Models;

namespace SimpleWebApp.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private SimpleWebAppContext _context;
        private readonly UserManager<SimpleWebAppUser> _userManager;

        public PostsController(SimpleWebAppContext context, UserManager<SimpleWebAppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            SimpleWebAppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userPosts = from p in _context.Posts
                            where p.AuthorID == user.Id
                            orderby p.Timestamp descending
                            select p;
            return View(userPosts.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include:"Content")] Post post)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {
                post.AuthorID = user.Id;
                post.Timestamp = DateTime.UtcNow;
                _context.Add(post);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.ID == id);

            if (post == null)
                return NotFound();

            if (id != post.ID || post.AuthorID != user.Id)
                return NotFound();

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind(include: "ID,Content")] Post post)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var dbPost = await _context.Posts.SingleOrDefaultAsync(p => p.ID == post.ID);
            if (id != post.ID || dbPost.AuthorID != user.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    dbPost.Content = post.Content;
                    dbPost.Timestamp = DateTime.UtcNow;
                    _context.Update(dbPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.ID == id);

            if (post == null)
                return NotFound();

            if (id != post.ID || post.AuthorID != user.Id)
                return NotFound();

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.ID == id);

            if (post.AuthorID != user.Id)
                return NotFound();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(p => p.ID == id);
        }
    }
}
