namespace TestIdentityApp.Data.Models;

public class HomeViewModel
{


public string Name { get; set; }
    public string Surname { get; set; }
    public ApplicationUser User { get; set; }
    public IList<string> Roles { get; set; } =new List<string>(); 
}
