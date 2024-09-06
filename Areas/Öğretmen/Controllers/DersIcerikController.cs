using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestIdentityApp.Data;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data.Repository.IRepository;


namespace TestIdentityApp.Areas.Öğretmen.Controllers;

[Area("Öğretmen")]

public class DersIcerikController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public DersIcerikController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment,
        ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(IEnumerable<IFormFile> IcerikFiles, DersIcerik dersIcerik)
    {
        if (dersIcerik != null && IcerikFiles.Any())
        {
            foreach (var file in IcerikFiles)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string relativePath = "/Images/" + uniqueFileName;

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                // You can append file paths to a list or concatenate if needed
                // Assuming you store file paths as a comma-separated string in HomeworkFilePath for now
                if (string.IsNullOrEmpty(dersIcerik.IcerikFileePath))
                {
                    dersIcerik.IcerikFileePath = relativePath;
                }
                else
                {
                    dersIcerik.IcerikFileePath += "," + relativePath; // Append to existing file paths
                }
            }
        }

        // Handle other properties

        _unitOfWork.DersIcerik.Add(dersIcerik);
        _unitOfWork.Save();

        return View();
    }

    public IActionResult Index(string dersAdi)
    {
        
        // Filter by the selected lesson
        List<DersIcerik> dersIceriks = _unitOfWork.DersIcerik
            .GetAll()
            .Where(d => d.DersAdi == dersAdi)
            .ToList();

        // Pass the filtered list to the view
        return View(dersIceriks);
    }

    


    [HttpPost, ActionName("Delete")]
   
    public IActionResult DeletePost(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        DersIcerik dersIceriktodelete = _unitOfWork.DersIcerik.Get(d => d.Id == id);
        if (dersIceriktodelete == null)
        {
            return NotFound();
        }

        _unitOfWork.DersIcerik.Remove(dersIceriktodelete);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }


}

    