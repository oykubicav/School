using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data.Repository.IRepository;

namespace TestIdentityApp.Controllers;

public class UserPageController : Controller

{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public UserPageController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    // GET
    [HttpGet]

    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var roles = await _userManager.GetRolesAsync(user);

        List<HikayeÖzeti> hikayeOzetleri = new List<HikayeÖzeti>();

        if (roles.Contains("Öğretmen"))
        {
            hikayeOzetleri = _unitOfWork.HikayeÖzeti.GetAll().ToList();
        }
        else if (roles.Contains("Öğrenci"))
        {
            hikayeOzetleri = _unitOfWork.HikayeÖzeti.GetAll(h => h.ÖğrenciId == user.Id).ToList();
        }
        else if (roles.Contains("Veli"))
        {
            var veli = user as Veli; // Assuming your Veli class is derived from ApplicationUser
            if (veli != null)
            {
                foreach (var öğrenci in veli.Öğrenciler)
                {
                    var öğrenciHikayeOzetleri = _unitOfWork.HikayeÖzeti.GetAll(h => h.ÖğrenciId == öğrenci.Id).ToList();
                    hikayeOzetleri.AddRange(öğrenciHikayeOzetleri);
                }
            }
        }

        var model = new UserProfileViewModel
        {
            User = user,
            HikayeOzetleri = hikayeOzetleri,
            Roles = roles
        };

        return View(model);
    }
}