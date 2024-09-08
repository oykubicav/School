using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestIdentityApp.Models;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data.Repository.IRepository;

namespace TestIdentityApp.Areas.Öğretmen.Controllers
{
    [Area("Öğretmen")]
    //[Authorize(Roles = SD.Role_Öğretmen)]
    public class HikayeÖzetiController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public HikayeÖzetiController(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: HikayeÖzeti/Index
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            // Fetch the stories for the student
            var hikayeOzetleri = _unitOfWork.HikayeÖzeti.GetAll(h => h.ÖğrenciId == user.Id).ToList();

            // Create a list of view models
            List<HikayeÖzetiViewModel> viewModelList = new List<HikayeÖzetiViewModel>();

            foreach (var hikaye in hikayeOzetleri)
            {
                // Fetch the related student (ApplicationUser) by ÖğrenciId
                var öğrenci = await _userManager.FindByIdAsync(hikaye.ÖğrenciId);

                // Populate the view model with both story and student details
                var viewModel = new HikayeÖzetiViewModel
                {
                    Id = hikaye.Id,
                    HikayeAdı = hikaye.HikayeAdı,
                    Özet = hikaye.Özet,
                    ÖğrenciName = öğrenci.Ad,
                    ÖğrenciSurname = öğrenci.Soyad
                };

                viewModelList.Add(viewModel);
            }

            // Pass the view model list to the view
            return View(viewModelList);
        }


        // GET: HikayeÖzeti/Create
      
        // POST: HikayeÖzeti/Create
        
       
      

        // GET: HikayeÖzeti/Edit/{id}
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            HikayeÖzeti hikayeÖzeti = _unitOfWork.HikayeÖzeti.Get(h => h.Id == id);
            if (hikayeÖzeti == null)
            {
                return NotFound();
            }

            return View(hikayeÖzeti);
        }

        // POST: HikayeÖzeti/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HikayeÖzeti hikayeÖzeti)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.HikayeÖzeti.Update(hikayeÖzeti);
                _unitOfWork.Save();
                TempData["SuccessMessage"] = "Hikaye Özeti başarıyla güncellendi.";
                return RedirectToAction("Index");
            }

            return View(hikayeÖzeti);
        }

        // GET: HikayeÖzeti/Details/{id}
        public IActionResult Details(int id)
        {
            HikayeÖzeti hikayeÖzeti = _unitOfWork.HikayeÖzeti.Get(h => h.Id == id);
            if (hikayeÖzeti == null)
            {
                return NotFound();
            }
            return View(hikayeÖzeti);
        }

        // GET: HikayeÖzeti/Delete/{id}
       
     
    }
}
    
    
        
    
