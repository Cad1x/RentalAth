using ATHRentalSystem.Data;
using ATHRentalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATHRentalSystem.Views
{
    public class TestModel : PageModel
    {

        public readonly ApplicationDbContext db;

        public List<VehicleDetailViewModel> Vehicle { get; set; }

        public TestModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void OnGet()
        {
            Vehicle = db.VehicleDetailViewModel.ToList();
        }

        public void OnPostUpdate()
        {
            Vehicle = db.VehicleDetailViewModel.ToList();
            foreach (var item in Vehicle)
            {
                item.Name += " Changed";
            }
            db.SaveChanges();
        }
    }
}
