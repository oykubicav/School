namespace TestIdentityApp.Data.Models;

public class Ödev
{
    public int? Id { get; set; }
    public string? Başlık { get; set; }
    public string? İçerik { get; set; }
    public DateTime? Tarih { get; set; }
    public int? ÖğretmenId { get; set; }
   
    public int? DersId { get; set; }
    public ICollection<Öğrenci>? SorumluÖğrenciler { get; set; }
}