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
    public class VehicleIDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleIDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/VehicleIDetail
        public async Task<IActionResult> Index()
        {
              return _context.VehicleDetailViewModel != null ? 
                          View(await _context.VehicleDetailViewModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.VehicleDetailViewModel'  is null.");
        }

        // GET: Admin/VehicleIDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VehicleDetailViewModel == null)
            {
                return NotFound();
            }

            var vehicleDetailViewModel = await _context.VehicleDetailViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleDetailViewModel == null)
            {
                return NotFound();
            }

            return View(vehicleDetailViewModel);
        }

        // GET: Admin/VehicleIDetail/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/VehicleIDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("color,material,Id,Name,Type,IsAvaible,picture")] VehicleDetailViewModel vehicleDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleDetailViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleDetailViewModel);
        }

        // GET: Admin/VehicleIDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleDetailViewModel == null)
            {
                return NotFound();
            }

            var vehicleDetailViewModel = await _context.VehicleDetailViewModel.FindAsync(id);
            if (vehicleDetailViewModel == null)
            {
                return NotFound();
            }
            return View(vehicleDetailViewModel);
        }

        // POST: Admin/VehicleIDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("color,material,Id,Name,Type,IsAvaible,picture")] VehicleDetailViewModel vehicleDetailViewModel)
        {
            if (id != vehicleDetailViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleDetailViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleDetailViewModelExists(vehicleDetailViewModel.Id))
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
            return View(vehicleDetailViewModel);
        }

        // GET: Admin/VehicleIDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VehicleDetailViewModel == null)
            {
                return NotFound();
            }

            var vehicleDetailViewModel = await _context.VehicleDetailViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleDetailViewModel == null)
            {
                return NotFound();
            }

            return View(vehicleDetailViewModel);
        }

        // POST: Admin/VehicleIDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleDetailViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VehicleDetailViewModel'  is null.");
            }
            var vehicleDetailViewModel = await _context.VehicleDetailViewModel.FindAsync(id);
            if (vehicleDetailViewModel != null)
            {
                _context.VehicleDetailViewModel.Remove(vehicleDetailViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleDetailViewModelExists(int id)
        {
          return (_context.VehicleDetailViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
