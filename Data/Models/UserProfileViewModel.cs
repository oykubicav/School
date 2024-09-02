using TestIdentityApp.Models;

namespace TestIdentityApp.Data.Models;

public class UserProfileViewModel
{
    public ApplicationUser User { get; set; }
    public IList<string> Roles { get; set; }
    public List<HikayeÖzeti> HikayeOzetleri { get; set; }
    public List<Öğrenci> Öğrenciler { get; set; }
}