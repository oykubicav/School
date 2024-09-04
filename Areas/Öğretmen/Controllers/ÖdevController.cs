using Microsoft.AspNetCore.Authorization;
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

    public ÖdevController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
        _context = context;
    }

    // GET
    [HttpGet]
    public IActionResult Create()
    {   ViewBag.Hafta = DateTime.Now.ToString("yyyy-MM-dd");
        return View();
    }

    [HttpPost]
    public IActionResult Create(IFormFile HomeworkFile, Ödev ödev, DateTime Hafta)
    {
        if (ödev.HomeworkFile != null)
        {

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + ödev.HomeworkFile.FileName;
            string relativePath = "/Images/" + uniqueFileName;

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ödev.HomeworkFile.CopyTo(fileStream);
            }

            ödev.HomeworkFilePath = relativePath;

        }
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
    public IActionResult Edit(IFormFile HomeworkFile, Ödev ödev, bool DeleteExistingFile, DateTime Hafta)
    {
        var existingÖdev = _unitOfWork.Ödev.Get(o => o.Id == ödev.Id); // Get the existing Ödev object

        // Handle file deletion if requested
        if (DeleteExistingFile && !string.IsNullOrEmpty(existingÖdev.HomeworkFilePath))
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, existingÖdev.HomeworkFilePath.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            existingÖdev.HomeworkFilePath = null; // Clear the file path
        }

        // Handle file upload (if a new file is uploaded)
        if (HomeworkFile != null)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(HomeworkFile.FileName);
            string relativePath = "/Images/" + uniqueFileName;

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                HomeworkFile.CopyTo(fileStream);
            }

            existingÖdev.HomeworkFilePath = relativePath; // Update the file path
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

}