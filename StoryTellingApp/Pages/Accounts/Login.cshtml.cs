using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoryTellingApp.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        public async Task OnGet()
        {
            var scheme=OpenIdConnectDefaults.AuthenticationScheme;
            var redirectUri = Url.ActionContext.HttpContext.Request.Scheme + "://" + Url.ActionContext.HttpContext.Request.Host;
            var authProperties = new AuthenticationProperties()
            {
                RedirectUri = redirectUri,
            };
            await HttpContext.ChallengeAsync(scheme, authProperties);
        }
    }
}
