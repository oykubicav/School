using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data.Repository.IRepository;
using TestIdentityApp.Models;

namespace TestIdentityApp.Controllers;

public class UserPageController : Controller

{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UserPageController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
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
        var students = await _userManager.GetUsersInRoleAsync("Öğrenci");
        var roles = await _userManager.GetRolesAsync(user);

        List<HikayeÖzeti> hikayeOzetleri = new List<HikayeÖzeti>();
        List<Öğrenci> Öğrenciler = new List<Öğrenci>();
        

        if (roles.Contains("Öğretmen"))
        {
            hikayeOzetleri = _unitOfWork.HikayeÖzeti.GetAll().ToList();
            Öğrenciler = _unitOfWork.Öğrenci.GetAll().ToList();
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

    [HttpPost]
    public async Task<IActionResult> ChangeProfilePhoto(IFormFile ProfileImage)
    {
        if (ProfileImage != null)
        {
            var user = await _userManager.GetUserAsync(User);
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + ProfileImage.FileName;
            string relativePath = "/images/" + uniqueFileName;

            string imagePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                ProfileImage.CopyTo(fileStream);
            }

            user.ImageUrl = relativePath;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Profile");
        }
        return RedirectToAction("Profile"); 
    }

    [HttpPost]
    public async Task<IActionResult> RemoveProfilePhoto()
    {
        var user = await _userManager.GetUserAsync(User);
        user.ImageUrl = null;
        await _userManager.UpdateAsync(user);
        return RedirectToAction("Profile");
    }




}