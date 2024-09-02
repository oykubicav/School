namespace TestIdentityApp.Data.Models;

public class HomeViewModel
{



    public ApplicationUser User { get; set; }
    public IList<string> Roles { get; set; } =new List<string>(); 
}
