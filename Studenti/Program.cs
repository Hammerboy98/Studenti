using Microsoft.EntityFrameworkCore;
using Studenti.Data;
using Serilog;
using Microsoft.AspNetCore.Identity;
using Studenti.Models;

var builder = WebApplication.CreateBuilder(args);

// Configura Serilog prima di costruire l'app
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()  // Aggiungi un livello minimo di debug per raccogliere più dettagli
    .WriteTo.Console()  // Scrive i log sulla console
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)  // Scrive i log in un file con un intervallo di giorno
    .CreateLogger();

// Usa Serilog per il logging delle richieste
builder.Host.UseSerilog();

builder.Services.AddControllersWithViews();

// Configura il DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Usa il middleware Identity
app.UseAuthentication();
app.UseAuthorization();

// Usa Serilog per il logging delle richieste HTTP
app.UseSerilogRequestLogging(opts =>
{
    opts.MessageTemplate = "Handled {RequestMethod} {RequestPath}";
});

// Configura il pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
