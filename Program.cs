using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Azure.Identity; // Add Azure SDK for managed identity
using Npgsql;
using TestIdentityApp;
using TestIdentityApp.Data;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data.Repository;
using TestIdentityApp.Data.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole(); 

// Instead of fetching the connection string directly, we will build it dynamically
var credential = new DefaultAzureCredential();
var connectionStringBuilder = new NpgsqlConnectionStringBuilder
{
    Host = "ogrenciplatformu-server.postgres.database.azure.com",  // Replace with your host
    Database = "postgres",                                         // Replace with your database
    Port = 5432,
    SslMode = SslMode.Require,
    TrustServerCertificate = true
};

// Add a new connection string that uses managed identity (Azure SDK)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connection = new NpgsqlConnection(connectionStringBuilder.ToString());
    options.UseNpgsql(connection);
});

// Identity setup
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

// Add services to the container
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

// Remove UseHttpsRedirection as it's handled by NGINX
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Ensure this is enabled for detailed error information in development
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

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
