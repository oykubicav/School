using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestIdentityApp.Models;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data.Repository.IRepository;

namespace TestIdentityApp.Areas.Öğrenci.Controllers
{
    [Area("Öğrenci")]
    //[Authorize(Roles = SD.Role_Öğretmen)]
    public class HikayeÖzetiController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public HikayeÖzetiController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: HikayeÖzeti/Index
        public async Task<IActionResult> Index()
        {    var user = await _userManager.GetUserAsync(User);
          

            var roles = await _userManager.GetRolesAsync(user);

            List<HikayeÖzeti> hikayeOzetleri = new List<HikayeÖzeti>();
            hikayeOzetleri = _unitOfWork.HikayeÖzeti.GetAll(h => h.ÖğrenciId == user.Id).ToList();
            return View(hikayeOzetleri);
        }

        // GET: HikayeÖzeti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HikayeÖzeti/Create
        [HttpPost]

       
        public async Task<IActionResult> Create(HikayeÖzeti hikayeÖzeti)
        {
            // Check if the model is valid
            
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                // Set the ÖğrenciId to the user's Id
                hikayeÖzeti.ÖğrenciId = user.Id;

                _unitOfWork.HikayeÖzeti.Add(hikayeÖzeti);

              
                _unitOfWork.Save();

                TempData["SuccessMessage"] = "Hikaye Özeti başarıyla eklendi.";
                return RedirectToAction("Index", "HikayeÖzeti"); // Redirect to a different action, such as an index or details page
            }

            // If user is null, return the view with an error
            ModelState.AddModelError(string.Empty, "User not found.");
            return View(hikayeÖzeti);
        }



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
            Console.WriteLine($"Id: {hikayeÖzeti.Id}, HikayeAdı: {hikayeÖzeti.HikayeAdı}, Özet: {hikayeÖzeti.Özet}");
            return View(hikayeÖzeti);
        }

        // POST: HikayeÖzeti/Edit
        [HttpPost]
        
        public async Task<IActionResult> Edit(HikayeÖzeti hikayeÖzeti)
        {

            
            
                // Log the incoming values to verify they are being bound correctly
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    // Set the ÖğrenciId to the user's Id
                    hikayeÖzeti.ÖğrenciId = user.Id;
                    hikayeÖzeti.HikayeAdı = Request.Form["HikayeAdı"];
                    hikayeÖzeti.Özet = Request.Form["Özet"];
                    Console.WriteLine($"Id: {hikayeÖzeti.Id}, HikayeAdı: {hikayeÖzeti.HikayeAdı}, Özet: {hikayeÖzeti.Özet}");
                // Update the entity in the database
                _unitOfWork.HikayeÖzeti.Update(hikayeÖzeti); 
                _unitOfWork.Save();

                TempData["SuccessMessage"] = "Hikaye Özeti başarıyla güncellendi.";
                return View(hikayeÖzeti);
            }

            // If something goes wrong, return the same view with the model to show validation errors
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
      
        // POST: HikayeÖzeti/Delete/{id}
        [HttpPost, ActionName("Delete")]
      
        public IActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HikayeÖzeti hikayeözetiToDelete = _unitOfWork.HikayeÖzeti.Get(h => h.Id == id);
            if (hikayeözetiToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.HikayeÖzeti.Remove(hikayeözetiToDelete);
            _unitOfWork.Save();
            TempData["SuccessMessage"] = "Hikaye Özeti başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}
    
    
        
