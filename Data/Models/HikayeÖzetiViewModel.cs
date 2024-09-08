namespace TestIdentityApp.Data.Models;

public class HikayeÖzetiViewModel
{
    public int? Id { get; set; }
    public string? HikayeAdı { get; set; }
    public string? Özet { get; set; }
    
    // Properties for student information
    public string? ÖğrenciName { get; set; }
    public string? ÖğrenciSurname { get; set; }  
}