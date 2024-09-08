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
            // Fetch all HikayeÖzeti records
            var hikayeOzetleri = _unitOfWork.HikayeÖzeti.GetAll().ToList();
    
            // Initialize a list to hold the view models
            var hikayeOzetViewModelList = new List<HikayeÖzetiViewModel>();

            foreach (var hikaye in hikayeOzetleri)
            {
                // Fetch the related student (ApplicationUser) by ÖğrenciId
                var öğrenci = await _userManager.FindByIdAsync(hikaye.ÖğrenciId);
        
                if (öğrenci != null)
                {
                    // Populate the view model with the HikayeÖzeti and student's name & surname
                    var viewModel = new HikayeÖzetiViewModel
                    {
                        hikayeÖzeti = hikaye,  // Set the HikayeÖzeti object
                        ÖğrenciName = öğrenci.Ad,  // Assuming ApplicationUser has Name property
                        ÖğrenciSurname = öğrenci.Soyad  // Assuming ApplicationUser has Surname property
                    };

                    hikayeOzetViewModelList.Add(viewModel);
                }
            }

            // Pass the list of view models to the view
            return View(hikayeOzetViewModelList);
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
    
    
        
    
