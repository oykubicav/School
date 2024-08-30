using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TestIdentityApp.Data.Models;

namespace TestIdentityApp.Models;

public class YıldızÖğrenci
{
    public int? Id { get; set; }
    public DateTime? Hafta { get; set; }
    public int? ÖğrenciId { get; set; }
    public Öğrenci? Öğrenci { get; set; }
    [ValidateNever]
    public string ImageUrl{ get; set; }

}