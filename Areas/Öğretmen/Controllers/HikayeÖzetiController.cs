using Microsoft.AspNetCore.Authorization;
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

        public HikayeÖzetiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: HikayeÖzeti/Index
        public IActionResult Index()
        {
            List<HikayeÖzeti> hikayÖzetiList = _unitOfWork.HikayeÖzeti.GetAll().ToList();
            return View(hikayÖzetiList);
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
    
    
        
    
