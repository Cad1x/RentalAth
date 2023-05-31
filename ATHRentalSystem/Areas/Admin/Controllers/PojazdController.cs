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

namespace ATHRentalSystem.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    
    public class PojazdController : Controller
    {
        private readonly ApplicationDbContext db;

        public PojazdController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Admin/Pojazd
        public async Task<IActionResult> Index()
        {
              return db.pojazd != null ? 
                          View(await db.pojazd.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.pojazd'  is null.");
        }

        // GET: Admin/Pojazd/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.pojazd == null)
            {
                return NotFound();
            }

            var pojazdViewModel = await db.pojazd
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (pojazdViewModel == null)
            {
                return NotFound();
            }

            return View(pojazdViewModel);
        }

        // GET: Admin/Pojazd/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Pojazd/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId")] PojazdViewModel pojazdViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Add(pojazdViewModel);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pojazdViewModel);
        }

        // GET: Admin/Pojazd/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.pojazd == null)
            {
                return NotFound();
            }

            var pojazdViewModel = await db.pojazd.FindAsync(id);
            if (pojazdViewModel == null)
            {
                return NotFound();
            }
            return View(pojazdViewModel);
        }

        // POST: Admin/Pojazd/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId")] PojazdViewModel pojazdViewModel)
        {
            if (id != pojazdViewModel.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(pojazdViewModel);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PojazdViewModelExists(pojazdViewModel.VehicleId))
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
            return View(pojazdViewModel);
        }

        // GET: Admin/Pojazd/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.pojazd == null)
            {
                return NotFound();
            }

            var pojazdViewModel = await db.pojazd
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (pojazdViewModel == null)
            {
                return NotFound();
            }

            return View(pojazdViewModel);
        }

        // POST: Admin/Pojazd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.pojazd == null)
            {
                return Problem("Entity set 'ApplicationDbContext.pojazd'  is null.");
            }
            var pojazdViewModel = await db.pojazd.FindAsync(id);
            if (pojazdViewModel != null)
            {
                db.pojazd.Remove(pojazdViewModel);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PojazdViewModelExists(int id)
        {
          return (db.pojazd?.Any(e => e.VehicleId == id)).GetValueOrDefault();
        }
    }
}
