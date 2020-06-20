using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KalyanaInfo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace KalyanaInfo.Areas.PostArea.Controllers
{
    [Area("PostArea")]
    public class PostsController : Controller
    {
        private readonly kalyanadiaryContext _context;
        private readonly IHostEnvironment _ENV;
        public PostsController(kalyanadiaryContext context,IHostEnvironment env)
        {
            _context = context;
            _ENV = env;
        }

        // GET: PostArea/Posts
        public async Task<IActionResult> Index()
        {
            var kalyanadiaryContext = _context.Post.Include(p => p.User);
            return View(await kalyanadiaryContext.ToListAsync());
        }

        // GET: PostArea/Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: PostArea/Posts/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: PostArea/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Title")] Post post,IFormFile image, IFormFile video)
        {
            if(image!=null)
            {
                string DirPath = _ENV.ContentRootPath + "\\wwwroot\\Images\\PostImages\\";
                string FileEx = Path.GetExtension(image.FileName);
                string fn = Guid.NewGuid() + FileEx;
                string FinalFileName = DirPath + fn;
                post.Images = fn;


                FileStream stream = new FileStream(FinalFileName, FileMode.Create);
                await image.CopyToAsync(stream);
            }
            else
            {
                post.Images = "noimage";
            }
            if(video!=null)
            {
                string DirPath = _ENV.ContentRootPath + "\\wwwroot\\Videos\\PostVideos\\";
                string FileEx = Path.GetExtension(video.FileName);
                string VideoName = Guid.NewGuid() + FileEx;
                string FinalFileName = DirPath + VideoName;
                post.Video = VideoName;
                 FileStream stream = new FileStream(FinalFileName, FileMode.Create);
                await video.CopyToAsync(stream);


            }
            else
            {
                post.Video = "novideo";
            }
            if(post.Title==null)
            {
                post.Title = "notitle";
            }

            post.CreatedDate = DateTime.Now;
            post.ModifiedDate = DateTime.Now;
            post.PostType = "public";
            if (post.Description == null)
            {
                ModelState.AddModelError("", "Description cant be null");
                return View(post);
            }
            post.UserId = (HttpContext.Session.GetInt32("id"))??default(int);
            if (ModelState.IsValid)
            {

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(post);
        }

        // GET: PostArea/Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Person, "Id", "About", post.UserId);
            return View(post);
        }

        // POST: PostArea/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Images,CreatedDate,ModifiedDate,PostType,Video,UserId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Person, "Id", "About", post.UserId);
            return View(post);
        }

        // GET: PostArea/Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: PostArea/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
