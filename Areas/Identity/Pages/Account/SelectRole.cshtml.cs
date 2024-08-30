using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace TestIdentityApp.Areas.Identity.Pages.Account
{
    public class SelectRole : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public SelectRole(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task OnGetAsync()
        {
            if (!await _roleManager.RoleExistsAsync(SD.Role_Öğretmen))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Öğretmen));
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Öğrenci));
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Veli));
            }
        }
    }
}