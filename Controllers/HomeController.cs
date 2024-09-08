using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data.Repository.IRepository;
using TestIdentityApp.Models;

namespace TestIdentityApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork)
    {     
        _logger = logger;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        // Get the last Yıldız Öğrenci record or null if none exists
        var lastYıldızÖğrenci = _unitOfWork.YıldızÖğrenci.GetAll()
            .OrderByDescending(yo => yo.Id)
            .FirstOrDefault();

        var user = await _userManager.GetUserAsync(User);

        // Prepare an empty model if there's no Yıldız Öğrenci or user
        var model = new HomeViewModel();

        if (user != null)
        {
            var roles = await _userManager.GetRolesAsync(user);

            // If there's a Yıldız Öğrenci record, find the student
            if (lastYıldızÖğrenci != null)
            {
                var student = await _userManager.FindByIdAsync(lastYıldızÖğrenci.ÖğrenciId);
                if (student != null)
                {
                    model.Name = student.Ad;
                    model.Surname = student.Soyad;
                }
            }

            // Set user and roles regardless of Yıldız Öğrenci existence
            model.User = user;
            model.Roles = roles ?? new List<string>();
        }

        // Return the populated model to the view
        return View(model);
    }


    public IActionResult Blog()
    {
        return View(); // This will render blog.cshtml from the Views/Home folder
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
