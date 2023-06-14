using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ATHRentalSystem.Data;
using ATHRentalSystem.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ATHRentalSystem.Areas.Users.Controllers
{
    [Area("Users")]

    public class RezerwacjeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezerwacjeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users/Rezerwacje
        public async Task<IActionResult> Index()
        {
              return _context.rezerwacje != null ? 
                          View(await _context.rezerwacje.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.rezerwacje'  is null.");
        }

        // GET: Users/Rezerwacje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.rezerwacje == null)
            {
                return NotFound();
            }

            var rezerwacjeViewModel = await _context.rezerwacje
                .FirstOrDefaultAsync(m => m.RezerwacjaId == id);
            if (rezerwacjeViewModel == null)
            {
                return NotFound();
            }

            return View(rezerwacjeViewModel);
        }

        // GET: Users/Rezerwacje/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Rezerwacje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RezerwacjaId,ImieRezerwanta,NazwiskoRezerwanta,RezerwacjaOd,RezerwacjaDo,zatwierdz")] RezerwacjeViewModel rezerwacjeViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezerwacjeViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rezerwacjeViewModel);
        }

        // GET: Users/Rezerwacje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.rezerwacje == null)
            {
                return NotFound();
            }

            var rezerwacjeViewModel = await _context.rezerwacje.FindAsync(id);
            if (rezerwacjeViewModel == null)
            {
                return NotFound();
            }
            return View(rezerwacjeViewModel);
        }

        // POST: Users/Rezerwacje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RezerwacjaId,ImieRezerwanta,NazwiskoRezerwanta,RezerwacjaOd,RezerwacjaDo,zatwierdz")] RezerwacjeViewModel rezerwacjeViewModel)
        {
            if (id != rezerwacjeViewModel.RezerwacjaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezerwacjeViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezerwacjeViewModelExists(rezerwacjeViewModel.RezerwacjaId))
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
            return View(rezerwacjeViewModel);
        }

        // GET: Users/Rezerwacje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.rezerwacje == null)
            {
                return NotFound();
            }

            var rezerwacjeViewModel = await _context.rezerwacje
                .FirstOrDefaultAsync(m => m.RezerwacjaId == id);
            if (rezerwacjeViewModel == null)
            {
                return NotFound();
            }

            return View(rezerwacjeViewModel);
        }

        // POST: Users/Rezerwacje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.rezerwacje == null)
            {
                return Problem("Entity set 'ApplicationDbContext.rezerwacje'  is null.");
            }
            var rezerwacjeViewModel = await _context.rezerwacje.FindAsync(id);
            if (rezerwacjeViewModel != null)
            {
                _context.rezerwacje.Remove(rezerwacjeViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezerwacjeViewModelExists(int id)
        {
          return (_context.rezerwacje?.Any(e => e.RezerwacjaId == id)).GetValueOrDefault();
        }
    }
}
