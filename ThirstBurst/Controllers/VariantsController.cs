using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThirstBurst.Data;
using ThirstBurst.Models;

namespace ThirstBurst.Controllers
{
    public class VariantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VariantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Variants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Variants.ToListAsync());
        }

        // GET: Variants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variants = await _context.Variants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (variants == null)
            {
                return NotFound();
            }

            return View(variants);
        }

        // GET: Variants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Variants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Variant_Name,V_Image,Release_Date")] Variants variants)
        {
            if (ModelState.IsValid)
            {
                _context.Add(variants);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(variants);
        }

        // GET: Variants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variants = await _context.Variants.FindAsync(id);
            if (variants == null)
            {
                return NotFound();
            }
            return View(variants);
        }

        // POST: Variants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Variant_Name,V_Image,Release_Date")] Variants variants)
        {
            if (id != variants.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variants);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariantsExists(variants.Id))
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
            return View(variants);
        }

        // GET: Variants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variants = await _context.Variants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (variants == null)
            {
                return NotFound();
            }
            List<VariantsOfDrink> drinkVariantss = await _context.VariantsOfDrink.Where<VariantsOfDrink>(a => a.VariantId == variants.Id).ToListAsync();
            List<Drink> drinks = await _context.Drink.ToListAsync();


            ViewData["drinks"] = drinks;
            ViewData["drinkVariantss"] = drinkVariantss;

            return View(variants);
        }

        // POST: Variants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variants = await _context.Variants.FindAsync(id);
            _context.Variants.Remove(variants);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VariantsExists(int id)
        {
            return _context.Variants.Any(e => e.Id == id);
        }
    }
}
