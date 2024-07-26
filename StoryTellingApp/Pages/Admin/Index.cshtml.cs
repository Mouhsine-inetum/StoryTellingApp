using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;
using StoryTellingApp.Entity;
using StoryTellingApp.Factory.Interfaces;

namespace StoryTellingApp.Pages.Admin
{
    [AuthorizeForScopes(Scopes = ["https://talestale.onmicrosoft.com/860a243f-8d96-47d1-85b0-7971f7ca99a2/access_as_user", "https://talestale.onmicrosoft.com/860a243f-8d96-47d1-85b0-7971f7ca99a2/access_as_admin"])]
    public class IndexModel : PageModel
    {
        private readonly IAdminClient _adminClient;
        public List<UserDisplay> users;
        public IndexModel(IAdminClient adminClient)
        {
            _adminClient = adminClient;
        }
        public async Task OnGet()
        {
            PaginatedItems<UserDisplay> result = await _adminClient.Get();
            users=result.Items;
        }
    }
}
