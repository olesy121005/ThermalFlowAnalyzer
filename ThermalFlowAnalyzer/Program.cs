using Microsoft.EntityFrameworkCore;
using ThermalFlowAnalyzer.Data;
using ThermalFlowAnalyzer.Infrastructure;
using ThermalFlowAnalyzer.Logic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// ================== SERVICES ==================

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(
        0,
        new DoubleModelBinderProvider()
    );
});

// ===== ¡¿«¿ ƒ¿ÕÕ€’ =====
builder.Services.AddDbContext<ThermalDbContext>(options =>
    options.UseSqlite("Data Source=thermalflow.db"));

// ===== —≈–¬»—€ =====
builder.Services.AddScoped<ICounterflowSolver, CounterflowSolver>();

// =====  ”À‹“”–¿ RU / EN =====
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = new[]
    {
        new CultureInfo("ru-RU"),
        new CultureInfo("en-US")
    };

    options.DefaultRequestCulture = new RequestCulture("ru-RU");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

// ================== BUILD ==================
var app = builder.Build();

// ================== MIDDLEWARE ==================
app.UseRequestLocalization();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Analysis}/{action=Dashboard}/{id?}");

app.Run();
