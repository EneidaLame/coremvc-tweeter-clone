using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Data;
using SocialApp.Models;

namespace SocialApp.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext Db;

        public PostController(ApplicationDbContext Context)
        {
            Db = Context;
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Posts = Db.Posts.ToList().OrderByDescending(p => p.Id);
            return View();
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPostAsync(Post NewPost)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            NewPost.ApplicationUserId = userId;
            Db.Posts.Add(NewPost);
            await Db.SaveChangesAsync();

            return RedirectToAction("Index", "Post");
        }

        [Authorize]
        public IActionResult MyPosts()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Posts = Db.Posts.Where(p => p.ApplicationUserId == userId);
            return View();
        }

        [HttpGet]
        public IActionResult SearchPost(String searchStr)
        {
            ViewBag.searchStr = searchStr;
            ViewBag.Posts = Db.Posts.Where(p => p.Content.Contains(searchStr));
            return View();
        }
    }
}
