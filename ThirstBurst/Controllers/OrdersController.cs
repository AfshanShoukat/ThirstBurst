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
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.Drink_ordered).Include(o => o.User_);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Drink_ordered)
                .Include(o => o.User_)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
       // public IActionResult Create()
        //{
          //  ViewData["Drink_Id"] = new SelectList(_context.Drink, "Id", "Image_url");
            //ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Id");
           // return View();
        //}

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,Quantity,Status,LastUpdated,Drink_Id,User_Id")] Orders orders)
        {
            orders.Status = OrderState.InCart;
            orders.LastUpdated = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["Drink_Id"] = new SelectList(_context.Drink, "Id", "Name", orders.Drink_Id);
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "UserName", orders.User_Id);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Quantity,Status,LastUpdated,Drink_Id,User_Id")] Orders orders)
        {
            if (id != orders.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderId))
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
            ViewData["Drink_Id"] = new SelectList(_context.Drink, "Id", "Id", orders.Drink_Id);
            ViewData["User_Id"] = new SelectList(_context.Users, "Id", "Id", orders.User_Id);
            return View(orders);
        }

        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Drink_ordered)
                .Include(o => o.User_)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            order.Status = OrderState.OrderPlaced;
            order.LastUpdated = DateTime.Now;
            _context.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Drink_ordered)
                .Include(o => o.User_)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
