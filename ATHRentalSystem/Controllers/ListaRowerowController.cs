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
    public class ListaRowerowController : Controller
    {
        //


        private readonly IMapper _mapper;

        public readonly ApplicationDbContext db;

        public List<PunktWypozyczenViewModel> PunktWypozyczen { get; set; }

        public ListaRowerowController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            _mapper = mapper;
        }


        //[HttpGet]
        //public IActionResult Details(int id)
        //{
        //    var VehicleDetailViewModel = Vehicle.FirstOrDefault(r => r.Id == id);
        //    return View(VehicleDetailViewModel);
        //}

        // GET: New
        public async Task<IActionResult> Index()
        {
            List<VehicleDetailViewModel> list = new();
            foreach (var item in db.VehicleDetailViewModel)
            {
                list.Add(_mapper.Map<VehicleDetailViewModel>(item));
            }
            return View(list);

            //    return db.VehicleDetailViewModel != null ?
            //                View(await db.VehicleDetailViewModel.ToListAsync()) :
            //                Problem("Entity set 'ApplicationDbContext.VehicleDetailViewModel'  is null.");
        }





        //public void OnGet()
        //{
        //    Vehicle = db.VehicleDetailViewModel.ToList();
        //}

        //public void OnPostUpdate()
        //{
        //    Vehicle = db.VehicleDetailViewModel.ToList();
        //    foreach (var item in Vehicle)
        //    {
        //        item.Name += " Changed";
        //    }
        //    db.SaveChanges();
        //}

        //private readonly ApplicationDbContext _context;

        //public List<VehicleDetailViewModel> vehicleDetailViewModels { get; set; }

        //public NewController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}



        // GET: New/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.VehicleDetailViewModel == null)
            {
                return NotFound();
            }

            var vehicleDetailViewModel = await db.VehicleDetailViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleDetailViewModel == null)
            {
                return NotFound();
            }

            return View(vehicleDetailViewModel);
        }

        // GET: New/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: New/Create
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("color,material,Id,Name,Type,IsAvaible,picture")] VehicleDetailViewModel vehicleDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Add(vehicleDetailViewModel);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleDetailViewModel);
        }

        // GET: New/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.VehicleDetailViewModel == null)
            {
                return NotFound();
            }

            var vehicleDetailViewModel = await db.VehicleDetailViewModel.FindAsync(id);
            if (vehicleDetailViewModel == null)
            {
                return NotFound();
            }
            return View(vehicleDetailViewModel);
        }

        // POST: New/Edit/5
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
                    db.Update(vehicleDetailViewModel);
                    await db.SaveChangesAsync();
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

        // GET: New/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.VehicleDetailViewModel == null)
            {
                return NotFound();
            }

            var vehicleDetailViewModel = await db.VehicleDetailViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleDetailViewModel == null)
            {
                return NotFound();
            }

            return View(vehicleDetailViewModel);
        }

        // POST: New/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.VehicleDetailViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VehicleDetailViewModel'  is null.");
            }
            var vehicleDetailViewModel = await db.VehicleDetailViewModel.FindAsync(id);
            if (vehicleDetailViewModel != null)
            {
                db.VehicleDetailViewModel.Remove(vehicleDetailViewModel);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleDetailViewModelExists(int id)
        {
            return (db.VehicleDetailViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
