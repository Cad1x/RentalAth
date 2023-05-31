using System.ComponentModel.DataAnnotations;

namespace ATHRentalSystem.Models
{
    public class VehicleItemViewModel
    {
        public int Id { get; set; }
        [Display(Name="Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Typ")]
        public string Type { get; set; }
        [Display(Name = "Dostępność")]
        public bool IsAvaible { get; set; }
        [Display(Name = "Zdjęcie poglądowe")]
        public string picture { get; set; }

    }
}
