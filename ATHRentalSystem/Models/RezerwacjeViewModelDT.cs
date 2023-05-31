namespace ATHRentalSystem.Models
{
    public class RezerwacjeViewModelDT
    {
        public int RezerwacjaId { get; set; }
        public string ImieRezerwanta { get; set; }

        public string NazwiskoRezerwanta { get; set; }

        public DateTime RezerwacjaOd { get; set; }

        public DateTime RezerwacjaDo { get; set; }
    }
}
