using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KalyanaInfo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace KalyanaInfo.Areas.ShopeArea.Controllers
{
    [Area("ShopeArea")]
    public class ShopesController : Controller
    {
        private readonly kalyanadiaryContext _context;
        private readonly IHostEnvironment _ENV;

        public ShopesController(kalyanadiaryContext context, IHostEnvironment env)
        {
            _context = context;
            _ENV = env;
        }

        // GET: ShopeArea/Shopes
        public async Task<IActionResult> Index()
        {
            var kalyanadiaryContext = _context.Shope.Include(s => s.ShopeTypeNavigation).Include(s => s.User);
            return View(await kalyanadiaryContext.ToListAsync());
        }

        // GET: ShopeArea/Shopes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shope = await _context.Shope
                .Include(s => s.ShopeTypeNavigation)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shope == null)
            {
                return NotFound();
            }

            return View(shope);
        }

        // GET: ShopeArea/Shopes/Create
        public IActionResult Create()
        {
            ViewData["ShopeType"] = new SelectList(_context.ShopeTypes, "Id", "ShopeName");
            ViewData["UserId"] = new SelectList(_context.Person, "Id", "About");
            return View();
        }

        // POST: ShopeArea/Shopes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ShortDescription,LongDescription,ShopeType,Address,UserId")] Shope shope, List<IFormFile> images)
        {
            string ImagesName = "";
            if (images.Count > 0)
            {

                foreach (var image in images)
                {
                    string DirPath = _ENV.ContentRootPath + "\\wwwroot\\Images\\ShopeImages\\";
                    string FileEx = Path.GetExtension(image.FileName);
                    string fn = Guid.NewGuid() + FileEx;
                    string FinalFileName = DirPath + fn;
                    ImagesName += fn + ",";


                    FileStream stream = new FileStream(FinalFileName, FileMode.Create);
                    await image.CopyToAsync(stream);
                }
            }
            else
            {
                ImagesName = "NoImages";

            }


            if (ModelState.IsValid)
            {
                if (!ImagesName.Contains("NoImages"))
                {
                    ImagesName = ImagesName.Remove(ImagesName.LastIndexOf(","));
                }
                shope.Images = ImagesName;
                _context.Add(shope);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopeType"] = new SelectList(_context.ShopeTypes, "Id", "ShopeName", shope.ShopeType);
            ViewData["UserId"] = new SelectList(_context.Person, "Id", "About", shope.UserId);
            return View(shope);
        }

        // GET: ShopeArea/Shopes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shope = await _context.Shope.FindAsync(id);
            if (shope == null)
            {
                return NotFound();
            }
            ViewData["ShopeType"] = new SelectList(_context.ShopeTypes, "Id", "ShopeName", shope.ShopeType);
            ViewData["UserId"] = new SelectList(_context.Person, "Id", "About", shope.UserId);
            return View(shope);
        }

        // POST: ShopeArea/Shopes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortDescription,LongDescription,Images,ShopeType,Address,UserId")] Shope shope,List<IFormFile> images)
        {
            string ImagesName = "";
            if(images.Count>0)
            {
                foreach (var image in images)
                {
                    string DirPath = _ENV.ContentRootPath + "\\wwwroot\\Images\\ShopeImages\\";
                    string FileEx = Path.GetExtension(image.FileName);
                    string fn = Guid.NewGuid() + FileEx;
                    string FinalFileName = DirPath + fn;
                    ImagesName += fn + ",";


                    FileStream stream = new FileStream(FinalFileName, FileMode.Create);
                    await image.CopyToAsync(stream);
                }
                ImagesName = ImagesName.Remove(ImagesName.LastIndexOf(","));
                shope.Images = ImagesName;
            }



            if (id != shope.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shope);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopeExists(shope.Id))
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
            ViewData["ShopeType"] = new SelectList(_context.ShopeTypes, "Id", "ShopeName", shope.ShopeType);
            ViewData["UserId"] = new SelectList(_context.Person, "Id", "About", shope.UserId);
            return View(shope);
        }

        // GET: ShopeArea/Shopes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shope = await _context.Shope
                .Include(s => s.ShopeTypeNavigation)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shope == null)
            {
                return NotFound();
            }

            return View(shope);
        }

        // POST: ShopeArea/Shopes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shope = await _context.Shope.FindAsync(id);
            _context.Shope.Remove(shope);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopeExists(int id)
        {
            return _context.Shope.Any(e => e.Id == id);
        }
        public async Task<IActionResult> MyShopes(int id)
        {
            if (HttpContext.Session.GetInt32("id") == id)
            {

                return View(await _context.Shope.Where(posts => posts.UserId == id).Include(p => p.User).ToListAsync());
            }
            return View();

        }
        public string DeleteShope(int? id)
        {
            try
            {
                var shop = _context.Shope.Find(id);
                Shope p = shop;
                if (p != null)
                {
                    _context.Remove(p);
                    _context.SaveChanges();
                    return "1";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "0";
        }
      
    }
}
