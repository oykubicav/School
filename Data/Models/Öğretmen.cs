using Microsoft.AspNetCore.Identity;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Models;

namespace TestIdentityApp.Data.Models;

public class Öğretmen:ApplicationUser
{
  
 
    public ICollection<Ödev> Ödevler { get; set; }
    public ICollection<Ders> Dersler { get; set; }
    public ICollection<Öğrenci> Öğrenciler { get; set; }
    public ICollection<Veli> Veliler { get; set; }
    public ICollection<YıldızÖğrenci> YıldızÖğrenciler { get; set; }
    
    
    
    
}