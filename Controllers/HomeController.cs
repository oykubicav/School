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
        var lastYıldızÖğrenci = _unitOfWork.YıldızÖğrenci.GetAll()
            .OrderByDescending(yo => yo.Id) // Assuming Id is the primary key
            .FirstOrDefault();

        
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var student = await _userManager.FindByIdAsync(lastYıldızÖğrenci.ÖğrenciId);
                var model = new HomeViewModel
                {  Name = student.Ad,
                    Surname = student.Soyad,
                    User = user,
                    Roles = roles ?? new List<string>()
                };
        
                return View(model);
            }
    
            // If the user is not logged in, return the view without any model
            return View();
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
