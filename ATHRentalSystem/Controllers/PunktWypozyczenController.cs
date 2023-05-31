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
    public class PunktWypozyczenController : Controller
    {

        private readonly IMapper _mapper;

        public readonly ApplicationDbContext db;

        public List<PunktWypozyczenViewModel> PunktWypozyczen { get; set; }

        public PunktWypozyczenController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            _mapper = mapper;
        }

    
     
        // GET: PunktWypozyczen
        public async Task<IActionResult> Index()
        {

            List<PunktWypozyczenViewModel> list = new();
            foreach (var item in db.punktWypożyczeń)
            {
                list.Add(_mapper.Map<PunktWypozyczenViewModel>(item));
            }
            return View(list);
              //return db.punktWypożyczeń != null ? 
              //            View(await db.punktWypożyczeń.ToListAsync()) :
              //            Problem("Entity set 'ApplicationDbContext.punktWypożyczeń'  is null.");
        }

        // GET: PunktWypozyczen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.punktWypożyczeń == null)
            {
                return NotFound();
            }

            var punktWypozyczenViewModel = await db.punktWypożyczeń
                .FirstOrDefaultAsync(m => m.PunktId == id);
            if (punktWypozyczenViewModel == null)
            {
                return NotFound();
            }

            return View(punktWypozyczenViewModel);
        }

        // GET: PunktWypozyczen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PunktWypozyczen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PunktId,NazwaWypozyczalnia,Miasto,Ulica,Numer,IloscDostepnych")] PunktWypozyczenViewModel punktWypozyczenViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Add(punktWypozyczenViewModel);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(punktWypozyczenViewModel);
        }

        // GET: PunktWypozyczen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.punktWypożyczeń == null)
            {
                return NotFound();
            }

            var punktWypozyczenViewModel = await db.punktWypożyczeń.FindAsync(id);
            if (punktWypozyczenViewModel == null)
            {
                return NotFound();
            }
            return View(punktWypozyczenViewModel);
        }

        // POST: PunktWypozyczen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PunktId,NazwaWypozyczalnia,Miasto,Ulica,Numer,IloscDostepnych")] PunktWypozyczenViewModel punktWypozyczenViewModel)
        {
            if (id != punktWypozyczenViewModel.PunktId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(punktWypozyczenViewModel);
                    await db.SaveChangesAsync();
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

        // GET: PunktWypozyczen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.punktWypożyczeń == null)
            {
                return NotFound();
            }

            var punktWypozyczenViewModel = await db.punktWypożyczeń
                .FirstOrDefaultAsync(m => m.PunktId == id);
            if (punktWypozyczenViewModel == null)
            {
                return NotFound();
            }

            return View(punktWypozyczenViewModel);
        }

        // POST: PunktWypozyczen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.punktWypożyczeń == null)
            {
                return Problem("Entity set 'ApplicationDbContext.punktWypożyczeń'  is null.");
            }
            var punktWypozyczenViewModel = await db.punktWypożyczeń.FindAsync(id);
            if (punktWypozyczenViewModel != null)
            {
                db.punktWypożyczeń.Remove(punktWypozyczenViewModel);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PunktWypozyczenViewModelExists(int id)
        {
          return (db.punktWypożyczeń?.Any(e => e.PunktId == id)).GetValueOrDefault();
        }
    }
}
