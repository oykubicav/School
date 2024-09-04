using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestIdentityApp.Data;
using TestIdentityApp.Models;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data.Repository.IRepository;

namespace TestIdentityApp.Areas.Öğretmen.Controllers
{
    [Area("Öğretmen")]
    //[Authorize(Roles = SD.Role_Öğretmen)]
    public class YıldızÖğrenciController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public YıldızÖğrenciController(IUnitOfWork unitOfWork, ApplicationDbContext applicationDbContext,
            RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _context = applicationDbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> SelectYıldızÖğrenci()
        {
            var students = await _userManager.GetUsersInRoleAsync("Öğrenci");

            ViewBag.Öğrenciler = students.Select(s => new SelectListItem
            {
                Value = s.Id,
                Text = $"{s.Ad} {s.Soyad}"
            }).ToList();

            // Default to the current week
            ViewBag.Hafta = DateTime.Now.ToString("yyyy-MM-dd");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SelectYıldızÖğrenci(string selectedÖğrenciId, DateTime hafta)
        {
            if (string.IsNullOrEmpty(selectedÖğrenciId) || hafta == default)
            {
                // Re-populate the dropdown list in case of validation errors
                var students = await _userManager.GetUsersInRoleAsync("Öğrenci");
                ViewBag.Öğrenciler = students.Select(s => new SelectListItem
                {
                    Value = s.Id,
                    Text = $"{s.Ad} {s.Soyad}"
                }).ToList();

                // Re-populate the week
                ViewBag.Hafta = hafta.ToString("yyyy-MM-dd");

                return View();
            }

            var yıldızÖğrenci = new YıldızÖğrenci
            {
                ÖğrenciId = selectedÖğrenciId,
                Hafta = hafta.ToUniversalTime() // Convert to
            };

            _unitOfWork.YıldızÖğrenci.Add(yıldızÖğrenci);
            _unitOfWork.Save();

            TempData["SuccessMessage"] = "Yıldız Öğrenci başarıyla seçildi.";
            return RedirectToAction("Profile", "UserPage", new { area = "" });
        }
    }
}
