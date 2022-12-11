using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThirstBurst.Data;
using ThirstBurst.Models;

namespace ThirstBurst.Controllers
{
    public class DrinksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DrinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Drinks
        public async Task<IActionResult> Index()
        {
            ViewData["VariantsOfDrink"]= await _context.VariantsOfDrink.ToListAsync();
            ViewData["Variantss"] = await _context.Variants.ToListAsync();
            var applicationDbContext = _context.Drink.Include(b => b.Drink_Company);
            return View(await _context.Drink.ToListAsync());
        }

        public async Task<IActionResult> DrinkList()
        {
            return View(await _context.Drink.ToListAsync());
        }

        public IActionResult SearchDrink()
        {
            return View();
        }
        public async Task<IActionResult> Result(string Name)
        {
            return View("Index", await _context.Drink.Where(a => a.Name.Contains(Name)).ToListAsync());
        }


        // GET: Drinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drink
                .Include(b => b.Drink_Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // GET: Drinks/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "CompanyName");
            ViewData["VariantId"] = new SelectList(_context.Variants, "Id", "Variant_Name");
            return View();
        }

        // POST: Drinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Image_url,CompanyId")] Drink drink,List<int> Variantss)
        {
            if (ModelState.IsValid)
            {
                _context.Drink.Add(drink);
                await _context.SaveChangesAsync();
                List<VariantsOfDrink> variantsofdrink = new List<VariantsOfDrink>();
                foreach (int variant in Variantss)
                {
                   variantsofdrink.Add(new VariantsOfDrink { VariantId = variant, DrinkId = drink.Id });
                }
                _context.VariantsOfDrink.AddRange(variantsofdrink);
                _context.SaveChanges();
               return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id", drink.CompanyId);
            return View(drink);
        }

        // GET: Drinks/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drink.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            IList<VariantsOfDrink> variantsofdrinks = await _context.VariantsOfDrink.Where<VariantsOfDrink>(a=>a.DrinkId == drink.Id).ToListAsync();
            IList<int> listVariantss = new List<int>();
            foreach(VariantsOfDrink variantsofdrink in variantsofdrinks)
            {
                listVariantss.Add(variantsofdrink.VariantId);
            }
            // var variantss = await _context.Variants.Where(a=>a.Id.Equals(listVariantss)).ToListAsync();

            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "CompanyName",drink.CompanyId);
            ViewData["VariantId"] = new MultiSelectList(_context.Variants, "Id", "Variant_Name", listVariantss.ToArray());
            return View(drink);
        }

        // POST: Drinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Image_url,CompanyId")] Drink drink)
        {
            if (id != drink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkExists(drink.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id", drink.CompanyId);
            return View(drink);
        }

        // GET: Drinks/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drink
                .Include(b => b.Drink_Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // POST: Drinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drink = await _context.Drink.FindAsync(id);
            _context.Drink.Remove(drink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkExists(int id)
        {
            return _context.Drink.Any(e => e.Id == id);
        }
    }
}
