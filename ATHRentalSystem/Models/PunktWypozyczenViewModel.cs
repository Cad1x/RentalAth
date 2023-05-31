using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace ATHRentalSystem.Models
{
    public class PunktWypozyczenViewModel
    {
        public int PunktId { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Użyj tylko liter")]

        public string NazwaWypozyczalnia { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Użyj tylko liter")]
        public string Miasto { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Użyj tylko liter")]

        public string Ulica { get; set; }
        [RegularExpression(@"^[1-9]+$", ErrorMessage = "Użyj tylko liczb")]

        public string Numer { get; set; }
        //public int IloscDostepnych { get; set; }


    }
}
