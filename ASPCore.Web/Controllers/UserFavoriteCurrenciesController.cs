using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPCore.Data;
using ASPCore.Data.Models;
using System.Security.Claims;

namespace ASPCore.Web.Controllers
{
    public class UserFavoriteCurrenciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserFavoriteCurrenciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserFavoriteCurrencies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserFavoriteCurrencies.Include(u => u.Currency);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserFavoriteCurrencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFavoriteCurrency = await _context.UserFavoriteCurrencies
                .Include(u => u.Currency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFavoriteCurrency == null)
            {
                return NotFound();
            }

            return View(userFavoriteCurrency);
        }

        // GET: UserFavoriteCurrencies/Create
        public IActionResult Create()
        {
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Code");
            return View();
        }

        // POST: UserFavoriteCurrencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CurrencyId")] UserFavoriteCurrency userFavoriteCurrency)
        {
            if (ModelState.IsValid)
            {
                userFavoriteCurrency.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _context.Add(userFavoriteCurrency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Code", userFavoriteCurrency.CurrencyId);
            return View(userFavoriteCurrency);
        }

        // GET: UserFavoriteCurrencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFavoriteCurrency = await _context.UserFavoriteCurrencies.FindAsync(id);
            if (userFavoriteCurrency == null)
            {
                return NotFound();
            }
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Code", userFavoriteCurrency.CurrencyId);
            return View(userFavoriteCurrency);
        }

        // POST: UserFavoriteCurrencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CurrencyId")] UserFavoriteCurrency userFavoriteCurrency)
        {
            if (id != userFavoriteCurrency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userFavoriteCurrency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFavoriteCurrencyExists(userFavoriteCurrency.Id))
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
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "Id", "Code", userFavoriteCurrency.CurrencyId);
            return View(userFavoriteCurrency);
        }

        // GET: UserFavoriteCurrencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFavoriteCurrency = await _context.UserFavoriteCurrencies
                .Include(u => u.Currency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFavoriteCurrency == null)
            {
                return NotFound();
            }

            return View(userFavoriteCurrency);
        }

        // POST: UserFavoriteCurrencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userFavoriteCurrency = await _context.UserFavoriteCurrencies.FindAsync(id);
            _context.UserFavoriteCurrencies.Remove(userFavoriteCurrency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFavoriteCurrencyExists(int id)
        {
            return _context.UserFavoriteCurrencies.Any(e => e.Id == id);
        }
    }
}
