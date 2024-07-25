using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;
using StoryTellingApp.Data.Services;
using StoryTellingApp.Entity;
using StoryTellingApp.Factory.Interfaces;

namespace StoryTellingApp.Pages.Tags
{
    [AuthorizeForScopes(Scopes = ["https://talestale.onmicrosoft.com/860a243f-8d96-47d1-85b0-7971f7ca99a2/read:item"])]
    public class IndexModel : PageModel
    {

        private ITagClient _tagClient;
        public IList<Entity.Tags> Tags;
        public IndexModel(ITagClient tagClient)
        {
            _tagClient = tagClient;
        }
        public async Task OnGet()
        {
            var value = await _tagClient.GetVM();
            Tags= value.Tags;
        }
    }
}
