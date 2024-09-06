using System.ComponentModel.DataAnnotations.Schema;

namespace TestIdentityApp.Data.Models;

public class DersIcerik
{  public int? Id { get; set; }
    public int? DersId { get; set; }
    public string? DersAdi { get; set; }
    public string? Konu { get; set; }
    public string? IcerikFileePath { get; set; } 
    [NotMapped]
    public IEnumerable<IFormFile>? IcerikFiles { get; set; }  
}