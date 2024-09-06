using Microsoft.AspNetCore.Http.Features;
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

var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") 
                       ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true; // Require confirmed email to login
      
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Enable detailed error pages
}
else
{
    app.UseExceptionHandler("/Home/Error");  // Fallback error handler for production
    app.UseHsts();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

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

