using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ATHRentalSystem.Data;
using ATHRentalSystem.Models;
using AutoMapper;

namespace ATHRentalSystem.Controllers
{
    public class RezerwacjeController : Controller
    {
        private readonly IMapper _mapper;

        public readonly ApplicationDbContext db;

        public List<RezerwacjeController> rezerwacjes { get; set; }

        public RezerwacjeController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            _mapper = mapper;
        }

        // GET: Rezerwacje
        public async Task<IActionResult> Index()
        {
            List<RezerwacjeViewModel> vehicles = new();

            foreach (var item in db.rezerwacje)
            {
                vehicles.Add(_mapper.Map<RezerwacjeViewModel>(item));
            }

            return View(vehicles);
        }



        // GET: Rezerwacje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.rezerwacje == null)
            {
                return NotFound();
            }

            var rezerwacjeViewModel = await db.rezerwacje
                .FirstOrDefaultAsync(m => m.RezerwacjaId == id);
            if (rezerwacjeViewModel == null)
            {
                return NotFound();
            }

            return View(rezerwacjeViewModel);
        }

        // GET: Rezerwacje/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rezerwacje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RezerwacjaId,ImieRezerwanta,NazwiskoRezerwanta,RezerwacjaOd,RezerwacjaDo")] RezerwacjeViewModel rezerwacjeViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Add(rezerwacjeViewModel);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rezerwacjeViewModel);
        }

        // GET: Rezerwacje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.rezerwacje == null)
            {
                return NotFound();
            }

            var rezerwacjeViewModel = await db.rezerwacje.FindAsync(id);
            if (rezerwacjeViewModel == null)
            {
                return NotFound();
            }
            return View(rezerwacjeViewModel);
        }

        // POST: Rezerwacje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RezerwacjaId,ImieRezerwanta,NazwiskoRezerwanta,RezerwacjaOd,RezerwacjaDo")] RezerwacjeViewModel rezerwacjeViewModel)
        {
            if (id != rezerwacjeViewModel.RezerwacjaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(rezerwacjeViewModel);
                    await db.SaveChangesAsync();
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

        // GET: Rezerwacje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.rezerwacje == null)
            {
                return NotFound();
            }

            var rezerwacjeViewModel = await db.rezerwacje
                .FirstOrDefaultAsync(m => m.RezerwacjaId == id);
            if (rezerwacjeViewModel == null)
            {
                return NotFound();
            }

            return View(rezerwacjeViewModel);
        }

        // POST: Rezerwacje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.rezerwacje == null)
            {
                return Problem("Entity set 'ApplicationDbContext.rezerwacje'  is null.");
            }
            var rezerwacjeViewModel = await db.rezerwacje.FindAsync(id);
            if (rezerwacjeViewModel != null)
            {
                db.rezerwacje.Remove(rezerwacjeViewModel);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezerwacjeViewModelExists(int id)
        {
          return (db.rezerwacje?.Any(e => e.RezerwacjaId == id)).GetValueOrDefault();
        }
    }
}
