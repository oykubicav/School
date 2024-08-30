using Microsoft.AspNetCore.Identity;
using TestIdentityApp.Models;

namespace TestIdentityApp.Data.Models;

public class Veli:ApplicationUser
{
   
    

   
   
    public ICollection<Öğrenci> Öğrenciler { get; set; }



}