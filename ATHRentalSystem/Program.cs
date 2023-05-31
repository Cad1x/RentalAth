using ATHRentalSystem.Data;
using ATHRentalSystem.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("ATHRentalSystem") );
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAutoMapper(typeof(Program));

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddCoreAdmin();


var app = builder.Build();



//builder.Services.AddControllers()
//    .AddFluentValidation(c => c.RegisterValidatorsFromAssemblies(Assembly.GetExecutingAssembly()));

//builder.Services.AddMvc();
//builder.Services.AddScoped<IValidator<RezerwacjeViewModel, RezerwacjeValidator>();

AddVehicleData(app);
AddShopData(app);
AddRezerwacja(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Admin",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
   name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapRazorPages();
});


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
//app.UseCoreAdminCustomUrl("admin");





static void AddVehicleData(WebApplication app)
{

    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

    var Vehicle1 = new VehicleDetailViewModel { Id = 1, Name = "G�rol", Type = "G�rski", IsAvaible = true, picture = "https://www.awex-rowery.pl/wp-content/uploads/2021/01/dsc0640-scaled.jpg", color = "czarno-zielony", material = "metal" };
    var Vehicle2 = new VehicleDetailViewModel { Id = 2, Name = "Mieszczuch", Type = "Miejski", IsAvaible = true, picture = "https://a.allegroimg.com/original/114a96/a8194ab44181a84ec40553775847/Rower-Meski-Miejski-28-uniwersal-DALLAS-na-prezent-Rok-produkcji-2021", color = "czarny", material = "metal" };
    var Vehicle3 = new VehicleDetailViewModel { Id = 3, Name = "Szpaner", Type = "Miejski", IsAvaible = false, picture = "http://i.kinja-img.com/gawker-media/image/upload/s--8zoC0a7C--/c_fit,fl_progressive,q_80,w_636/18rbjdq6jpnkrjpg.jpg", color = "br�zowy", material = "drewno" };

    db.VehicleModel.Add(Vehicle1);
    db.VehicleModel.Add(Vehicle2);
    db.VehicleModel.Add(Vehicle3);

    db.SaveChanges();

}

static void AddRezerwacja(WebApplication app)
{

    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

    var Vehicle1 = new RezerwacjeViewModel { RezerwacjaId = 1, ImieRezerwanta="Marcin", NazwiskoRezerwanta="Prokon", RezerwacjaOd = new DateTime(2023, 4, 23), RezerwacjaDo = new DateTime(2023, 5, 23) };
    var Vehicle2 = new RezerwacjeViewModel { RezerwacjaId = 2, ImieRezerwanta = "�ukasz", NazwiskoRezerwanta = "Maszwid�y", RezerwacjaOd = new DateTime(2023, 4, 30), RezerwacjaDo = new DateTime(2023, 6, 27) };
    var Vehicle3 = new RezerwacjeViewModel { RezerwacjaId = 3, ImieRezerwanta = "Emanuel", NazwiskoRezerwanta = "P�yw", RezerwacjaOd = new DateTime(2023, 4, 29), RezerwacjaDo = new DateTime(2023, 5, 30) };

    db.rezerwacje.Add(Vehicle1);
    db.rezerwacje.Add(Vehicle2);
    db.rezerwacje.Add(Vehicle3);

    db.SaveChanges();
}

static void AddShopData(WebApplication app)
{

    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

    var Shop1 = new PunktWypozyczenViewModel { PunktId = 1, NazwaWypozyczalnia = "Palnik", Miasto = "Warszawa",Ulica="Weso�a", Numer = "11A"};
    var Shop2 = new PunktWypozyczenViewModel { PunktId = 2, NazwaWypozyczalnia = "Turbisz", Miasto = "Bielsko-Bia�a", Ulica = "Krzywa", Numer = "123"};
    var Shop3 = new PunktWypozyczenViewModel { PunktId = 3, NazwaWypozyczalnia = "D�on", Miasto = "Rybnik", Ulica = "Wysoka", Numer = "42"};

    db.PunktWypozyczenViewModel.Add(Shop1);
    db.PunktWypozyczenViewModel.Add(Shop2);
    db.PunktWypozyczenViewModel.Add(Shop3);

    db.SaveChanges();
}

using(var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Moderator", "User" };
    foreach (var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = 
        scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string email = "admin@email.com";
    string password = "Qwerty1.";

    if(await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;
        await userManager.CreateAsync(user, password);

        userManager.AddToRoleAsync(user, "Admin");
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string email = "mod@email.com";
    string password = "Qwerty1.";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;
        await userManager.CreateAsync(user, password);

        userManager.AddToRoleAsync(user, "Moderator");
    }
}
using (var scope = app.Services.CreateScope())
{
    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string email = "user@email.com";
    string password = "Qwerty1.";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;
        await userManager.CreateAsync(user, password);

        userManager.AddToRoleAsync(user, "User");
    }
}

app.Run();
//static void ConfigureServices(IServiceCollection services)
//{
//    services.AddControllers();
//    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//    services.AddMvc();
//    services.AddScoped<IValidator<RezerwacjeViewModel>, RezerwacjeValidator>();

//}