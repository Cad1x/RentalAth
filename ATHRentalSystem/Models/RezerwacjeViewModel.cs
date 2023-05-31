//using FluentValidation;
//using FluentValidation.Results;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ATHRentalSystem.Models
{
    public class RezerwacjeViewModel : IValidatableObject
    {
        public int RezerwacjaId { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Użyj tylko liter")]

        public string ImieRezerwanta { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Użyj tylko liter")]

        public string NazwiskoRezerwanta { get; set; }

        public DateTime RezerwacjaOd { get; set; }

        public DateTime RezerwacjaDo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (RezerwacjaOd > RezerwacjaDo) yield return new ValidationResult("Nie oddasz roweru przed jego wypożyczeniem");
            if (ImieRezerwanta is null) yield return new ValidationResult("Wprowadź imię!");
            if (NazwiskoRezerwanta is null) yield return new ValidationResult("Wprowadź Nazwisko!");

        }
    }

   

    //public class RezerwacjeValidator : AbstractValidator<RezerwacjeViewModel>
    //{
    //    public RezerwacjeValidator()
    //    {
    //        RuleFor(x => x.ImieRezerwanta).NotEmpty().WithMessage($"Wprowadź imię!");
    //        RuleFor(x => x.NazwiskoRezerwanta).NotEmpty().WithMessage($"Wprowadź Nazwisko!");
    //        RuleFor(x => x.RezerwacjaOd).LessThanOrEqualTo(z=>z.RezerwacjaDo).WithMessage($"Data wypożyczenia musi być mniejsza");
    //        RuleFor(x => x.RezerwacjaOd).NotEmpty().WithMessage($"Wprowadź datę oddania!");
    //    }
    //}
 
}
