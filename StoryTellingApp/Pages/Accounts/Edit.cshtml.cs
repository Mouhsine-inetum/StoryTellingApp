using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoryTellingApp.Pages.Accounts
{
    public class EditModel : PageModel
    {
        public async Task OnGet()
        {
            var authProperties = new AuthenticationProperties()
            {
                RedirectUri = "/",
            };
            await HttpContext.ChallengeAsync("B2C_1_b2c_editProfile", authProperties);
        }
    }
}
