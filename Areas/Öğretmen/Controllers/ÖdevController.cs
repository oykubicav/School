using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestIdentityApp.Data;
using TestIdentityApp.Models;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data.Repository.IRepository;



namespace TestIdentityApp.Areas.Öğretmen.Controllers;

[Area("Öğretmen")]
public class ÖdevController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ÖdevController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // GET
    [HttpGet]
    public IActionResult Create()
    {   ViewBag.Hafta = DateTime.Now.ToString("yyyy-MM-dd");
        return View();
    }

    [HttpPost]
    public IActionResult Create(IEnumerable<IFormFile> HomeworkFile, Ödev ödev, DateTime Hafta)
    {
        if (HomeworkFile != null && HomeworkFile.Any())
        {
            foreach (var file in HomeworkFile)
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
                if (string.IsNullOrEmpty(ödev.HomeworkFilePath))
                {
                    ödev.HomeworkFilePath = relativePath;
                }
                else
                {
                    ödev.HomeworkFilePath += "," + relativePath; // Append to existing file paths
                }
            }
        }

        // Handle other properties
        Hafta = DateTime.SpecifyKind(Hafta.Date, DateTimeKind.Local); // Treat as local date
        ödev.Tarih = Hafta.ToUniversalTime(); 

        _unitOfWork.Ödev.Add(ödev);
        _unitOfWork.Save();
    
        return View(); 
    }

    // GET
    public IActionResult Index(){
        var expiredHomeworks = _unitOfWork.Ödev.GetAll()
            .Where(o => o.Tarih <= DateTime.Now)
            .ToList();

        // Delete the expired homeworks
        foreach (var homework in expiredHomeworks)
        {
            _unitOfWork.Ödev.Remove(homework);
        }

        _unitOfWork.Save(); // Save changes to the database
 
        List<Ödev> Ödevler = _unitOfWork.Ödev.GetAll().ToList();
     
        return View(Ödevler);
       
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var ödev = _unitOfWork.Ödev.Get(o => o.Id == id);  // Lambda expression to find by Id


        if (ödev == null)
        {
            return NotFound(); // Handle if the Ödev object is not found
        }

        // Set ViewBag.Hafta for date input field
        ViewBag.Hafta = ödev.Tarih.HasValue ? ödev.Tarih.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");


        return View(ödev); // Pass the Ödev object to the view
    }

   [HttpPost]
public IActionResult Edit(IEnumerable<IFormFile> HomeworkFile, Ödev ödev, string[] DeleteFiles, DateTime Hafta)
{
    var existingÖdev = _unitOfWork.Ödev.Get(o => o.Id == ödev.Id); // Get the existing Ödev object
    
    // Handle file deletions (if any files are marked for deletion)
    if (DeleteFiles != null && DeleteFiles.Length > 0)
    {
        foreach (var file in DeleteFiles)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, file.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        
        // Remove deleted files from the HomeworkFilePath string
        var remainingFiles = existingÖdev.HomeworkFilePath.Split(',')
                            .Where(f => !DeleteFiles.Contains(f))
                            .ToArray();
        existingÖdev.HomeworkFilePath = string.Join(",", remainingFiles);
    }

    // Handle file uploads (if any new files are uploaded)
    if (HomeworkFile != null && HomeworkFile.Any())
    {
        foreach (var file in HomeworkFile)
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

            // Append new files to HomeworkFilePath
            if (string.IsNullOrEmpty(existingÖdev.HomeworkFilePath))
            {
                existingÖdev.HomeworkFilePath = relativePath;
            }
            else
            {
                existingÖdev.HomeworkFilePath += "," + relativePath;
            }
        }
    }

    // Update other fields
    existingÖdev.Başlık = ödev.Başlık;
    existingÖdev.İçerik = ödev.İçerik;
    Hafta = DateTime.SpecifyKind(Hafta.Date, DateTimeKind.Local); // Local time
    existingÖdev.Tarih = Hafta.ToUniversalTime();

    // Save the changes
    _unitOfWork.Ödev.Update(existingÖdev);
    _unitOfWork.Save();

    return RedirectToAction("Index");
}
[HttpPost, ActionName("Delete")]
   
public IActionResult DeletePost(int? id)
{
    if (id == null || id == 0)
    {
        return NotFound();
    }

    Ödev ödevtodelete = _unitOfWork.Ödev.Get(d => d.Id == id);
    if (ödevtodelete == null)
    {
        return NotFound();
    }

    _unitOfWork.Ödev.Remove(ödevtodelete);
    _unitOfWork.Save();
    return RedirectToAction("Index");
}


}