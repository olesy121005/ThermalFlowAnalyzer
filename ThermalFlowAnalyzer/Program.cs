using Microsoft.EntityFrameworkCore;
using ThermalFlowAnalyzer.Data;
using ThermalFlowAnalyzer.Logic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// =====  ”À‹“”–¿ RU =====
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("ru-RU"),
        new CultureInfo("en-US")
    };

    options.DefaultRequestCulture = new RequestCulture("ru-RU");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// ===== ¡ƒ =====
builder.Services.AddDbContext<ThermalDbContext>(options =>
    options.UseSqlite("Data Source=thermalflow.db"));

builder.Services.AddScoped<ICounterflowSolver, CounterflowSolver>();

var app = builder.Build();

app.UseRequestLocalization();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Analysis}/{action=Dashboard}/{id?}");

app.Run();
