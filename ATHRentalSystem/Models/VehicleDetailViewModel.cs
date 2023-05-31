using System.ComponentModel.DataAnnotations;

namespace ATHRentalSystem.Models
{
    public class VehicleDetailViewModel : VehicleItemViewModel
    {
        [Display(Name = "Kolor")]
        public string color { get; set; }
        [Display(Name = "Tworzywo")]
        public string material { get; set; }
    }
}
