using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using TestIdentityApp;
using TestIdentityApp.Data;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data.Repository;
using TestIdentityApp.Data.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole(); 

// Updated connection string with actual PostgreSQL connection details
var connectionString = "Host=34.107.39.80;Database=postgres;Username=postgres;Password=Lebron2003";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configure Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Require confirmed email to login
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB limit for file uploads
});

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

// Configure application cookies for authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

var app = builder.Build();

// Configure HTTP request pipeline

// Remove UseHttpsRedirection since it's handled by NGINX
// app.UseHttpsRedirection(); // Commented out

// Add UseForwardedHeaders for reverse proxy
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Developer exception page for development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Use HSTS in production
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Configure route mappings
app.MapRazorPages();
app.MapAreaControllerRoute(
    name: "ÖğretmenArea",
    areaName: "Öğretmen",
    pattern: "Öğretmen/{controller=HikayeÖzeti}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "ÖğrenciArea",
    areaName: "Öğrenci",
    pattern: "Öğrenci/{controller=HikayeÖzeti}/{action=Create}/{id?}");

app.MapAreaControllerRoute(
    name: "VeliArea",
    areaName: "Veli",
    pattern: "Veli/{controller=HikayeÖzeti}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
