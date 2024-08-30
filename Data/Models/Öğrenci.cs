using Microsoft.AspNetCore.Identity;
using TestIdentityApp.Models;

namespace TestIdentityApp.Data.Models;

public class Öğrenci:ApplicationUser
{
   
  
    public string Sınıf { get; set; }
   
    public string VeliTelefon { get; set; }
   
    public ICollection<Ders> Dersler { get; set; }
    public ICollection<Ödev> Ödevler { get; set; }
    public ICollection<YıldızÖğrenci> YıldızÖğrenciler { get; set; }
    public ICollection<HikayeÖzeti> HikayeÖzetleri { get; set; }
    public string VeliId { get; set; }  // Foreign Key
    public Veli Veli { get; set; } 

}