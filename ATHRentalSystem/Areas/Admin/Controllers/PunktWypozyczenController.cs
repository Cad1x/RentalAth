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

namespace ATHRentalSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    [Area("Admin")]
    public class PunktWypozyczenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PunktWypozyczenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PunktWypozyczen
        public async Task<IActionResult> Index()
        {
              return _context.PunktWypozyczenViewModel != null ? 
                          View(await _context.PunktWypozyczenViewModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PunktWypozyczenViewModel'  is null.");
        }

        // GET: Admin/PunktWypozyczen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PunktWypozyczenViewModel == null)
            {
                return NotFound();
            }

            var punktWypozyczenViewModel = await _context.PunktWypozyczenViewModel
                .FirstOrDefaultAsync(m => m.PunktId == id);
            if (punktWypozyczenViewModel == null)
            {
                return NotFound();
            }

            return View(punktWypozyczenViewModel);
        }

        // GET: Admin/PunktWypozyczen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PunktWypozyczen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PunktId,NazwaWypozyczalnia,Miasto,Ulica,Numer")] PunktWypozyczenViewModel punktWypozyczenViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(punktWypozyczenViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(punktWypozyczenViewModel);
        }

        // GET: Admin/PunktWypozyczen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PunktWypozyczenViewModel == null)
            {
                return NotFound();
            }

            var punktWypozyczenViewModel = await _context.PunktWypozyczenViewModel.FindAsync(id);
            if (punktWypozyczenViewModel == null)
            {
                return NotFound();
            }
            return View(punktWypozyczenViewModel);
        }

        // POST: Admin/PunktWypozyczen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PunktId,NazwaWypozyczalnia,Miasto,Ulica,Numer")] PunktWypozyczenViewModel punktWypozyczenViewModel)
        {
            if (id != punktWypozyczenViewModel.PunktId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(punktWypozyczenViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PunktWypozyczenViewModelExists(punktWypozyczenViewModel.PunktId))
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
            return View(punktWypozyczenViewModel);
        }

        // GET: Admin/PunktWypozyczen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PunktWypozyczenViewModel == null)
            {
                return NotFound();
            }

            var punktWypozyczenViewModel = await _context.PunktWypozyczenViewModel
                .FirstOrDefaultAsync(m => m.PunktId == id);
            if (punktWypozyczenViewModel == null)
            {
                return NotFound();
            }

            return View(punktWypozyczenViewModel);
        }

        // POST: Admin/PunktWypozyczen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PunktWypozyczenViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PunktWypozyczenViewModel'  is null.");
            }
            var punktWypozyczenViewModel = await _context.PunktWypozyczenViewModel.FindAsync(id);
            if (punktWypozyczenViewModel != null)
            {
                _context.PunktWypozyczenViewModel.Remove(punktWypozyczenViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PunktWypozyczenViewModelExists(int id)
        {
          return (_context.PunktWypozyczenViewModel?.Any(e => e.PunktId == id)).GetValueOrDefault();
        }
    }
}
