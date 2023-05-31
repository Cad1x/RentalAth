using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ATHRentalSystem.Models;
using ATHRentalSystem.Areas.Admin.Models;

namespace ATHRentalSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<ATHRentalSystem.Models.VehicleDetailViewModel>? VehicleDetailViewModel { get; set; }
        public DbSet<ATHRentalSystem.Models.PunktWypozyczenViewModel>? PunktWypozyczenViewModel { get; set; }
        // protected override void OnConfiguring
        //(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseInMemoryDatabase(databaseName: "ATHRentalSystem");
        // }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<VehicleDetailViewModel>()
                .HasKey(o => o.Id);
            modelBuilder.Entity<PojazdViewModel>()
               .HasKey(o => o.VehicleId);
            modelBuilder.Entity<PunktWypozyczenViewModel>()
               .HasKey(o => o.PunktId);
            modelBuilder.Entity<RezerwacjeViewModel>()
               .HasKey(o => o.RezerwacjaId);
            modelBuilder.Entity<TypPojazduViewModel>()
               .HasKey(o => o.TypId);
        }
        public DbSet<ATHRentalSystem.Models.VehicleDetailViewModel>? VehicleModel { get; set; }
        public DbSet<ATHRentalSystem.Models.PojazdViewModel>? pojazd { get; set; }
        public DbSet<ATHRentalSystem.Models.PunktWypozyczenViewModel>? punktWypożyczeń { get; set; }
        public DbSet<ATHRentalSystem.Models.RezerwacjeViewModel>? rezerwacje { get; set; }
        public DbSet<ATHRentalSystem.Models.TypPojazduViewModel>? typPojazdu { get; set; }
        public DbSet<ATHRentalSystem.Areas.Admin.Models.Admin>? Admin { get; set; }
    }
}