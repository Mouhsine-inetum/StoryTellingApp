using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Identity.Web;

namespace StoryTellingApp.Pages.Accounts
{
    public class LogoutModel : PageModel
    {
     
        public async Task OnGet()
        {
            var scheme = OpenIdConnectDefaults.AuthenticationScheme;
 
            await HttpContext.SignOutAsync(scheme, new AuthenticationProperties()
            {
                RedirectUri = "https://login.microsoftonline.com/bc284a57-173f-4482-8248-5adf6f281fe1/oauth2/v2.0/logout"
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }
    }
}
