using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestIdentityApp.Models;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data.Repository.IRepository;

namespace TestIdentityApp.Areas.Veli.Controllers
{
    [Area("Veli")]
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
            var veli = user as Data.Models.Veli;
            if (veli!= null)
            {
                foreach (var öğrenci in veli.Öğrenciler)
                {
                    var öğrenciHikayeOzetleri = _unitOfWork.HikayeÖzeti.GetAll(h => h.ÖğrenciId == öğrenci.Id).ToList();
                    hikayeOzetleri.AddRange(öğrenciHikayeOzetleri);
                }
            }
            
           
            return View(hikayeOzetleri);
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

       
       
        
    }
}