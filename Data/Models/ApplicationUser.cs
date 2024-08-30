using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TestIdentityApp.Data.Models;

public class ApplicationUser:IdentityUser
{
    public string Ad { get; set; }
    public string Soyad { get; set; }
    [ValidateNever] public string? ImageUrl { get; set; }
   
}