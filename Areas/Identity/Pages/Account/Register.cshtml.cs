#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using TestIdentityApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using NuGet.Packaging.Signing;
using TestIdentityApp.Data;
using TestIdentityApp.Data.Models;

namespace TestIdentityApp.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            ApplicationDbContext context,
            IEmailSender emailSender,
            IWebHostEnvironment webHostEnvironment)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty] public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Role { get; set; }

            [ValidateNever] public IEnumerable<SelectListItem> RoleList { get; set; }

            [Required] public string Name { get; set; }

            public string Surname { get; set; }

            public string PhoneNumber { get; set; }

            public IFormFile ProfileImage { get; set; }
            [Display(Name = "Öğrenciler")] public List<string> SelectedÖğrencilerIds { get; set; } = new List<string>();

            [ValidateNever] public List<SelectListItem> ÖğrencilerList { get; set; }


        }

        public async Task OnGetAsync(string returnUrl = null, string role = null)
        {
            if (string.IsNullOrEmpty(role))
            {
                _logger.LogError(
                    "Role is null or empty before calling AddToRoleAsync. Assigning default role 'Öğrenci'.");

            }

            Input = new InputModel
            {
                Role = role
            };
            _logger.LogInformation($"Role set during form submission: {Input.Role}");

            if (role == SD.Role_Veli)
            {
                Input.ÖğrencilerList = _context.Öğrenciler.Select(o => new SelectListItem
                    { Value = o.Id.ToString(), Text = o.Ad + " " + o.Soyad }).ToList();
            }

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

public async Task<IActionResult> OnPostAsync(string returnUrl = null)
{_logger.LogInformation("Form submission started.");
    returnUrl ??= Url.Content("~/");
    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
    _logger.LogInformation($"Role received: {Input.Role}");

    ApplicationUser user;
    string imagePath = null;
    string relativePath = null; 

    if (Input.ProfileImage != null)
    {
        _logger.LogInformation($"Profile image received: {Input.ProfileImage.FileName}");
        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        string uniqueFileName = Guid.NewGuid().ToString() + Input.ProfileImage.FileName;
        relativePath = "/images/" + uniqueFileName;
                   
        imagePath = Path.Combine(uploadsFolder, uniqueFileName);
        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            Input.ProfileImage.CopyTo(fileStream);
        }
                  
    }

    // Check the role and create the appropriate user type
    
    // Check the role and create the appropriate user type
    if (Input.Role == SD.Role_Öğretmen)
    {
        user = new Data.Models.Öğretmen
        {
            UserName = Input.Email,
            Ad = Input.Name,
            Soyad = Input.Surname,
            PhoneNumber = Input.PhoneNumber,
            ImageUrl = relativePath
        };
    }
    else if (Input.Role == SD.Role_Öğrenci)
    {
        user = new Data.Models.Öğrenci
        {
            UserName = Input.Email,
            Ad = Input.Name,
            Soyad = Input.Surname,
            VeliTelefon = Input.PhoneNumber,
            ImageUrl = relativePath
        };
    }
    else
    {
        user = new Data.Models.Veli
        {
            UserName = Input.Email,
            Ad = Input.Name,
            Soyad = Input.Surname,
            PhoneNumber = Input.PhoneNumber,
            ImageUrl = relativePath,
            Öğrenciler = new List<Data.Models.Öğrenci>()
        };
        foreach (var studentId in Input.SelectedÖğrencilerIds)
        {
            var öğrenci = await _context.Öğrenciler.FindAsync(studentId);
            if (öğrenci != null)
            {
                öğrenci.VeliId = user.Id; // Set the foreign key
                öğrenci.Veli = (Veli)user;
                ((Data.Models.Veli)user).Öğrenciler.Add(öğrenci);
            }
        }
    }

    await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
    await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

    var result = await _userManager.CreateAsync(user, Input.Password);

    if (result.Succeeded)
    {
        _logger.LogInformation("User created a new account with password.");

        if (string.IsNullOrEmpty(Input.Role))
        {
            _logger.LogError("Role is null or empty before calling AddToRoleAsync");
        }
        else
        {
            _logger.LogInformation($"Attempting to add user to role: {Input.Role}");
            await _userManager.AddToRoleAsync(user, Input.Role);
        }

        var userId = await _userManager.GetUserIdAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callbackUrl = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
            protocol: Request.Scheme);

        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        if (_userManager.Options.SignIn.RequireConfirmedAccount)
        {
            return RedirectToPage("RegisterConfirmation",
                new { email = Input.Email, returnUrl = returnUrl });
        }
        else
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);
        }
    }

    foreach (var error in result.Errors)
    {
        ModelState.AddModelError(string.Empty, error.Description);
    }

    return Page();
}
 

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }

            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}

