using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.Build.Framework;
using TestIdentityApp.Models;
namespace TestIdentityApp.Data.Models;

public class HikayeÖzeti
{ 
    public int? Id { get; set; }
    
 

    public string? HikayeAdı{ get; set; }
    public string? Özet { get; set; }
   
    public string? ÖğrenciId { get; set; }
    

}