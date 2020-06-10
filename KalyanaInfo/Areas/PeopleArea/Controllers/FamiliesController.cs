using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KalyanaInfo.Models;

namespace KalyanaInfo.Areas.PeopleArea.Controllers
{
    [Area("PeopleArea")]
    public class FamiliesController : Controller
    {
        private readonly kalyanadiaryContext _context;

        public FamiliesController(kalyanadiaryContext context)
        {
            _context = context;
        }

        // GET: PeopleArea/Families
        public async Task<IActionResult> Index()
        {
            return View(await _context.Family.ToListAsync());
        }

        // GET: PeopleArea/Families/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Family
                .FirstOrDefaultAsync(m => m.Id == id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        // GET: PeopleArea/Families/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PeopleArea/Families/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Family family)
        {
            if (ModelState.IsValid)
            {
                _context.Add(family);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(family);
        }

        // GET: PeopleArea/Families/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Family.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }
            return View(family);
        }

        // POST: PeopleArea/Families/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Family family)
        {
            if (id != family.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(family);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyExists(family.Id))
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
            return View(family);
        }

        // GET: PeopleArea/Families/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Family
                .FirstOrDefaultAsync(m => m.Id == id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        // POST: PeopleArea/Families/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var family = await _context.Family.FindAsync(id);
            _context.Family.Remove(family);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilyExists(int id)
        {
            return _context.Family.Any(e => e.Id == id);
        }
    }
}
