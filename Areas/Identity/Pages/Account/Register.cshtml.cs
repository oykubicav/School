#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty] 
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Role { get; set; }

            [ValidateNever]
            public IEnumerable<SelectListItem> RoleList { get; set; }

            [Required]
            public string Name { get; set; }

            public string Surname { get; set; }

            public string PhoneNumber { get; set; }

            public IFormFile ProfileImage { get; set; }

            [Display(Name = "Öğrenciler")]
            public List<string> SelectedÖğrencilerIds { get; set; } = new List<string>();

            [ValidateNever]
            public List<SelectListItem> ÖğrencilerList { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null, string role = null)
        {
            Input = new InputModel { Role = role };

            if (role == SD.Role_Veli)
            {
                Input.ÖğrencilerList = _context.Öğrenciler
                    .Select(o => new SelectListItem { Value = o.Id.ToString(), Text = o.Ad + " " + o.Soyad })
                    .ToList();
            }

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Create user object based on role
            ApplicationUser user = CreateUserBasedOnRole();

            // Handle Profile Image Upload if exists
            string relativePath = await HandleProfileImageUploadAsync();

            if (!string.IsNullOrEmpty(relativePath))
            {
                user.ImageUrl = relativePath;
            }

            await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                await AssignUserRoleAsync(user);
                return RedirectToConfirmationPage(user, returnUrl);
            }

            // Add errors to ModelState if user creation fails
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

        private async Task<string> HandleProfileImageUploadAsync()
        {
            if (Input.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Input.ProfileImage.FileName);
                string imagePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await Input.ProfileImage.CopyToAsync(fileStream);
                }

                return "/images/" + uniqueFileName;
            }

            return null;
        }

        private ApplicationUser CreateUserBasedOnRole()
        {
            if (Input.Role == SD.Role_Öğretmen)
            {
                return new Data.Models.Öğretmen
                {
                    UserName = Input.Email,
                    Ad = Input.Name,
                    Soyad = Input.Surname,
                    PhoneNumber = Input.PhoneNumber
                };
            }
            else if (Input.Role == SD.Role_Öğrenci)
            {
                return new Data.Models.Öğrenci
                {
                    UserName = Input.Email,
                    Ad = Input.Name,
                    Soyad = Input.Surname,
                    VeliTelefon = Input.PhoneNumber
                };
            }
            else
            {
                var user = new Data.Models.Veli
                {
                    UserName = Input.Email,
                    Ad = Input.Name,
                    Soyad = Input.Surname,
                    PhoneNumber = Input.PhoneNumber,
                    Öğrenciler = new List<Data.Models.Öğrenci>()
                };

                foreach (var studentId in Input.SelectedÖğrencilerIds)
                {
                    var öğrenci = _context.Öğrenciler.Find(studentId);
                    if (öğrenci != null)
                    {
                        öğrenci.Veli = user;
                        user.Öğrenciler.Add(öğrenci);
                    }
                }

                return user;
            }
        }

        private async Task AssignUserRoleAsync(ApplicationUser user)
        {
            if (!string.IsNullOrEmpty(Input.Role))
            {
                await _userManager.AddToRoleAsync(user, Input.Role);
            }
        }

        private IActionResult RedirectToConfirmationPage(ApplicationUser user, string returnUrl)
        {
            // Generate confirmation link for email
            var userId = _userManager.GetUserIdAsync(user).Result;
            var code = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                protocol: Request.Scheme);

            // Simply redirect to the confirmation page without sending the email
            return RedirectToPage("/Account/RegisterConfirmation", 
                new { area = "Identity", email = Input.Email, returnUrl = returnUrl });
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
