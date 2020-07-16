using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KalyanaInfo.Models;

namespace KalyanaInfo.Areas.ShopeArea.Controllers
{
    [Area("ShopeArea")]
    public class ShopeTypesController : Controller
    {
        private readonly kalyanadiaryContext _context;

        public ShopeTypesController(kalyanadiaryContext context)
        {
            _context = context;
        }

        // GET: ShopeArea/ShopeTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShopeTypes.ToListAsync());
        }

        // GET: ShopeArea/ShopeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopeTypes = await _context.ShopeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopeTypes == null)
            {
                return NotFound();
            }

            return View(shopeTypes);
        }

        // GET: ShopeArea/ShopeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShopeArea/ShopeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShopeName")] ShopeTypes shopeTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shopeTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shopeTypes);
        }

        // GET: ShopeArea/ShopeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopeTypes = await _context.ShopeTypes.FindAsync(id);
            if (shopeTypes == null)
            {
                return NotFound();
            }
            return View(shopeTypes);
        }

        // POST: ShopeArea/ShopeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShopeName")] ShopeTypes shopeTypes)
        {
            if (id != shopeTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shopeTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopeTypesExists(shopeTypes.Id))
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
            return View(shopeTypes);
        }

        // GET: ShopeArea/ShopeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopeTypes = await _context.ShopeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopeTypes == null)
            {
                return NotFound();
            }

            return View(shopeTypes);
        }

        // POST: ShopeArea/ShopeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shopeTypes = await _context.ShopeTypes.FindAsync(id);
            _context.ShopeTypes.Remove(shopeTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopeTypesExists(int id)
        {
            return _context.ShopeTypes.Any(e => e.Id == id);
        }
    }
}
