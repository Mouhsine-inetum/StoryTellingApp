using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace StoryTellingApp.Pages.Accounts
{
    public class LogoutModel : PageModel
    {
        public async Task OnGet()
        {
            var scheme = OpenIdConnectDefaults.AuthenticationScheme;
 
            await HttpContext.SignOutAsync(scheme, new AuthenticationProperties());
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }
    }
}
