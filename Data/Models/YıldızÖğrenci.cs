using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TestIdentityApp.Data.Models;

namespace TestIdentityApp.Models;
public class YıldızÖğrenci
{
    public int Id { get; set; }
    public string ÖğrenciId { get; set; } // Use string to match the Id type
    public DateTime? Hafta { get; set; }
}

