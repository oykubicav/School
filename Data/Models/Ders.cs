using TestIdentityApp.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using TestIdentityApp.Models;


namespace TestIdentityApp.Data.Models;

public class Ders
{
    public int? DersId { get; set; }
    public string? DersAdi { get; set; }
    public DaysOfWeek? Günler {get; set; }
    public int? ÖğretmenId { get; set; }
    public Öğretmen?  Öğretmen { get; set; }
    public ICollection<Öğrenci>? Öğrenciler { get; set; }
    public ICollection<Ödev>? Ödevler { get; set; }
    
   
}
[Flags]
public enum DaysOfWeek
{
    Yok = 0,
    Pazartesi = 1 << 0,
    Salı = 1 << 1,
    Çarşamba = 1 << 2,
    Perşembe= 1 << 3,
    Cuma = 1 << 4,
   
}